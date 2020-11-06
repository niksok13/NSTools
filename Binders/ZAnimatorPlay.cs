using System.Collections;
using System.Collections.Generic;
using NSTools;
using UnityEngine;
using UnityEngine.UI;

public class ZAnimatorPlay : MonoBehaviour
{
    public string eventName;
    public string state;
    public bool global;
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
        if(global)
            EventManager.BindGlobal(eventName, UpdateLabel);
        else
            EventManager.Bind(eventName, UpdateLabel);
        
    }

    private void UpdateLabel(object args)
    {

        Log.Trace($"{this}: {eventName} = {state}",gameObject);  
        _anim.Play(state);
    }
}