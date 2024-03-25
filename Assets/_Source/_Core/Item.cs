using UnityEngine;
public class Item
{
    public string Name;
    public int Id;
    public string SpritePath;
    
    public Sprite Sprite;

    public override string ToString()
    {
        return $"{Name} (id: {Id})";
    }
}