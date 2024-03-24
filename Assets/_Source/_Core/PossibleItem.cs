using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PossibleItem : MonoBehaviour
{
    [SerializeField] private Button myButton;
    public Item Item;
    public CellManager cellManager;
    private void Awake()
    {
        myButton.onClick.AddListener(ChooseMeFromPanel);
    }

    private void ChooseMeFromPanel()
    {
        cellManager.selectedCell.AddItem(Item);
        cellManager.possibleItemsPanel.SetActive(false);
    }
    
    private void ChooseMeAsCraftResult()
    {
        cellManager.resultCell.AddItem(Item);
    }
}
