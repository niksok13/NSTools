using UnityEngine;
using UnityEngine.EventSystems;

namespace NSTools.Controls
{
    public class ZButtonDataFlag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        public string key;
        public void OnPointerDown(PointerEventData eventData)
        {
            Log.Trace($"{this}: {key} = true", gameObject);
            Game.Data.Set(key, true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Log.Trace($"{this}: {key} = false", gameObject);
            Game.Data.Set(key, false);
        }

    }
}