using NSTools.Controls;
using UnityEngine.EventSystems;

namespace NSTools.Controls
{
    public class ZButtonDataToggle : ZButtonAbstract
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            var value = !Game.Data.Get(key, false);
            Log.Trace($"{this}: {key} = {value}", gameObject);
            Game.Data.Set(key, value);
        }
    }
}