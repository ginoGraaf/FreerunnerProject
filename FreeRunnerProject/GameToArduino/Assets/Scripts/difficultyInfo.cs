using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class difficultyInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text textbox;
    public string text;


    public void OnPointerEnter(PointerEventData eventData)
    {
        textbox.enabled = true;
        textbox.text = text;
        //transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textbox.enabled = false;
    }
}
