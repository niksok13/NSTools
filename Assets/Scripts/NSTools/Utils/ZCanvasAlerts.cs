using UnityEngine;
using UnityEngine.UI;

namespace NSTools.Utils
{
    public class ZCanvasAlerts : MonoBehaviour
    {
        public Text label;
        private string key = "show_alert";
        private Canvas cnv;
        private EZ.EZQueue ez;
        void Awake()
        {
            cnv = GetComponent<Canvas>();
            DontDestroyOnLoad(gameObject);
            Game.Event.BindGlobal<string>(key,InvokeMessage);
            cnv.enabled = false;
        }
    
        void InvokeMessage(object arg)
        {
            var str = (string) arg;
            label.text = str;
            cnv.enabled = true;
            var tf = (RectTransform)label.transform;
            tf.anchoredPosition = Vector3.zero;
            var transp = new Color(1, 1, 1, 0);
            ez?.Kill();
            ez = EZ.SpawnGlobal().Add(t =>
            {
                t = EZ.BackOut(t);
                tf.localScale = Vector3.one*t;
                label.color = Color.Lerp(transp, Color.white, t);

            }).Wait(0.3f).Add(t =>
            {
                t = EZ.QuadIn(1 - t);
                tf.localScale = Vector3.one*t;
                label.color = Color.Lerp(transp, Color.white, t);
            }).Add(() =>
            {
                cnv.enabled = false;
            });
        }

    }
}
