using System;
using System.Collections.Generic;

namespace NSTools
{
  
    public class ReactiveDict
    {
        private Dictionary<string, object> data;
        private EventManager eventManager;

        public ReactiveDict()
        {
            data = new Dictionary<string, object>();
            eventManager = new EventManager();
        }

        public void Set<T>(string name, T value)
        {
            data[name] = value;
            eventManager.Invoke(name, value);
        }

        public T Get<T>(string name, T fallback)
        {
            if (data.ContainsKey(name))
                return (T)data[name];
            return fallback;
        }

        public void Bind(string name, Action<object[]> callback)
        {
            eventManager.Bind(name, callback);
        }

        public void Unbind(string name, Action<object[]> callback)
        {
            eventManager.Unbind(name, callback);
        }
    }
}
