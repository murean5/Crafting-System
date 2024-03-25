using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.EventSystems.PointerEventData;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Color highlightedColor;
    [SerializeField] private Image myItemImage;
    private Color _baseColor;

    private Image _myImage;

    public Item MyItem;
    public bool isResultCell;
    public bool isFull;

    private CellManager _cellManager;
    
    private Camera _mainCamera;

    private void Awake()
    {
        _myImage = GetComponent<Image>();
        _baseColor = _myImage.color;
        
        if (Camera.main is not null)
        {
            _mainCamera = Camera.main;
        }

        _cellManager = _mainCamera.GetComponent<CellManager>();
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
        if (isResultCell) return;
        
        _cellManager.selectedCell = this;
        
        if (eventData.button == InputButton.Right)
        {
            _cellManager.possibleItemsPanel.SetActive(!_cellManager.possibleItemsPanel.activeSelf);
        }
        else if (eventData.button == InputButton.Left)
        {
            _cellManager.possibleItemsPanel.SetActive(false);
        }
        else if (eventData.button == InputButton.Middle)
        {
            if (!isFull) return;
            
            Clear();
            _cellManager.UpdateCraftingTable();
        }
    }

    public void Clear()
    {
        isFull = false;
        MyItem = null;
        myItemImage.sprite = null;
        myItemImage.gameObject.SetActive(false);
    }
    
    public void AddItem(Item item)
    {
        MyItem = item;
        myItemImage.sprite = MyItem.Sprite;
        myItemImage.gameObject.SetActive(true);
        isFull = true;

        if (isResultCell) return;
        _cellManager.UpdateCraftingTable();
    }
}