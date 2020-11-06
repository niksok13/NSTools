using System;
using System.Collections.Generic;

namespace NSTools
{
  
    public static class Model
    {
        private static Dictionary<string, object> data = new Dictionary<string, object>();
        
        public static void Set<T>(string name, T value)
        {
            data[name] = value;
            EventManager.Invoke("model."+name, value);
        }

        public static T Get<T>(string name, T fallback)
        {
            if (data.ContainsKey(name))
                return (T)data[name];
            return fallback;
        }

        public static void Bind(string name, Action<object> callback)
        {
            EventManager.Bind("model."+name, callback);
            callback.Invoke(null);
        }

        public static void BindGlobal(string name, Action<object> callback)
        {
            EventManager.BindGlobal("model."+name, callback);
            callback.Invoke(null);
        }

        public static void Unbind(string name, Action<object> callback)
        {
            EventManager.Unbind("model."+name, callback);
        }

        public static void UnbindGlobal(string name, Action<object> callback)
        {
            EventManager.UnbindGlobal("model."+name, callback);
        }
    }
}
