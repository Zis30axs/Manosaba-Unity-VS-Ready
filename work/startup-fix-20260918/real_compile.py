from pathlib import Path
import subprocess, sys, json
sys.stdout.reconfigure(encoding='utf-8')
OUT=Path(__file__).resolve().parent
ROOT=OUT.parents[1]
U=Path('E:/Unity/6000.0.48f1/Editor/Data')
references=list((U/'Managed/UnityEngine').glob('*.dll'))
references+=list((ROOT/'UnityProject/Library/ScriptAssemblies').glob('*.dll'))
references+=[ROOT/'UnityProject/Packages/com.elringus.naninovel/Plugins/Naninovel.Common.dll']
references+=[ROOT/'UnityProject/Assets/Plugins/Il2CppStubs/Il2CppDummyDll.dll']
references += [U/'MonoBleedingEdge/lib/mono/4.8-api'/n for n in ['mscorlib.dll','System.dll','System.Core.dll','Facades/netstandard.dll']]
files=list((OUT/'modified').rglob('*.cs'))
cmd=[str(U/'NetCoreRuntime/dotnet.exe'),str(U/'DotNetSdkRoslyn/csc.dll'),'-nologo','-utf8output','-target:library','-nostdlib+','-langversion:9.0',f'-out:{OUT/"real-source-check.dll"}']
cmd += [f'-r:{p}' for p in references]+[str(p) for p in files]
p=subprocess.run(cmd,capture_output=True,text=True,encoding='utf-8',errors='replace')
print('COMMAND '+json.dumps(cmd));print('STDOUT\n'+p.stdout);print('STDERR\n'+p.stderr);print('EXIT '+str(p.returncode))
sys.exit(p.returncode)
