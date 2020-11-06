using NSTools;
using UnityEngine;

public class ZBindActive : ZModelBind
{
    public bool defaultValue;
    public bool invert;

    protected override void SetValue(object obj)
    {
        var value = Model.Get(key, defaultValue);
        Log.Trace($"{this}: {key} = {value}",gameObject);
        gameObject.SetActive(value!=invert);
    }

}
