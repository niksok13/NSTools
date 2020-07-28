using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace NSTools
{
    public class EventManager
    {
        private Dictionary<string, Action<object[]>> binds, global_binds;

        private static EventManager instance;
        public static EventManager Instance
        {
            get{
                if (instance == null)
                {
                    instance = new EventManager();
                    SceneManager.sceneLoaded += (a,b)=> instance.binds = new Dictionary<string, Action<object[]>>();
                }
                return instance;
            }
        }

        public EventManager()
        {
            binds = new Dictionary<string, Action<object[]>>();
            global_binds = new Dictionary<string, Action<object[]>>();
        }

        public void Bind(string name, Action<object[]> ev)
        {
            if (binds.ContainsKey(name)) 
                binds[name] += ev;
            else
                binds[name] = ev;
        }

        public void Unbind(string name, Action<object[]> ev)
        {
            if (binds.ContainsKey(name)) binds[name] -= ev;
        }
        
        public void BindGlobal(string name, Action<object[]> ev)
        {
            if (global_binds.ContainsKey(name)) 
                global_binds[name] += ev;
            else
                global_binds[name] = ev;
        }

        public void UnbindGlobal(string name, Action<object[]> ev)
        {
            if (global_binds.ContainsKey(name)) global_binds[name] -= ev;
        }

        public void Invoke(string name, params object[] args)
        {
            if (binds.ContainsKey(name))
                foreach (var action in binds[name].GetInvocationList())
                    ((Action<object[]>)action).BeginInvoke(args,null,null);
            if (global_binds.ContainsKey(name)) 
                foreach (var action in global_binds[name].GetInvocationList())
                    ((Action<object[]>)action).BeginInvoke(args,null,null);
        }
    }
}
