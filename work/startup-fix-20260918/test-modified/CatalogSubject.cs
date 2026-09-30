using System; using System.Collections.Generic; using System.IO; using System.Reflection;
using UnityEngine.AddressableAssets.ResourceLocators;
class CatalogSubject {
class CatalogProbe { public string path,status,error,locator_id; public long bytes; public int keys,location_edges,keys_with_multiple_locations; }
public static string Run(string path) { var result=new CatalogSubject().ProbeCatalog(path); Console.WriteLine("catalog="+result.status+" "+result.error); return result.status; }
        private CatalogProbe ProbeCatalog(string catalogPath)
        {
            var probe = new CatalogProbe { path = catalogPath };
            // Only the shipped reference catalog retains the pre-reconstruction name.
            // Keep this alias scoped to its synchronous probe, not all runtime loads.
            ResolveEventHandler resolveLegacyAssembly = (sender, args) =>
                new AssemblyName(args.Name).Name == "Assembly-CSharp"
                    ? typeof(WitchTrials.Views.GameInitializer).Assembly : null;
            AppDomain.CurrentDomain.AssemblyResolve += resolveLegacyAssembly;
            try
            {
                if (!File.Exists(catalogPath))
                {
                    probe.status = "ABSENT";
                    return probe;
                }

                probe.bytes = new FileInfo(catalogPath).Length;

                Type ccd = Type.GetType("UnityEngine.AddressableAssets.ResourceLocators.ContentCatalogData, Unity.Addressables");
                if (ccd == null)
                {
                    probe.status = "TYPE_MISSING";
                    return probe;
                }

                const BindingFlags any = BindingFlags.Public | BindingFlags.NonPublic |
                                         BindingFlags.Static | BindingFlags.Instance;
                MethodInfo load = ccd.GetMethod("LoadFromFile", any, null, new[] { typeof(string), typeof(int) }, null);
                if (load == null)
                {
                    probe.status = "LOADER_MISSING";
                    return probe;
                }

                object data = load.Invoke(null, new object[] { catalogPath, 1024 });

                MethodInfo makeLocator = ccd.GetMethod("CreateCustomLocator", any, null,
                    new[] { typeof(string), typeof(string), typeof(int) }, null);
                if (makeLocator == null)
                {
                    probe.status = "LOCATOR_FACTORY_MISSING";
                    return probe;
                }

                var locator = makeLocator.Invoke(data, new object[] { catalogPath, null, 1024 }) as IResourceLocator;
                if (locator == null)
                {
                    probe.status = "LOCATOR_NULL";
                    return probe;
                }

                probe.locator_id = locator.LocatorId;
                foreach (object key in locator.Keys)
                {
                    probe.keys++;
                    IList<UnityEngine.ResourceManagement.ResourceLocations.IResourceLocation> locations;
                    if (locator.Locate(key, typeof(object), out locations) && locations != null)
                    {
                        probe.location_edges += locations.Count;
                        if (locations.Count > 1) probe.keys_with_multiple_locations++;
                    }
                }
                probe.status = "PARSED";
            }
            catch (Exception ex)
            {
                Exception inner = ex is TargetInvocationException && ex.InnerException != null ? ex.InnerException : ex;
                probe.status = "ERROR";
                probe.error = inner.GetType().Name + ": " + inner.Message;
            }
            finally
            {
                AppDomain.CurrentDomain.AssemblyResolve -= resolveLegacyAssembly;
            }
            return probe;
        }


}