using NSTools.Controls;
using UnityEngine.EventSystems;

namespace NSTools.Controls
{
    public class ZButtonFSM : ZButtonAbstract
    {

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable)return;
            Log.Trace($"{this} FSM.Signal({key})", gameObject);
            Game.Fsm.Signal(key);
        }
    }
}