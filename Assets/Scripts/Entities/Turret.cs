using UnityEngine;

public enum Direction
{
    Up = 1,
    Down = -1
}


public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject ship;
    [SerializeField] private SpriteRenderer outlineSprite;
    [SerializeField] private ShootAnimator animator;
    [SerializeField] public Direction aimDirection;
    [SerializeField] private bool isStatic;
    [SerializeField] private float shootTimeOffset;

    protected Transform turretTransform;
    protected TurretData data;

    private Vector3 direction;
    private ObjectPooling poolInstance;
    private string turretPoolName;

    private Timer burstTimer;
    private Bullet bullet;

    private void Awake()
    {
        turretTransform = transform;
        poolInstance = ObjectPooling.Instance;
    }

    /// <summary>
    /// Sets turret's static state. If it is, turret can't shoot and create bullet pools.
    /// </summary>
    /// <param name="state"></param>
    public void SetState(bool state)
    {
        isStatic = !state;
    }

    public void Shoot()
    {
        if (data.isBurstFire)
        {
            burstTimer.SetIterarions(data.shootsCount);
            burstTimer.Enable();
        }
        else
        {
            if (data.shootsCount == 1)
            {
                SpawnBullet();
                return;
            }
            else
            {
                for (byte i = 0; i < data.shootsCount; i++)
                {
                    direction.x = -0.25f + (i * 0.6f / (data.shootsCount));
                    direction.y = (sbyte)aimDirection;
                    SpawnBullet(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f * (sbyte)aimDirection);
                }
            }
        }
    }

    private void SpawnBullet()
    {
        SpawnBullet(0);
    }

    private void SpawnBullet(float rotation)
    {
        poolInstance.SpawnFromPool(turretPoolName, turretTransform.position, rotation);
        Aim(turretTransform.position + (Vector3.up * (sbyte)aimDirection));
        animator.Shoot(aimDirection);
    }

    public void SetData(TurretData data)
    {
        if (data == null)
        {
            this.data = null;
            GetComponent<SpriteRenderer>().sprite = null;
            outlineSprite.sprite = null;
            return;
        }

        this.data = data.ApplyModule();
        if (this.data.bulletSpeed < 1)
            this.data.bulletSpeed = 1;

        GetComponent<SpriteRenderer>().sprite = data.sprite;
        outlineSprite.sprite = data.sprite;

        if (data.module == null)
            outlineSprite.color = data.outlineColor;
        else
            outlineSprite.color = data.module.outlineColor;

        if (isStatic)
            return;

        CreateBulletPool();

        Timer.Create(turretTransform, CreateTimer, data.isTimeSpread ? shootTimeOffset : 0, false);

        if (data.isBurstFire)
        {
            burstTimer = Timer.Create(turretTransform, SpawnBullet, 0.15f, data.shootsCount);
            burstTimer.Disable();
        }
    }

    public TurretData GetData() { return data; }

    protected void Aim(Vector3 point)
    {
        direction = point - turretTransform.position;
        turretTransform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x)
            * Mathf.Rad2Deg - 90f, Vector3.forward);
    }

    public void CreateBulletPool()
    {
        turretPoolName = ship.name.Remove(1, 4) + gameObject.name.Remove(0, 6) + data.name;

        if (poolInstance.Exists(turretPoolName))
            return;

        GameObject bulletsParent = new GameObject(turretPoolName);
        bullet = ApplyBulletStats();

        poolInstance.AddPool(turretPoolName, bullet.gameObject, (byte)(Mathf.CeilToInt(1 + data.fireRate * data.shootsCount * 10 * data.destroyTime / data.bulletSpeed)), bulletsParent.transform);
    }

    private Bullet ApplyBulletStats()
    {
        Bullet bullet = data.bulletPrefab.GetComponent<Bullet>();
        bullet.SetDirection(aimDirection);
        bullet.SetIgnoreTag(ship.tag);
        bullet.SetData(data);
        bullet.SetShipsParent(ship.transform.parent);
        return bullet;
    }

    private void CreateTimer()
    {
        Timer.Create(turretTransform, Shoot, (1 / data.fireRate), true);
    }
}