using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NSTools
{

    public class EZ
    {
        private static Action ezes, ezes_global;
        private static Queue<Action> heavyQueue = new Queue<Action>();
        private Queue<Tuple<float,Action<float>>> actions;
        private Tuple<float,Action<float>> currentAction;
        private float timer;
        private bool isLooped;
        
        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            SceneManager.sceneUnloaded += s =>  ezes = null;  
            var go = new GameObject("EZRunner");
            go.AddComponent<EZRunner>();
            UnityEngine.Object.DontDestroyOnLoad(go);
        }
        
        public class EZRunner : MonoBehaviour
        {
            void Update()
            {
                if (heavyQueue.Count > 0)
                {
                    var task = heavyQueue.Dequeue();
                    task.Invoke();
                }
                ezes?.Invoke();
                ezes_global?.Invoke();
            }
        }

        public static void Enqueue(Action task)
        {
            heavyQueue.Enqueue(task);   
        }
        
        private void Update()
        {
            if (currentAction == null)
            {
                if (actions.Count == 0)
                {
                    ezes -= Update;
                    ezes_global -= Update;
                    return;
                }
                currentAction = actions.Dequeue();
            }
            var delta = Time.smoothDeltaTime;
            timer += delta;
            if (timer < currentAction.Item1)
            {
                currentAction.Item2(timer / currentAction.Item1);
                return;
            }
            currentAction.Item2(1);
            timer -= currentAction.Item1;
            timer -= delta;
            if (isLooped) 
                actions.Enqueue(currentAction);
            currentAction = null;
            Update();
        }
        
       

        public static EZ Spawn(bool global = false)
        {
            var ez = new EZ();
            ez.Clear();
            if (global)
                ezes_global += ez.Update;
            else
                ezes += ez.Update;
            return ez;
        }

        public static EZ SpawnGlobal()
        {
            return Spawn(true);
        }


        public EZ Clear()
        {
            actions = new Queue<Tuple<float,Action<float>>>();
            currentAction = null;
            timer = 0;
            isLooped = false;
            return this;
        }
        

        public EZ Add(float duration, Action<float> action)
        {
            actions.Enqueue(new Tuple<float, Action<float>>(duration,action));
            return this;
        }
        public EZ Add(Action<float> action)
        {
            actions.Enqueue(new Tuple<float, Action<float>>(0.3f,action));
            return this;
        }

        public EZ Add(float d, Action action) => Wait(d).Add(() => action());
        public EZ Add(Action action) => Add(0, t => action());
        public EZ Wait(float duration = 0.1f) => Add(duration, t => { });
        
        public EZ Loop() => Add(() => isLooped = true);
        
        // --- Easing functions --- 
        // Usage: Lerp(from, to, EZ.QuadIn(t))
        public static float QuadIn(float t) => t * t;
        public static float QuadOut(float t)
        {
            var a = 1 - t;
            return 1 - a * a;
        }
        public static float CubicIn(float t) => t * t * t;
        public static float CubicOut(float t)
        {
            var a = 1 - t;
            return 1 - a * a * a;
        }
    }
}