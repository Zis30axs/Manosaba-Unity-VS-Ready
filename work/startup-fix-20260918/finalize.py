from pathlib import Path
import subprocess, sys, hashlib, shutil, difflib, json, zipfile, os
sys.stdout.reconfigure(encoding='utf-8')
OUT=Path(__file__).resolve().parent; ROOT=OUT.parents[1]
manifest=json.loads((OUT/'manifest.json').read_text())
def digest(p):return hashlib.sha256(p.read_bytes()).hexdigest()
events=[]
def run(cmd,cwd=OUT,env=None):
 p=subprocess.run(cmd,cwd=cwd,env=env,capture_output=True,text=True,encoding='utf-8',errors='replace')
 events.append('COMMAND '+json.dumps(cmd)+'\nSTDOUT\n'+p.stdout+'\nSTDERR\n'+p.stderr+'\nEXIT '+str(p.returncode))
 return p
restored=OUT/'rollback-copy'
shutil.copytree(OUT/'modified',restored,dirs_exist_ok=True)
p=run(['C:/Program Files/Git/bin/bash.exe',str(OUT/'ROLLBACK.sh'),restored.as_posix()],env=dict(os.environ,PATH='C:\\Program Files\\Git\\usr\\bin;C:\\Program Files\\Git\\bin;'+os.environ.get('PATH','')));assert p.returncode==0,p.stderr
for rel,h in manifest.items():assert digest(restored/rel)==h
p=run([sys.executable,str(OUT/'check.py'),str(restored),str(OUT/'test-rollback')])
(OUT/'ROLLBACK.txt').write_text(p.stdout+'\nSTDERR\n'+p.stderr+'\nEXIT '+str(p.returncode),encoding='utf-8')
assert p.returncode==1 and 'failures=3' in p.stdout
patch=''
for rel in manifest:
 a=(OUT/'baseline'/rel).read_bytes().decode('utf-8').splitlines(True)
 b=(OUT/'modified'/rel).read_bytes().decode('utf-8').splitlines(True)
 patch+=''.join(difflib.unified_diff(a,b,fromfile='a/'+rel,tofile='b/'+rel))
(OUT/'DIFF_FILE.patch').write_bytes(patch.encode('utf-8'))
rebuilt=OUT/'patch-copy';shutil.copytree(OUT/'baseline',rebuilt,dirs_exist_ok=True)
env=dict(os.environ,GIT_CEILING_DIRECTORIES=str(OUT))
p=run(['C:/Program Files/Git/bin/git.exe','-c','core.autocrlf=false','apply','--check',str(OUT/'DIFF_FILE.patch')],rebuilt,env);assert p.returncode==0,p.stderr
p=run(['C:/Program Files/Git/bin/git.exe','-c','core.autocrlf=false','apply',str(OUT/'DIFF_FILE.patch')],rebuilt,env);assert p.returncode==0,p.stderr
for rel in manifest:assert digest(rebuilt/rel)==digest(OUT/'modified'/rel),rel
# The delivered modified file remains patched; rollback only ever touched a separate copy.
with zipfile.ZipFile(OUT/'MODIFIED_FILE.zip','w',zipfile.ZIP_DEFLATED) as z:
 for rel in manifest:z.write(OUT/'modified'/rel,rel)
with zipfile.ZipFile(OUT/'MODIFIED_FILE.zip') as z:
 assert z.testzip() is None
 for rel in manifest:assert z.read(rel)==(OUT/'modified'/rel).read_bytes()
for rel,h in manifest.items():assert digest(ROOT/rel)==h,'original changed: '+rel
hashes={rel:{'original':h,'modified':digest(OUT/'modified'/rel),'rollback':digest(restored/rel),'patch_rebuilt':digest(rebuilt/rel)} for rel,h in manifest.items()}
header='''TARGET: E:/Manosaba-VSCodeReady/UnityProject startup source files identified in supplied log.
Changed symbols:
- LocalizeImage.OnEnable: bind missing _image before synchronous subscription; missing component reports error.
- LocalizeImage.OnDisable: guard an unbound _image during teardown.
- ManosabaBoot.ProbeCatalog: map only Assembly-CSharp to GameInitializer's assembly within try/finally.
BASELINE: 3 failures; NullReferenceException on unbound Image and early disable, FileNotFoundException in legacy catalog fixture; exit 1.
MODIFIED: same fixture inputs, 5 checks pass; exit 0.
ROLLBACK: restores baseline failures, exit 1; both SHA256 hashes match originals.
REAL COMPILE: full modified files compile with installed Unity and project assemblies, exit 0 (serialized-field warnings remain).
BOUNDARIES: tests use actual LocalizeImage source with Unity/service doubles and exact ProbeCatalog method with a synthetic catalog implementation. No Unity Play Mode or real shipped binary catalog validation was performed. Original project files remain unchanged; ZIP contains drop-in source copies. No claim that full startup is fixed.
Unresolved issues in supplied log:
- TMP font assets for three configured languages must be restored/generated and registered; a raw Font is not TMP_FontAsset.
- TrueShadow UI shaders referenced by BlendModeExtensions are missing from inspected shader sources; graphics not repaired/tested.
- DefaultLocalePresenter constructor registers IDefaultLocaleSelector and GameInitializer.InitializeAsyncCore registers another instance; duplicate warning remains.
- processInput warning not changed.
Tool errors retained: venv Python process launch exit 101; output encoding exception exit 1 (fixed with explicit UTF-8); prepare.py CRLF assertion exit 1 (normalized only working copy); first modified run exit 1; real compile missing Il2CppDummyDll reference exit 1 (added actual plugin reference). These are not evaluator successes.
'''
logs=[]
for name in ['BASELINE.txt','MODIFIED.txt','ROLLBACK.txt','REAL_COMPILE.txt']:
 logs.append('\n=== '+name+' ===\n'+(OUT/name).read_text(encoding='utf-8'))
verification=header+'\nHASHES\n'+json.dumps(hashes,indent=2)+'\n'+'\n'.join(events)+''.join(logs)
(OUT/'VERIFICATION.txt').write_text(verification,encoding='utf-8')
# Reopen every role and every modified source before reporting.
for name in ['MODIFIED_FILE.zip','DIFF_FILE.patch','VERIFICATION.txt','ROLLBACK.sh']:
 p=OUT/name; data=p.read_bytes();assert data;print(str(p.resolve()),'bytes='+str(len(data)),'sha256='+hashlib.sha256(data).hexdigest())
for rel in manifest:assert (OUT/'modified'/rel).read_bytes()==(rebuilt/rel).read_bytes()
print('BASELINE=1 MODIFIED=0 ROLLBACK=1 REAL_COMPILE=0; rollback hashes and patch reconstruction exact; originals unchanged')
