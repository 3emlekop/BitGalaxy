using UnityEngine;

[RequireComponent(typeof(ShootAnimator))]
public class Turret : Item
{
    [SerializeField] public bool isActivated = true;
    [SerializeField] private GameObject ownerShipGameObject;
    [SerializeField] private float shootTimeOffset;

    private TurretData turretData;
    private ShootAnimator shootAnimator;
    private Projectile projectile;
    private Vector2 aimDirection;
    private Vector2 shootDirection;
    private string turretPoolName;
    private Timer burstTimer;

    new private void Awake()
    {
        base.Awake();
        shootAnimator = GetComponent<ShootAnimator>();
    }

    public void PrepareToShoot()
    {
        if (IsActive == false)
            return;

        Aim(aimDirection);

        if (turretData.ShootsCount == 1)
        {
            Shoot();
            return;
        }

        if (turretData.IsBurstFire)
        {
            burstTimer.SetIterations((byte)turretData.ShootsCount);
            burstTimer.Enable();
        }
        else
        {
            for (byte i = 0; i < turretData.ShootsCount; i++)
            {
                shootDirection.x = aimDirection.x - 0.25f + (i * 0.6f / turretData.ShootsCount);
                shootDirection.y = aimDirection.y;
                Shoot(Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg - 90f);
            }
        }
    }

    private void Shoot()
    {
        Shoot(0);
    }

    private void Shoot(float rotation)
    {
        ObjectPooling.Instance.SpawnFromPool(turretPoolName, itemTransform.position, rotation);
        shootAnimator.Animate(aimDirection);
    }

    public void SetData(TurretData data)
    {
        base.SetData(data);
        if (data == null)
        {
            turretData = null;
            return;
        }

        turretData = data;
        GetComponent<SpriteRenderer>().sprite = data.Sprite;
        outlineSpriteRenderer.sprite = data.Sprite;

        if (data.Module == null)
            outlineSpriteRenderer.color = data.RarityColor;
        else
            outlineSpriteRenderer.color = data.Module.ModuleColor;

        if(isActivated == false)
            return;

        CreateBulletPool();

        Timer.Create(itemTransform, CreateShootTimer, data.IsTimeSpread ? shootTimeOffset : 0, false);
        if (data.IsBurstFire)
        {
            burstTimer = Timer.Create(itemTransform, Shoot, 0.15f, (byte)data.ShootsCount);
            burstTimer.Disable();
        }
    }

    public void Aim(Vector2 direction)
    {
        itemTransform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f, Vector2.up);
    }

    private void CreateBulletPool()
    {
        string shipFirstSign = ownerShipGameObject.name.Remove(1);
        string shipIndex = ownerShipGameObject.name.Remove(0, ownerShipGameObject.name.Length - 2);
        turretPoolName = shipFirstSign + shipIndex + turretData.Name;

        if (ObjectPooling.Instance.Exists(turretPoolName))
            return;

        GameObject projectilesParent = new GameObject(turretPoolName);
        projectile = ApplyProjectileStats();

        var destroyTime = projectile is Bullet bullet ? bullet.destroyTime : 1;
        ObjectPooling.Instance.AddPool(turretPoolName, projectile.gameObject,
            (byte)Mathf.CeilToInt(1 + turretData.FireRate * turretData.ShootsCount * 10 * destroyTime / turretData.BulletSpeed),
            projectilesParent.transform);
    }

    private Projectile ApplyProjectileStats()
    {
        return turretData.Projectile.SetData(aimDirection, ownerShipGameObject.transform, turretData);
    }

    private void CreateShootTimer()
    {
        Timer.Create(itemTransform, Shoot, 1 / turretData.FireRate, true);
    }
}