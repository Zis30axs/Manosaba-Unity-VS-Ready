using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
namespace UnityEngine.ResourceManagement.ResourceLocations { public interface IResourceLocation {} }
namespace UnityEngine.AddressableAssets.ResourceLocators {
 using UnityEngine.ResourceManagement.ResourceLocations;
 public interface IResourceLocator { string LocatorId{get;} IEnumerable Keys{get;} bool Locate(object key, Type type, out IList<IResourceLocation> locations); }
 public class ContentCatalogData: IResourceLocator {
  public static ContentCatalogData LoadFromFile(string path,int size)=>new ContentCatalogData();
  public IResourceLocator CreateCustomLocator(string path,string suffix,int size)=>this;
  public string LocatorId=>"synthetic-legacy-fixture";
  public IEnumerable Keys=>new[]{"fixture-key"};
  public bool Locate(object key,Type type,out IList<IResourceLocation> locations) {
   var a=Assembly.Load("Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
   if(a.GetType("WitchTrials.Views.GameInitializer")==null)throw new Exception("type missing");
   locations=new List<IResourceLocation>(); return true;
  }
 }
}
