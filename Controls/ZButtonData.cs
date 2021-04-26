using NSTools.Controls;
using UnityEngine.EventSystems;

namespace NSTools.Controls
{
    public class ZButtonData : ZButtonAbstract
    {
        public bool value;

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable)return;
            Log.Trace($"{this}: {key} = {value}", gameObject);
            Game.Data.Set(key, value);
        }
    }
}