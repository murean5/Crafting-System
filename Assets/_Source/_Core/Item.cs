using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
public class Item
{
    public string Name;
    public int Id;
    public int Quantity;
    public int MaxQuantity;
    public string SpritePath;
    
    [JsonIgnore]
    public Sprite Sprite;

    protected Item()
    {
        Name = string.Empty;
        Id = -1;
        Quantity = -1;
        MaxQuantity = -1;
        SpritePath = string.Empty;
    }

    protected Item(string name, int id, int quantity, int maxQuantity, string spritePath)
    {
        Name = name;
        Id = id;
        Quantity = quantity;
        MaxQuantity = maxQuantity;
        SpritePath = spritePath;
    }

    public override string ToString()
    {
        return $"{Name} (id: {Id})";
    }
}