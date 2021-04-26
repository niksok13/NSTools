using UnityEngine;
using UnityEngine.EventSystems;

namespace NSTools.Controls
{

    public abstract class ZButtonAbstract : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public string key;


        private CanvasRenderer mat;
        protected bool interactable = true;

        public void Awake()
        {
            mat = GetComponent<CanvasRenderer>();
            Game.Data.Bind<bool>($"btn_{key}_active", SetActive);
        }

        private void SetActive(bool value)
        {
            interactable = Game.Data.Get($"btn_{key}_active",true);
            var color = interactable ? Color.white : Color.gray / 2;
            mat.SetColor(color);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!interactable)return;
            mat.SetColor(Color.gray);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!interactable)return;
            mat.SetColor(Color.white);
        }

        public abstract void OnPointerClick(PointerEventData eventData);
    }
}