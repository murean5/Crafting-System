using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

internal struct RecipeData 
{
    public string Recipe;
    public int ResultItemId;
}

public class CellManager : MonoBehaviour
{
    [SerializeField] private List<Cell> craftingCells;

    [SerializeField] private Transform possibleItemsScrollViewContent;
    [SerializeField] public GameObject possibleItemsPanel;
    private List<Item> _possibleItems;
    
    [SerializeField] private GameObject itemPanelPrefab;

    public Cell selectedCell;
    [SerializeField] public Cell resultCell;

    private const string PathToPossibleItemsJson = "Lists/possibleItems";
    private const string PathToRecipesJson = "Lists/recipes";

    private List<RecipeData> _craftingRecipes;

    private void Start()
    {
        _possibleItems = LoadPossibleItems(PathToPossibleItemsJson);

        foreach (var item in _possibleItems)
        {
            AddItemToScrollViewContent(item);
        }

        foreach (var cell in craftingCells)
        {
            cell.isResultCell = false;
            cell.isFull = false;
        }

        resultCell.isResultCell = true;
        resultCell.isFull = false;

        _craftingRecipes = LoadRecipes(PathToRecipesJson);
    }

    public void UpdateCraftingTable()
    {
        List<int> idsInTheTable = new();
        foreach (var cell in craftingCells)
        {
            if (cell.isFull)
            {
                idsInTheTable.Add(cell.MyItem.Id);
            }
            else
            {
                idsInTheTable.Add(-1);
            }
        }

        var recipeInLine = string.Join(' ', idsInTheTable);

        var foundCorrectRecipe = false;
        foreach (var recipe in _craftingRecipes)
        {
            if (recipe.Recipe == recipeInLine)
            {
                resultCell.AddItem(_possibleItems.Find(item => item.Id == recipe.ResultItemId));
                foundCorrectRecipe = true;
                break;
            }
        }

        if (!foundCorrectRecipe && resultCell.isFull)
        {
            resultCell.Clear();
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

    private static List<Item> LoadPossibleItems(string path)
    {
        var json = Resources.Load<TextAsset>(path);
        return JsonConvert.DeserializeObject<List<Item>>(json.text);
    }

    private static List<RecipeData> LoadRecipes(string path)
    {
        var json = Resources.Load<TextAsset>(path);
        return JsonConvert.DeserializeObject<List<RecipeData>>(json.text);
    }
}