from pathlib import Path
import subprocess, sys, json
sys.stdout.reconfigure(encoding='utf-8')

OUT=Path(__file__).resolve().parent
source=Path(sys.argv[1]).resolve()
build=Path(sys.argv[2]).resolve(); build.mkdir(parents=True,exist_ok=True)
unity=Path('E:/Unity/6000.0.48f1/Editor/Data')
csc=[str(unity/'NetCoreRuntime/dotnet.exe'),str(unity/'DotNetSdkRoslyn/csc.dll')]
mono=str(unity/'MonoBleedingEdge/bin/mono.exe')
refs=unity/'MonoBleedingEdge/lib/mono/4.8-api'
common=['-nologo','-utf8output','-langversion:9.0','-nostdlib+',*[f'-r:{refs/x}' for x in ('mscorlib.dll','System.dll','System.Core.dll')]]
def run(cmd):
    p=subprocess.run(cmd,cwd=build,capture_output=True,text=True,encoding='utf-8',errors='replace')
    print('COMMAND '+json.dumps(cmd)); print('STDOUT\n'+p.stdout,end=''); print('STDERR\n'+p.stderr,end=''); print('EXIT '+str(p.returncode))
    return p.returncode
if run(csc+common+['-target:library',f'-out:{build/"Unity.Addressables.dll"}',str(OUT/'CatalogFixture.cs')]):sys.exit(2)
boot=(source/'UnityProject/Assets/Scripts/Runtime/ManosabaBoot.cs').read_text(encoding='utf-8')
start=boot.index('        private CatalogProbe ProbeCatalog(')
end=boot.index('        private static bool HasFlag',start)
method=boot[start:end]
wrapper='''using System; using System.Collections.Generic; using System.IO; using System.Reflection;
using UnityEngine.AddressableAssets.ResourceLocators;
class CatalogSubject {
class CatalogProbe { public string path,status,error,locator_id; public long bytes; public int keys,location_edges,keys_with_multiple_locations; }
public static string Run(string path) { var result=new CatalogSubject().ProbeCatalog(path); Console.WriteLine("catalog="+result.status+" "+result.error); return result.status; }
'''+method+'\n}'
(build/'CatalogSubject.cs').write_text(wrapper,encoding='utf-8')
(build/'catalog-fixture.bin').write_bytes(b'SYNTHETIC TEST FIXTURE')
cmd=csc+common+[f'-r:{build/"Unity.Addressables.dll"}',f'-out:{build/"WitchTrials.Game.exe"}',
str(OUT/'Harness.cs'),str(build/'CatalogSubject.cs'),str(source/'UnityProject/Assets/Scripts/Game/WitchTrials.Game/WitchTrials.Views/LocalizeImage.cs')]
if run(cmd):sys.exit(2)
sys.exit(run([mono,str(build/'WitchTrials.Game.exe'),str(build/'catalog-fixture.bin')]))
