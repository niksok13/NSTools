namespace NSTools.Binders
{
    public class ZBindActive : ZBindAbstract
    {
        public bool defaultValue;
        public bool invert;

        protected override void SetValue(object obj)
        {
            var value = Game.Data.Get(key, defaultValue);
            Log.Trace($"{this}: {key} = {value}", gameObject);
            gameObject.SetActive(value != invert);
        }
    }
}