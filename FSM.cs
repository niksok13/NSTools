using System.Collections.Generic;
using UnityEngine;

namespace NSTools
{
    public static class FSM
    {
        public class State
        {
            public virtual string Enter() => "";
            public virtual string Exit() => "";
            public virtual string Signal(string name, object arg) => "";
        }

        private static Dictionary<string,State> _states;
        private static State _current;

        static FSM()
        {
            _states = new Dictionary<string, State>();
        }

        public static void Add<T>(string name) where T : new()
        {
            _states[name] = new T() as State;
        }

        public static void Go(string name)
        {
            if (!_states.ContainsKey(name)) return;
            Log.Info($"FSM:Go({name})");
            
            var result = _current?.Exit();
            if (result!=null && _states.ContainsKey(result)) {
                Go(result);
                return;                
            }

            _current = _states[name];
            
            result = _current.Enter();
            if (_states.ContainsKey(result)) 
                Go(result);
        }

        public static void Signal(string name, object arg = null)
        {
            Log.Trace($"FSM.Signal({name}, {arg}",Camera.main);
            var result = _current?.Signal(name, arg);
            if (_states.ContainsKey(result)) Go(result);
        }
    }
}