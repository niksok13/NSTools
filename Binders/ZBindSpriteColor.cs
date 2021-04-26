using UnityEngine;

namespace NSTools.Binders
{
    public class ZBindSpriteColor : ZBindAbstract
    {
        public Color defaultValue;
        private SpriteRenderer img;

        void Awake()
        {
            img = GetComponent<SpriteRenderer>();
        }

        protected override void SetValue(object obj)
        {
            var value = Game.Data.Get(key, defaultValue);
            Log.Trace($"{this}: {key} = {value}", gameObject);
            img.color = value;
        }

    }
}
