using System;
using UnityEngine;

public enum Grade
{
    F, E, D, C, B, A
}

[System.Serializable]
public class TurretData : ItemData
{
    [SerializeField] private int damage;
    [SerializeField] private float fireRate;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int shootsCount;
    [SerializeField] [Range(0, 100)] private int critChance;
    [SerializeField] private float splashRadius;
    [SerializeField] private bool isBurstFire;
    [SerializeField] private bool isTimeSpread;
    [SerializeField] private Grade grade;
    [SerializeField] private string moduleName;
    [SerializeField] private string projectileName;

    public int Damage => damage;
    public float FireRate => fireRate;
    public float BulletSpeed => bulletSpeed;
    public int ShootsCount => shootsCount;
    public int CritChance => critChance;
    public float SplashRadius => splashRadius;
    public bool IsBurstFire => isBurstFire;
    public bool IsTimeSpread => isTimeSpread;
    public Grade Grade => grade;
    public ModuleData Module => ResourceManager.instance.GetModule(moduleName);
    public Projectile Projectile => ResourceManager.instance.GetProjectile(projectileName);
}
