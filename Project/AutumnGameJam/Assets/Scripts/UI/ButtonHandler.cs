using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TMP_Text _highlightedText;
    [SerializeField] Color _baseColor;
    [SerializeField] Color highlightedColor;
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        _highlightedText.color = highlightedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _highlightedText.color = Color.black;
    }
}
