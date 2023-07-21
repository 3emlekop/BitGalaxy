using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Turret")]
public class TurretData : ItemData
{
    public bool isModuleApplied = false;
    [Header("Presets")]
    public GameObject bulletPrefab;
    public ModuleData module;

    [Header("Turret Properties")]
    [Range(0, 100)]
    public byte damage;

    [Range(0.1f, 10)]
    public float fireRate;

    [Range(0.1f, 10)]
    public float bulletSpeed;

    [Range(1, 10)]
    public byte shootsCount;

    public bool isBurstFire;

    public bool isSplash;

    [Range(0, 10)]
    public float splashRadius;

    public bool isAutoTaget;

    [Range(0, 1)]
    public float autoTargetForce;

    public bool isLaser;

    [Range(0, 1)]
    public float accuracy;

    [Range(0, 1)]
    public float critChance;

    public bool destroyOnCollision;

    public float destroyTime;

    public bool isTimeSpread;

    public TurretData ApplyModule()
    {
        if (module == null || isModuleApplied)
            return this;

        damage += module.damage;

        fireRate += module.fireRate;

        bulletSpeed += module.bulletSpeed;
        if (bulletSpeed < 0.1f) bulletSpeed= 0.1f;

        shootsCount += module.shootsCount;

        splashRadius += module.splashRadius;

        autoTargetForce += module.autoTargetForce;
        Mathf.Clamp01(autoTargetForce);

        accuracy += module.accuracy;
        Mathf.Clamp01(accuracy);

        critChance += module.critChance;
        Mathf.Clamp01(critChance);

        isModuleApplied = true;

        return this;
    }
}

