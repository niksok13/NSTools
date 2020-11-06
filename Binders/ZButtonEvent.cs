using NSTools;
using UnityEngine.EventSystems;

public class ZButtonEvent : ZClickable
{
    public string key;
    public string arg;
    public override void OnPointerClick(PointerEventData eventData)
    {
        Log.Trace($"{this}: {key} - {arg}",gameObject);
        EventManager.Invoke(key,arg);        
    }
}
