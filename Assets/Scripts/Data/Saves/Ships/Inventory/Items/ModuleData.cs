using UnityEngine;
using System;

[Serializable]
public class ModuleData : ItemData
{
    private enum ModuleType
    {
        first, second, third
    }

    [SerializeField] private ModuleType moduleType;
    [SerializeField] private int damageModifier;
    [SerializeField] private float fireRateModifier;
    [SerializeField] private float bulletSpeedModifier;
    [SerializeField] private int shootsCountModifier;
    [SerializeField] [Range(0, 100)] private int critChanceModifier;
    [SerializeField] private float splashRadiusModifier;

    public Color ModuleColor
    {
        get
        {
            switch (moduleType)
            {
                case ModuleType.first:
                    return new Color(0.3f, 0.6f, 1f);
                case ModuleType.second:
                    return new Color(0.6f, 0.3f, 1f);
                case ModuleType.third:
                    return new Color(1f, 0.3f, 0.7f);
                default:
                    return Color.white;
            }
        }
    }
    public int DamageModifier => damageModifier;
    public float FireRateModifier => fireRateModifier;
    public float BulletSpeedModifier => bulletSpeedModifier;
    public int ShootsCounteModifier => shootsCountModifier;
    public int CritChanceModifier => critChanceModifier;
    public float SplashRadiusModifier => splashRadiusModifier;
    new public ItemType ItemType => ItemType.Modules;
    new public Sprite Sprite
    {
        get
        {
            return ResourceManager.instance.GetSprite(Enum.GetName(typeof(ItemType), ItemType), "Module" + moduleType);
        }
    }
}
