using UnityEngine;
using UnityEngine.UI;

namespace NSTools.Binders
{
    [RequireComponent(typeof(CanvasScaler))]
    [RequireComponent(typeof(GraphicRaycaster))]
    [RequireComponent(typeof(Image))]
    public class ZBindCanvas : MonoBehaviour
    {
        public Transform window;
        public string fieldName;
        public Vector2 offscreen = Vector2.up * 1000;
        public bool global = false;
        private Image _fader;
        private Canvas _canvas;
        private bool current;
        private EZ.EZQueue ez;
        public bool defaultState;
        
        void Awake()
        {
            if (global)
            {
                DontDestroyOnLoad(this);
                Game.Data.BindGlobal<bool>(fieldName, SetActive);
            }
            else
                Game.Data.Bind<bool>(fieldName, SetActive);

            _fader = GetComponent<Image>();
            _canvas = GetComponent<Canvas>();
            window.localPosition = defaultState ? Vector3.zero : (Vector3)offscreen;
            _canvas.enabled = defaultState;
        }

        void SetActive(bool value = false)
        {
            Log.Trace($"{this}: {fieldName} = {value}", gameObject);
            if (value == current) return;
            current = value;
            ez?.Kill();
            _canvas.enabled = true;
            var pos_from = window.localPosition;

            _fader.color = new Color(0, 0, 0, value ? 0 : 0.7f);
            if (value)
            {
                ez = EZ.Spawn(global).Add(t =>
                {
                    t = EZ.BackOut(t);
                    var c = _fader.color;
                    c.a = t * 0.7f;
                    _fader.color = c;
                    window.localPosition = Vector3.LerpUnclamped(pos_from, Vector3.zero, t);
                });
            }
            else
            {
                ez = EZ.Spawn(global).Add(t =>
                {
                    var c = _fader.color;
                    c.a = (1 - t) * 0.7f;
                    _fader.color = c;
                    window.localPosition = Vector3.LerpUnclamped(pos_from, offscreen, t);
                }).Add(() =>
                {
                    _fader.color = new Color(0, 0, 0, 0);
                    _canvas.enabled = false;
                });
            }
        }
    }
}
