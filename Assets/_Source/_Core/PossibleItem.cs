using UnityEngine;
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
}
