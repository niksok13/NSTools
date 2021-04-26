using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace NSTools
{
    public class EventManager
    {
        
        private Dictionary<string, Delegate> binds, global_binds;

        public EventManager()
        {
            binds = new Dictionary<string, Delegate>();
            global_binds = new Dictionary<string, Delegate>();
            SceneManager.sceneUnloaded += s=> binds = new Dictionary<string, Delegate>();
        }

        /// <summary>
        /// Subscribe method to event until next scene change
        /// </summary>
        /// <param name="name">Event name</param>
        /// <param name="ev">Action to bind</param>
        /// <typeparam name="T">Event argument type</typeparam>
        public void Bind<T>(string name, Action<T> ev)
        {
            if (binds.ContainsKey(name))
                binds[name] = Delegate.Combine(binds[name],ev);
            else
                binds[name] = ev;
        }
  
        /// <summary>
        /// Subscribe method to event globally (Use in DDOL)
        /// </summary>
        /// <param name="name">Event name</param>
        /// <param name="ev">Action to bind</param>
        /// <typeparam name="T">Event argument type</typeparam>
        public void BindGlobal<T>(string name, Action<T> ev)
        {
            if (global_binds.ContainsKey(name))
                global_binds[name] = Delegate.Combine(global_binds[name],ev);
            else
                global_binds[name] = ev;
        }
        
        /// <summary>
        /// Unsubscribe method from event 
        /// </summary>
        /// <param name="name">Event name</param>
        /// <param name="ev">Action to unbind</param>
        /// <typeparam name="T">Event argument type</typeparam>
        public void Unbind<T>(string name, Action<T> ev)
        {
            if (binds.ContainsKey(name))
                binds[name] = Delegate.Remove(binds[name],ev);
        }
        
        /// <summary>
        /// Unsubscribe method from event globally (Use in DDOL)
        /// </summary>
        /// <param name="name">Event name</param>
        /// <param name="ev">Action to unbind</param>
        /// <typeparam name="T">Event argument type</typeparam>
        public void UnbindGlobal<T>(string name, Action<T> ev)
        {
            if (global_binds.ContainsKey(name)) 
                global_binds[name] = Delegate.Remove(global_binds[name],ev);
        }

        /// <summary>
        /// Invoke event with defined argument
        /// </summary>
        /// <param name="name">Event name</param>
        /// <param name="arg">Invocation argument</param>
        /// <typeparam name="T">Event argument type</typeparam>
        public void Invoke<T>(string name, T arg)
        {
            if (binds.ContainsKey(name))
                binds[name].DynamicInvoke(arg);
            if (global_binds.ContainsKey(name)) 
                global_binds[name].DynamicInvoke(arg);
        }
    }
}