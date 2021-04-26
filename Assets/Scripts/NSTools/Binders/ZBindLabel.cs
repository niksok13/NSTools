using UnityEngine.UI;

namespace NSTools.Binders
{
    public class ZBindLabel : ZBindAbstract
    {
        private Text _label;
        void Awake()
        {
            _label = GetComponent<Text>();
        }

        protected override void SetValue(object obj)
        {
            var value = Game.Data.Get(key, new object());
            Log.Trace($"{this}: {key} = {value}", gameObject);
            _label.text = value.ToString();
        }
    }
}