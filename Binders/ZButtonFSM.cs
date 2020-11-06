using NSTools;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZButtonFSM : ZClickable
{
    public string ev;
    public string arg;
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        Log.Trace($"{this} FSM.Signal({ev}, {arg})",gameObject);
        FSM.Signal(ev, arg);    
    }
}
