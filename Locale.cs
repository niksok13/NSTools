using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;


// Simple i18n tool. Depends on Newtonsoft.Json.
// How to use: Locale.Get("message.hello", username)

namespace NSTools
{
    public static class Locale
    {
        public static Dictionary<string, string> data;
 
        public static void Init(string lang = "en")
        {
            string res = Resources.Load<TextAsset>($"locale/{lang}").text;
            data = new Dictionary<string, string>();
            var tree = JsonConvert.DeserializeObject<Dictionary<string,Dictionary<string, string>>>(res);
            foreach (var part in tree)
                foreach (var entry in part.Value)
                    data[$"{part.Key}.{entry.Key}"] = entry.Value;
        }

        public static string Get(string key, params string[] args)
        {
            if (data==null) Init();
            var result = key;
            if (data.ContainsKey(key))
                result = data[key];
            return string.Format(result, args);
        }
    }
}
