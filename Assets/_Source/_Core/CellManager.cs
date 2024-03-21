using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : MonoBehaviour
{
    [SerializeField] private List<Cell> craftingCells;
    private Dictionary<string, int> _nameDictionary;

    [SerializeField] private Transform possibleItemsScrollViewContent;
    [SerializeField] public GameObject possibleItemsPanel;
    
    [SerializeField] private GameObject itemPanelPrefab;
    
    public Cell editingCell;

    private List<Item> _possibleItems;

    private const string PathToNameDictionaryJson = "Dictionaries/nameDictionary";
    private const string PathToPossibleItemsJson = "Lists/possibleItems";

    private void Start()
    {
        _nameDictionary = LoadNameDictionary(PathToNameDictionaryJson);
        _possibleItems = LoadPossibleItems(PathToPossibleItemsJson);
        foreach (var item in _possibleItems)
        {
            AddItemToScrollViewContent(item);
        }

        foreach (var cell in craftingCells)
        {
            cell.isStorage = false;
            cell.isFull = false;
        }
    }

    private void AddItemToScrollViewContent(Item item)
    {
        var itemPanelInstance = Instantiate(itemPanelPrefab, possibleItemsScrollViewContent).transform;
        var possibleItem = itemPanelInstance.GetComponent<PossibleItem>();
        
        var icon = itemPanelInstance.Find("Icon").GetComponent<Image>();
        var label = itemPanelInstance.Find("Label").GetComponent<Text>();
        
        icon.sprite = Resources.Load<Sprite>(item.SpritePath);
        item.Sprite = icon.sprite;
        
        label.text = item.Name;
        possibleItem.Item = item;
        possibleItem.cellManager = this;
    }

    private static Dictionary<string, int> LoadNameDictionary(string path)
    {
        var json = Resources.Load<TextAsset>(path);
        return JsonConvert.DeserializeObject<Dictionary<string, int>>(json.text);
    }

    private static List<Item> LoadPossibleItems(string path)
    {
        var json = Resources.Load<TextAsset>(path);
        return JsonConvert.DeserializeObject<List<Item>>(json.text);
    }
}