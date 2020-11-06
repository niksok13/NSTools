using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ZLinkLabel : MonoBehaviour, IPointerClickHandler
{
    public string url;
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.OpenURL(string.IsNullOrEmpty(url)?GetComponent<Text>().text.Trim():url);
    }
}
