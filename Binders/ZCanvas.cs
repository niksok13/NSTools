using NSTools;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
[RequireComponent(typeof(GraphicRaycaster))]
public class ZCanvas : MonoBehaviour
{
    public Transform window;
    public string fieldName;
    public Vector2 offscreen = Vector2.up*1000;
    public bool global = false;
    private Image _fader;
    private Canvas _canvas;
    private bool current;
    private EZ ez;
    public bool defaultState;

    private CanvasGroup cg;
    public MonoBehaviour listener;
    
    void Awake()
    {
        _fader = GetComponent<Image>();
        _canvas = GetComponent<Canvas>();
        
        cg = GetComponent<CanvasGroup>();
        if (global)
        {
            DontDestroyOnLoad(this);
            Model.BindGlobal(fieldName, SetActive);
        }
        else
            Model.Bind(fieldName, SetActive);

        _canvas.enabled = defaultState;
        window.localPosition = defaultState?Vector3.zero : (Vector3)offscreen;
    }


    void SetActive(object obj) {
        var value  = Model.Get(fieldName, defaultState);
        Log.Trace($"{this}: {fieldName} = {value}",gameObject);
        if (value == current) return;
        current = value;
        ez?.Clear();
        _canvas.enabled = true;
        var pos_from = window.localPosition;
        if (value)
        {
            ez = EZ.Spawn(global).Add(t =>
            {
                cg.alpha = t;
                _fader.color = new Color(0, 0, 0, t * 0.7f);
                window.localPosition = Vector3.Lerp(pos_from, Vector3.zero, EZ.CubicOut(t));
            });
        }
        else
        {
            ez = EZ.Spawn(global).Add(t =>
            {
                cg.alpha = 1-t;
                _fader.color = new Color(0,0,0,(1-t)*0.7f);
                window.localPosition = Vector3.Lerp(pos_from, offscreen, EZ.CubicIn(t));
            }).Add(() =>
            {
                _fader.color = new Color(0,0,0,0);
                _canvas.enabled = false;
                listener?.SendMessage("ResetScroll");
            });
            
        }
    }

}
