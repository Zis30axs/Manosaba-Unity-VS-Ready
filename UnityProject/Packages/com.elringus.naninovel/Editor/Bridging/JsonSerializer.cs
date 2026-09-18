using System;

namespace Naninovel
{
    #if JSON_AVAILABLE
    public class JsonSerializer : ISerializer
    {
        private static readonly Newtonsoft.Json.JsonSerializerSettings settings = new() {
            ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver {
                NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
            },
            Formatting = Newtonsoft.Json.Formatting.None,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore,
            DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include
        };

        public string Serialize (object poco) => Newtonsoft.Json.JsonConvert.SerializeObject(poco, Newtonsoft.Json.Formatting.None, settings);
        public string Serialize (object poco, Type type) => Newtonsoft.Json.JsonConvert.SerializeObject(poco, type, Newtonsoft.Json.Formatting.None, settings);
        public object Deserialize (string serialized, Type type) => Newtonsoft.Json.JsonConvert.DeserializeObject(serialized, type, settings);
    }
    #else
    public class JsonSerializer : ISerializer
    {
        public string Serialize (object poco) => UnityEngine.JsonUtility.ToJson(poco);
        public string Serialize (object poco, Type type) => UnityEngine.JsonUtility.ToJson(poco);
        public object Deserialize (string serialized, Type type) => UnityEngine.JsonUtility.FromJson(serialized, type);
    }
    #endif
}
