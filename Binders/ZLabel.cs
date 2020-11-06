using System.Collections;
using System.Collections.Generic;
using NSTools;
using UnityEngine;
using UnityEngine.UI;

public class ZLabel : MonoBehaviour
{
    public string title;
    public bool global;
    private Text _label;
    void Start()
    {
        _label = GetComponent<Text>();
        
        if(global)
            NSTools.Model.BindGlobal(title, UpdateLabel);
        else
            NSTools.Model.Bind(title, UpdateLabel);
        
    }

    private void UpdateLabel(object obj)
    {
        var value = Model.Get<object>(title,null);

        Log.Trace($"{this}: {title} = {value}",gameObject);  
        _label.text = value==null?title:value.ToString();
    }
}
