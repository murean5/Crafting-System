using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color highlightedColor;
    private Color _baseColor;
    
    private Image _myImage;

    private void Awake()
    {
        _myImage = GetComponent<Image>();
        _baseColor = _myImage.color;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _myImage.color = highlightedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _myImage.color = _baseColor;
    }
}
