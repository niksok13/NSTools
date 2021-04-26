using NSTools.Controls;
using UnityEngine.EventSystems;

namespace NSTools.Controls
{
    public class ZButtonEvent : ZButtonAbstract
    {
        public string arg;
        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable)return;
            Log.Trace($"{this}: {key} - {arg}", gameObject);
            Game.Event.Invoke(key, arg);
        }
    }
}