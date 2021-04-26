using UnityEngine;
using UnityEngine.UI;

namespace NSTools.Binders
{

    [RequireComponent(typeof(Image))]
    public class ZBindImageColor : ZBindAbstract
    {
        public Color defaultValue;
        private Image img;

        void Awake()
        {
            img = GetComponent<Image>();
        }

        protected override void SetValue(object obj)
        {
            var value = Game.Data.Get(key, defaultValue);
            Log.Trace($"{this}: {key} = {value}", gameObject);
            img.color = value;
        }

    }
}