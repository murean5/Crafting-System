using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static UnityEngine.EventSystems.PointerEventData;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Color highlightedColor;
    [SerializeField] private Image myItemImage;
    private Color _baseColor;

    private Image _myImage;

    public Item MyItem;
    public bool isStorage;
    public bool isFull;

    private CellManager _cellManager;

    private void Awake()
    {
        _myImage = GetComponent<Image>();
        _baseColor = _myImage.color;
        _cellManager = GetComponentInParent<CellManager>();
        if (!isFull)
        {
            myItemImage.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _myImage.color = highlightedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _myImage.color = _baseColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == InputButton.Right)
        {
            _cellManager.editingCell = this;
            _cellManager.possibleItemsPanel.SetActive(!_cellManager.possibleItemsPanel.activeSelf);
        }
        else if (eventData.button == InputButton.Left)
        {
            _cellManager.possibleItemsPanel.SetActive(false);
        }
    }

    public void AddPossibleItemToMe(PossibleItem possibleItem)
    {
        MyItem = possibleItem.Item;
        myItemImage.sprite = MyItem.Sprite;
        myItemImage.gameObject.SetActive(true);
        isFull = true;
        print($"Added {possibleItem.Item} to {name}");
    }
}