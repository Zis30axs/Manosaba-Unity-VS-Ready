using System;
using System.Collections.Generic;
using System.Reflection;
using GigaCreation.Essentials.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Il2CppDummyDll {
 [AttributeUsage(AttributeTargets.All)] public class TokenAttribute:Attribute { public string Token; }
 [AttributeUsage(AttributeTargets.All)] public class AddressAttribute:Attribute { public string RVA, Offset, VA; }
 [AttributeUsage(AttributeTargets.All)] public class FieldOffsetAttribute:Attribute { public string Offset; }
}
namespace UnityEngine {
 public class MonoBehaviour { public object Component; public T GetComponent<T>() where T:class => Component as T; }
 public class Sprite { public string Name; public Sprite(string n) { Name=n; } }
 public class SerializeField:Attribute {}
 public class RequireComponent:Attribute { public RequireComponent(Type t) {} }
 public static class Debug { public static void LogError(string s, object o) { Console.WriteLine("ERROR: "+s); } }
}
namespace UnityEngine.UI { public class Image { public Sprite sprite, overrideSprite; public int Resizes; public void SetNativeSize() { Resizes++; } } }
namespace GigaCreation.Essentials.Localization {
 public enum LocaleKind { Ja, EnUs, ZhHans }
 public interface ILocalizationProvider { LocaleKind SourceLocale {get;} LocaleKind CurrentLocale {get;} IObservable<LocaleKind> OnLocaleChangedAsObservable(); }
 public interface ILocalizationManager:ILocalizationProvider {}
}
namespace GigaCreation.Tools.Service { public static class ServiceLocator { public static object Value; public static T Get<T>(bool b) => (T)Value; } }
namespace UniRx {
 public static class Extensions {
  private class Observer<T>:IObserver<T> { public Action<T> Action; public void OnNext(T t)=>Action(t); public void OnError(Exception e) {throw e;} public void OnCompleted(){} }
  public static IDisposable Subscribe<T>(this IObservable<T> o, Action<T> f) => o.Subscribe(new Observer<T>{Action=f});
  public static T AddTo<T>(this T d, object owner) where T:IDisposable => d;
 }
}
namespace WitchTrials.Views { public class GameInitializer {} }
class Manager:ILocalizationManager,IObservable<LocaleKind>,IDisposable {
 public LocaleKind SourceLocale=>LocaleKind.Ja; public LocaleKind CurrentLocale=>LocaleKind.ZhHans;
 public bool Disposed; public IObservable<LocaleKind> OnLocaleChangedAsObservable()=>this;
 public IDisposable Subscribe(IObserver<LocaleKind> o) { o.OnNext(CurrentLocale); return this; }
 public void Dispose(){Disposed=true;}
}
class Program {
 static int failures;
 static void Check(string name, Action run) { try {run(); Console.WriteLine(name+": PASS");} catch(Exception e) { failures++; while(e is TargetInvocationException && e.InnerException!=null)e=e.InnerException; Console.WriteLine(name+": "+e.GetType().Name+": "+e.Message); } }
 static void Expect(bool b) { if(!b)throw new Exception("assertion failed"); }
 static Type T=typeof(WitchTrials.Views.LocalizeImage);
 static BindingFlags F=BindingFlags.Instance|BindingFlags.NonPublic;
 static void Set(object o,string f,object value)=>T.GetField(f,F).SetValue(o,value);
 static void Call(object o,string m, params object[] args)=>T.GetMethod(m,F).Invoke(o,args);
 static void ImageCase(bool bind) {
  var manager=new Manager(); GigaCreation.Tools.Service.ServiceLocator.Value=manager;
  var original=new Sprite("source"); var translated=new Sprite("zh-Hans");
  var image=new Image{sprite=original}; var item=new WitchTrials.Views.LocalizeImage{Component=image};
  if(bind)Set(item,"_image",image);
  var ct=T.GetNestedType("LocaleContent",BindingFlags.NonPublic); var c=Activator.CreateInstance(ct,true);
  ct.GetField("Locale").SetValue(c,LocaleKind.ZhHans); ct.GetField("Sprite").SetValue(c,translated);
  var array=Array.CreateInstance(ct,1); array.SetValue(c,0); Set(item,"_settings",array);
  Call(item,"OnEnable"); Expect(image.overrideSprite==translated); Expect(image.Resizes==2);
  Call(item,"ModifyImageByLocale",LocaleKind.EnUs); Expect(image.overrideSprite==original);
  Call(item,"OnDisable"); Expect(image.overrideSprite==null && manager.Disposed);
 }
 static int Main(string[] args) {
  Console.WriteLine("Test boundary: real LocalizeImage source; Unity/UI/service test doubles. Catalog method uses synthetic legacy-assembly fixture, not Unity Play Mode.");
  Check("unbound Image + synchronous locale replay + fallback + disable",()=>ImageCase(false));
  Check("existing Image binding preserved",()=>ImageCase(true));
  Check("disable before enable",()=>Call(new WitchTrials.Views.LocalizeImage(),"OnDisable"));
  Check("catalog missing path and resolver cleanup",()=>{
   Expect(CatalogSubject.Run("missing-catalog-fixture.bin")=="ABSENT");
   try { Assembly.Load("Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"); throw new Exception("resolver leaked"); }
   catch(System.IO.FileNotFoundException) {}
  });
  Check("legacy catalog Assembly-CSharp maps to reconstructed assembly",()=>Expect(CatalogSubject.Run(args[0])=="PARSED"));
  Console.WriteLine("failures="+failures); return failures==0?0:1;
 }
}
