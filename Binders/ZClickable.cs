using UnityEngine;
using UnityEngine.EventSystems;

namespace NSTools
{
    public abstract class ZClickable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        private CanvasRenderer mat;
        public void Awake()
        {
            mat = GetComponent<CanvasRenderer>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            mat.SetColor(Color.gray);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            mat.SetColor(Color.white);
        }

        public abstract void OnPointerClick(PointerEventData eventData);
    }
}