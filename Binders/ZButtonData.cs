using NSTools;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZButtonData : ZClickable
{
    public string key;
    public bool value;
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        Log.Trace($"{this}: {key} = {value}",gameObject);
        Model.Set(key,value);      
    }
}
