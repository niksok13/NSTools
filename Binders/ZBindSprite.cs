using NSTools;
using UnityEngine;
using UnityEngine.UI;

public class ZBindSprite : ZModelBind
{
    public Sprite defaultValue;
    private Image img;
    
    void Start()
    {
        img = GetComponent<Image>();
    }

    protected override void SetValue(object obj)
    {
        var value = Model.Get(key, defaultValue);
        Log.Trace($"{this}: {key} = {value}",gameObject);
        img.sprite = value;
    }

}
