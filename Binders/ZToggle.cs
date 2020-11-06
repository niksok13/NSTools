using NSTools;
using UnityEngine.EventSystems;

public class ZToggle : ZClickable
{
    public string key;
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        var value = !Model.Get(key, false);
        Log.Trace($"{this}: {key} = {value}",gameObject);
        Model.Set(key,value);      
    }
}
