using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NSTools.Controls
{
    public class ZButtonUrl : MonoBehaviour, IPointerClickHandler
    {
        public string url;
        public void OnPointerClick(PointerEventData eventData)
        {
            Application.OpenURL(string.IsNullOrEmpty(url) ? GetComponent<Text>().text.Trim() : url);
        }
    }
}