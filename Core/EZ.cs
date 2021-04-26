using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NSTools
{

    public static class EZ
    {
        // Heavy task queue.
        private static Queue<Action> heavyQueue = new Queue<Action>();
        public static void Enqueue(Action task)
        {
            heavyQueue.Enqueue(task);   
        }

        
        // EZ Queue spawners.
        private static Action ezes, ezes_global;
        public static EZQueue Spawn(bool global = false)
        {
            var ez = new EZQueue();
            if (global)
                ezes_global += ez.Update;
            else
                ezes += ez.Update;
            return ez;
        }

        public static EZQueue SpawnGlobal()
        {
            return Spawn(true);
        }

        
        public class EZQueue
        {
            private class EZTask
            {
                public float duration;
                public Action<float> action;
            }
            
            private Queue<EZTask> actions = new Queue<EZTask>();
            private EZTask currentAction;
        
            private float timer;
            private bool isLooped;
            
            public void Kill()
            {
                ezes -= Update;
                ezes_global -= Update;
                actions.Clear();
            }
        
            public EZQueue Wait(Func<bool> a)
            {
                Add(float.MaxValue, t =>
                {
                    if (!a()) return;
                    timer = 0;
                    currentAction = null;
                }).Wait(0.3f);
                return this;
            }

            public EZQueue Add(float duration, Action<float> action)
            {
                actions.Enqueue(new EZTask{action = action, duration = duration});
                return this;
            }
            public EZQueue Add(Action<float> action) => Add(0.3f, action);
            public EZQueue Add(Action action) => Add(0, t => action());
            public EZQueue Wait(float duration = 0.1f) => Add(duration, t => { });
        
        
            public EZQueue Loop() => Add(() => isLooped = true);
            public void Unloop() => isLooped = false;
            
            
            public void Update()
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
                var delta = Time.deltaTime;
                if (timer < currentAction.duration)
                {
                    timer += delta;
                    currentAction.action(timer / currentAction.duration);
                    return;
                }
                currentAction.action(1);
                timer -= currentAction.duration;
                if (isLooped) 
                    actions.Enqueue(currentAction);
                currentAction = null;
            }
        }
        
        //Global EZ handler
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
        

        // --- Easing functions --- 
        // Usage: Lerp(from, to, EZ.QuadIn(t))
        
        //Quad
        public static float QuadIn(float t) => t * t;
        public static float QuadOut(float t) => 1 - (1 - t) * (1 - t);
        public static float QuadInOut(float t) => (1 - t) * QuadIn(t) + t * QuadOut(t);
        public static float QuadOutIn(float t) => t * QuadIn(t) + (1 - t) * QuadOut(t);


        //Cubic
        public static float CubicIn(float t) => t * t * t;
        public static float CubicOut(float t) => 1 - (1 - t) * (1 - t) * (1 - t);
        public static float CubicInOut(float t) => (1 - t) * CubicIn(t) + t * CubicOut(t);
        public static float CubicOutIn(float t) => t * CubicIn(t) + (1 - t) * CubicOut(t);

        //Back
        private static float b1 = 1.70158f;
        private static float b2 = 2.70158f;
        public static float BackIn(float t) => b2 * t * t * t - b1 * t * t;
        public static float BackOut(float t) => 1 - BackIn(1 - t);
    }
}