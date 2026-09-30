"""Extend the existing four-role ledger with the resource-only transaction.
Never applies the previous turn's proposed C# fixes to the working project.
"""
from pathlib import Path
import base64, difflib, hashlib, json, os, shutil, subprocess, sys, zipfile
sys.stdout.reconfigure(encoding='utf-8')
OUT=Path(__file__).resolve().parent; ROOT=OUT.parents[1]
T=OUT/'resource-transaction'; T.mkdir(exist_ok=True)
GIT='C:/Program Files/Git/bin/git.exe'
PY=sys.executable
def hashfile(p):return hashlib.sha256(p.read_bytes()).hexdigest()
def put(root,rel,data):
 p=root/rel;p.parent.mkdir(parents=True,exist_ok=True);p.write_bytes(data)
def head(rel):
 p=subprocess.run([GIT,'show','HEAD:'+rel],cwd=ROOT,capture_output=True)
 return p.stdout if p.returncode==0 else None
files=['UNITY_RUNTIME_INTEGRATION.md','UnityProject/Assets/Editor/RuntimeResourceRepair.cs','UnityProject/Assets/Editor/RuntimeResourceRepair.cs.meta',
 'UnityProject/Assets/AddressableAssetsData/AddressableAssetSettings.asset','UnityProject/Assets/TextMesh Pro/Resources/TMP Settings.asset',
 'UnityProject/Packages/com.le-tai.true-shadow/PROVENANCE.md','UnityProject/Packages/com.le-tai.true-shadow/Resources.meta',
 'tools/unity-rebuild/runtime_resources.py','tools/unity-rebuild/check_runtime_resource_contract.py','tools/unity-rebuild/classify_runtime_resource_log.py']
files += [p.relative_to(ROOT).as_posix() for p in (ROOT/'UnityProject/Packages/com.le-tai.true-shadow/Resources').rglob('*') if p.is_file()]
files += [p.relative_to(ROOT).as_posix() for p in (ROOT/'UnityProject/Assets/AddressableAssetsData').rglob('Manosaba.RuntimeFonts*') if p.is_file()]
# Native generated font artifacts are local-only, intentionally excluded from Git commit.
files += [p.relative_to(ROOT).as_posix() for p in (ROOT/'UnityProject/Assets/GameContent/RuntimeFonts').rglob('*') if p.is_file()]
files += ['UnityProject/Assets/GameContent/RuntimeFonts.meta']
files=sorted(set(files)); manifest={}
baseline=T/'baseline'; modified=T/'modified'
for rel in files:
 before=head(rel);after=(ROOT/rel).read_bytes()
 manifest[rel]={'baseline':hashlib.sha256(before).hexdigest() if before is not None else None,'modified':hashlib.sha256(after).hexdigest()}
 if before is not None:put(baseline,rel,before)
 put(modified,rel,after)
# Identical fixed fixture code is present in every snapshot; it is not changed by this transaction.
fixture=[]
for p in (ROOT/'UnityProject/Packages/com.le-tai.true-shadow').rglob('*.cs'):
 rel=p.relative_to(ROOT).as_posix();fixture.append(rel)
 for dest in (baseline,modified):put(dest,rel,p.read_bytes())
for dest in (baseline,modified):put(dest,'.manosaba-transaction-copy',b'resource-transaction-20260918\n')
(T/'manifest.json').write_text(json.dumps(manifest,indent=2),encoding='utf-8')
# Preserve earlier roles before extending them; previous proposed fixes are archival, not staged.
previous=OUT/'previous-transaction';previous.mkdir(exist_ok=True)
for name in ['MODIFIED_FILE.zip','DIFF_FILE.patch','VERIFICATION.txt','ROLLBACK.sh']:
 if not (previous/name).exists():shutil.copy2(OUT/name,previous/name)
# Binary-aware git patch includes assets exactly, without changing the repository's index.
env=dict(os.environ,GIT_CONFIG_NOSYSTEM='1',GIT_CEILING_DIRECTORIES=str(T))
p=subprocess.run([GIT,'-c','core.autocrlf=false','diff','--no-index','--binary','--no-renames','baseline','modified'],cwd=T,env=env,capture_output=True)
assert p.returncode in (0,1),p.stderr
patch=p.stdout.replace(b'a/baseline/',b'a/').replace(b'b/modified/',b'b/')
# New files use a/modified and removed files b/baseline in no-index headers.
patch=patch.replace(b'a/modified/',b'a/').replace(b'b/baseline/',b'b/')
(OUT/'DIFF_FILE.patch').write_bytes(patch)
lines=['#!/bin/sh','set -eu','test "$#" -eq 1 || { echo "usage: ROLLBACK.sh TARGET_COPY_DIRECTORY" >&2; exit 2; }',
 'root=$1','test -f "$root/.manosaba-transaction-copy" || { echo "Refusing unmarked directory; operate on a transaction copy, never your live project." >&2; exit 2; }']
for i,rel in enumerate(files):
 before=head(rel)
 if before is None:lines.append('rm -f -- "$root/'+rel+'"')
 else:
  lines+=['mkdir -p "$root/'+Path(rel).parent.as_posix()+'"','base64 -d > "$root/'+rel+'" <<\'BASELINE_'+str(i)+'\'',base64.b64encode(before).decode(),'BASELINE_'+str(i)]
(OUT/'ROLLBACK.sh').write_text('\n'.join(lines)+'\n',encoding='utf-8',newline='\n');(OUT/'ROLLBACK.sh').chmod(0o755)
events=[]
def run(cmd,cwd=ROOT,env=None):
 p=subprocess.run(cmd,cwd=cwd,env=env,capture_output=True,text=True,encoding='utf-8',errors='replace')
 events.append({'command':cmd,'cwd':str(cwd),'stdout':p.stdout,'stderr':p.stderr,'exit':p.returncode});return p
check=ROOT/'tools/unity-rebuild/check_runtime_resource_contract.py'
b=run([PY,str(check),str(baseline)]);m=run([PY,str(check),str(modified)])
assert b.returncode==1 and m.returncode==0
restored=T/'rollback-copy';shutil.copytree(modified,restored,dirs_exist_ok=True)
shellenv=dict(os.environ,PATH='C:\\Program Files\\Git\\usr\\bin;C:\\Program Files\\Git\\bin;'+os.environ.get('PATH',''))
r=run(['C:/Program Files/Git/bin/bash.exe',str(OUT/'ROLLBACK.sh'),restored.as_posix()],env=shellenv);assert r.returncode==0,r.stderr
r=run([PY,str(check),str(restored)]);assert r.returncode==b.returncode and r.stdout==b.stdout
for rel,info in manifest.items():
 p=restored/rel
 assert (hashfile(p) if p.exists() else None)==info['baseline'],rel
 info['rollback']=hashfile(p) if p.exists() else None
# Reapply the delivered patch to the rollback copy and verify every byte.
p=run([GIT,'-c','core.autocrlf=false','apply',str(OUT/'DIFF_FILE.patch')],restored,env);assert p.returncode==0,p.stderr
for rel,info in manifest.items():assert hashfile(restored/rel)==info['modified'],rel
assert run([PY,str(check),str(restored)]).returncode==0
# Reopen native data and package both current and prior records with explicit separation.
with zipfile.ZipFile(OUT/'MODIFIED_FILE.zip','w',zipfile.ZIP_DEFLATED) as z:
 for rel in files:z.write(modified/rel,'resource-transaction/'+rel)
 z.write(previous/'MODIFIED_FILE.zip','previous-transaction/MODIFIED_FILE.zip')
 z.writestr('README.txt','resource-transaction contains the current resource-only changes. previous-transaction is the earlier unapplied C# proposal, NOT part of this commit. GameContent includes LOCAL-ONLY generated assets, not permission to redistribute original fonts.\n')
with zipfile.ZipFile(OUT/'MODIFIED_FILE.zip') as z:assert z.testzip() is None
(T/'manifest.json').write_text(json.dumps(manifest,indent=2),encoding='utf-8')
(T/'events.json').write_text(json.dumps(events,indent=2),encoding='utf-8')
ledger=(previous/'VERIFICATION.txt').read_text(encoding='utf-8')+'\n\n=== RESOURCE TRANSACTION EXTENSION ===\n'
ledger+='Previous C# transaction is archived, NOT applied to the project or included in resource commit.\n'
ledger+='Matching on-disk command: BASELINE exit 1 (17 missing contracts); MODIFIED exit 0; ROLLBACK exit 1, identical baseline output and hashes; reapply exit 0. This rollback is a separate copy, NOT a Unity runtime rollback.\n'
ledger+='Real Unity evidence: baseline verify 14 failures; modified2 verify / modified verify-built 0 failures; G4b reached System/System_Title; shader null and all three font failures 0. CJK preview visually inspected.\n'
ledger+='\nCOMMAND EVENTS\n'+json.dumps(events,indent=2)+'\nHASHES\n'+json.dumps(manifest,indent=2)
for p in sorted((ROOT/'tools/unity-rebuild/out/runtime-resources').glob('*.execution.json')):
 ledger+='\n=== '+str(p)+' ===\n'+p.read_text(encoding='utf-8')
for rel in ['tools/unity-rebuild/out/runtime-resources/baseline-verify.json','tools/unity-rebuild/out/runtime-resources/modified2-verify.json','tools/unity-rebuild/out/runtime-resources/modified-verify-built.json','tools/unity-rebuild/out/unity/g1_compile.json','work/startup-fix-20260918/resource-research/modified-log-classification.json']:
 ledger+='\n=== '+rel+' ===\n'+(ROOT/rel).read_text(encoding='utf-8-sig')
(OUT/'VERIFICATION.txt').write_text(ledger,encoding='utf-8')
for name in ['MODIFIED_FILE.zip','DIFF_FILE.patch','VERIFICATION.txt','ROLLBACK.sh']:
 p=OUT/name;data=p.read_bytes();print(str(p.resolve()),len(data),hashlib.sha256(data).hexdigest())
print('RESOURCE BASELINE=1 MODIFIED=0 ROLLBACK=1 REAPPLIED=0; hash and binary patch verification passed')
