using UnityEngine;
using UnityEngine.UI;

namespace NSTools.Utils
{
    public class ZCanvasLoader : MonoBehaviour
    { 
        public Image fg, bg;
        private bool curstate = true;
        private EZ.EZQueue ez;
        private Canvas cv;
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            cv = GetComponent<Canvas>();
            Game.Data.BindGlobal<bool>("loader_visible",SetVisible);
        }

        private void SetVisible(bool value)
        {
            var newstate = Game.Data.Get("loader_visible",false);
            if (curstate == newstate)return;
            curstate = newstate;
            ez?.Kill();
            var circle_from = fg.color;
            var fader_from = bg.color;
            cv.enabled = true;
            gameObject.SetActive(true);
            if (newstate)
            {
                ez = EZ.SpawnGlobal().Add(0.1f,pt =>
                {
                    fg.color = Color.Lerp(circle_from, Color.black, pt);
                    bg.color = Color.Lerp(fader_from, Color.white, pt);
                });
            }else{
                ez = EZ.SpawnGlobal().Add(0.1f,pt=>{
                    fg.color = Color.Lerp(circle_from,Color.clear,pt);
                    bg.color = Color.Lerp(fader_from,Color.clear,pt);
                }).Add(() =>
                {
                    cv.enabled = false;
                });
            }
        }
    }
}
