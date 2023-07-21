using UnityEngine;

public class ItemData : ScriptableObject
{
    [Header("Main Properties")]
    new public string name;
    public string description;
    public int price;
    public Sprite sprite;
    public Color outlineColor;
}
