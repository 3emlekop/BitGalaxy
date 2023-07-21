using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Module")]
public class ModuleData : ItemData
{
    [Header("Modificators")]

    public byte damage;

    [Range(-10f, 10f)]
    public float fireRate;

    public float bulletSpeed;

    [Range(-5, 5)]
    public byte shootsCount;

    public float splashRadius;

    [Range(-1f, 1f)]
    public float autoTargetForce;

    [Range(-1f, 1f)]
    public float accuracy;

    [Range(-1f, 1f)]
    public float critChance;
}
