using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NSTools.Utils
{
    public static class NSHelpers
    {
        public static T GetRandomItem<T>(this ICollection<T> list)
        {
            var index = Random.Range(0, list.Count);
            return list.ElementAtOrDefault(index);
        }
        public static string Format(this string str, params object[] args) => string.Format(str, args);
        
        public static HashSet<string> msg_win = new HashSet<string>(new[]
        {
            "Tremendous!", "Wicked!", "You Rock!", "Champion!", "Insane!",
            "Wonderful!", "Splendid!", "Well done!", "Awesome!", "Marvellous!", 
            "Lovely!", "Super!", "Great!", "Glorious!", "Fantastic!", "Smashing!",
            "Fantabulous!", "Gorgeous!", "Heavenly!", "Incredible!", "Sensational!",
            "Cool!", "Nice!", "OK!", "Good!", "You are good...",
            "Amazing!", "Perfect!", "Magnificent!", "Excellent!",        
        });
    }
}