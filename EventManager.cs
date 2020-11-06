using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace NSTools
{
    public static class EventManager
    {
        private static Dictionary<string, Action<object>> binds, global_binds;

        static EventManager()
        {
            binds = new Dictionary<string, Action<object>>();
            global_binds = new Dictionary<string, Action<object>>();
            SceneManager.sceneUnloaded += s=> binds = new Dictionary<string, Action<object>>();
        }

        public  static void Bind(string name, Action<object> ev)
        {
            Log.Trace($"EventManager Bind {ev.Target} to {name}", ev.Target as Object);
            if (binds.ContainsKey(name))
                binds[name] += ev;
            else
                binds[name] = ev;
        }

        public  static void Unbind(string name, Action<object> ev)
        {
            if (binds.ContainsKey(name)) binds[name] -= ev;
        }
        
        public static  void BindGlobal(string name, Action<object> ev)
        {
            Log.Trace($"EventManager BindGlobal {ev.Target} to {name}", ev.Target as Object);
            if (global_binds.ContainsKey(name))
                global_binds[name] += ev;
            else
                global_binds[name] = ev;
        }

        public static  void UnbindGlobal(string name, Action<object> ev)
        {
            if (global_binds.ContainsKey(name)) global_binds[name] -= ev;
        }

        public  static void Invoke(string name, object arg=null)
        {
            Log.Trace($"Invoke {name} - {arg}", Camera.main);
            if (binds.ContainsKey(name))
                binds[name].DynamicInvoke(arg);
            if (global_binds.ContainsKey(name)) 
                global_binds[name].DynamicInvoke(arg);
        }
    }
}
