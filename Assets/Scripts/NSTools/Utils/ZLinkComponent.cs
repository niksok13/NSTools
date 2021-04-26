using UnityEngine;

namespace NSTools.Utils
{
    public class ZLinkComponent : MonoBehaviour
    {
        public string key;
        public Component value;
        
        void Awake()
        {
            Game.Data.Set(key,value);
        }
    }
}
