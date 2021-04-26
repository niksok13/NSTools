using UnityEngine;

namespace NSTools.Binders
{
    public abstract class ZBindAbstract : MonoBehaviour
    {
        
        public string key;
        public bool global;
        void Start()
        {
            if (global)
                Game.Data.BindGlobal<object>(key,SetValue);
            else
                Game.Data.Bind<object>(key,SetValue);
        }

        protected abstract void SetValue(object arg);

        void OnDestroy()
        {
            if (global)
                Game.Data.UnbindGlobal<object>(key,SetValue);
            else
                Game.Data.Unbind<object>(key,SetValue);
        }
    }
}