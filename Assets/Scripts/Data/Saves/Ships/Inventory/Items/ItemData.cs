using System;
using UnityEngine;

public enum Rarity
{
    common, uncommon, rare, mythic, legendary
}

public abstract class ItemData
{
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private int price;
    [SerializeField] private Rarity rarity;
    [NonSerialized] public Sprite sprite;

    public string Name => name;
    public string Description => description;
    public int Price => price;
    public Color RarityColor
    {
        get
        {
            switch (rarity)
            {
                case Rarity.common:
                    return new Color(0.66f, 1f, 0.54f);
                case Rarity.uncommon:
                    return new Color(0.54f, 0.75f, 1f);
                case Rarity.rare:
                    return new Color(0.54f, 1f, 0.95f);
                case Rarity.mythic:
                    return new Color(0.83f, 0.54f, 1f);
                case Rarity.legendary:
                    return new Color(1f, 0.54f, 0.54f);
                default:
                    return Color.white;
            }
        }
    }
    public Sprite Sprite => sprite;
}
