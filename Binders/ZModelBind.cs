using UnityEngine;

namespace NSTools
{
    public abstract class ZModelBind :MonoBehaviour
    {
        
        public string key;
        public bool global;
        void Awake()
        {
            if (global)
                Model.BindGlobal(key,SetValue);
            else
                Model.Bind(key,SetValue);
        }

        protected abstract void SetValue(object arg);
    }
}