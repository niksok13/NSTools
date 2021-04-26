using UnityEngine;
using UnityEngine.UI;

namespace NSTools.Binders
{
    public class ZBindImageFill : ZBindAbstract
    {
        private Image rt;
    
        void Awake()
        {
            rt = GetComponent<Image>();
        }

        protected override void SetValue(object arg)
        {
            var value = Game.Data.Get(key, 1f);
            rt.fillAmount = value;
        }
    }
}
