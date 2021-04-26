using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace NSTools
{
  
    public class ObservableData
    {
        private EventManager eventManager;
        private Dictionary<string, object> data;
        
        public ObservableData(){
            eventManager = new EventManager();
            data = new Dictionary<string, object>();
        }

        /// <summary>
        /// Set value of observable data
        /// </summary>
        /// <param name="key">Key of observable data</param>
        /// <param name="value">Value of observable data</param>
        /// <typeparam name="T">Observable data type</typeparam>
        public void Set<T>(string key, T value)
        {
            data[key] = value;
            eventManager.Invoke(key, value);
        }     
 

        /// <summary>
        /// Get value from observable data
        /// </summary>
        /// <param name="key">Key of observable data</param>
        /// <param name="fallback">Fallback value</param>
        /// <typeparam name="T">Observable data type</typeparam>
        /// <returns></returns>
        public T Get<T>(string key, T fallback)
        {
            data.TryGetValue(key, out var val);
            if (val is T cast)
                return cast;
            Log.Error($"Unable to get observable {key}: {val}");
            return fallback;
        }

        
        /// <summary>
        /// Bind method to data value until next scene change.
        /// Unbind methods on destroy! 
        /// </summary>
        /// <param name="key">Key of observable data</param>
        /// <param name="callback">Action to bind</param>
        /// <typeparam name="T">Observable data type</typeparam>
        public void Bind<T>(string key, Action<T> callback)
        {
            eventManager.Bind(key, callback);
        }
        
        /// <summary>
        /// Bind method to data value globally (Use in DDOL)
        /// </summary>
        /// <param name="key">Key of observable data</param>
        /// <param name="callback">Action to bind</param>
        /// <typeparam name="T">Observable data type</typeparam>
        public void BindGlobal<T>(string key, Action<T> callback)
        {
            eventManager.BindGlobal(key, callback);
        }

        /// <summary>
        /// Remove method from value observer list
        /// </summary>
        /// <param name="key">Key of observable data</param>
        /// <param name="callback">Action to bind</param>
        /// <typeparam name="T">Observable data type</typeparam>
        public void Unbind<T>(string key, Action<T> callback)
        {
            eventManager.Unbind(key, callback);
        }

        /// <summary>
        /// Remove method from value global observer list (Use in DDOL)
        /// </summary>
        /// <param name="key">Key of observable data</param>
        /// <param name="callback">Action to bind</param>
        /// <typeparam name="T">Observable data type</typeparam>
        public void UnbindGlobal<T>(string key, Action<T> callback)
        {
            eventManager.UnbindGlobal(key, callback);
        }
    }
}
