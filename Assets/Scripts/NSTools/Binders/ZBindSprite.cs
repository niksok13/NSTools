using UnityEngine;
using UnityEngine.UI;

namespace NSTools.Binders
{
    public class ZBindSprite : ZBindAbstract
    {
        public Sprite defaultValue;
        private Image img;

        void Awake()
        {
            img = GetComponent<Image>();
        }

        protected override void SetValue(object obj)
        {
            var value = Game.Data.Get(key, defaultValue);
            Log.Trace($"{this}: {key} = {value}", gameObject);
            img.sprite = value;
        }
    }
}
