using System.Collections.Generic;
using System.Linq;
using NSTools.Utils;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using UnityEngine;

namespace NSTools
{
    public static class Log
    {
      
        enum LogLevel
        {
            Trace = 1,
            Debug = 2,
            Info = 3,
            Error = 4,
            Fatal = 5,
            Mute = 6
        }

#if UNITY_EDITOR
        private static LogLevel level = LogLevel.Debug;
        private static string template = "<color={0}>{1}:</color>    <b>{2}</b>";

#else
        private static string template = "{1}:    {2}";
        private static LogLevel level = LogLevel.Trace;

        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.None);
            Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
        }
#endif

        public static void Trace(object str, Object ctx = null)
        {
            if ((int)level > 1) return;
            UnityEngine.Debug.LogWarning(template.Format("cyan", "Trace", str), ctx);
        }

        public static void Debug(object str)
        {
            if ((int)level > 2) return;
            UnityEngine.Debug.Log(template.Format("blue", "Debug", str));
        }

        public static void Info(object str)
        {
            if ((int)level > 3) return;
            UnityEngine.Debug.Log(template.Format("green", "Info", str));
        }

        public static void Error(object str)
        {
            if ((int)level > 4) return;
            UnityEngine.Debug.Log(template.Format("red", "Error", str));
        }

        public static void Fatal(object str)
        {
            if ((int)level > 5) return;
            UnityEngine.Debug.Log(template.Format("purple", "Fatal", str));
        }
    }
}