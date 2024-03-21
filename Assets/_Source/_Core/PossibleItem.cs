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
        myButton.onClick.AddListener(ChooseMe);
    }

    private void ChooseMe()
    {
        cellManager.editingCell.AddPossibleItemToMe(this);
        cellManager.possibleItemsPanel.SetActive(false);
    }
}
