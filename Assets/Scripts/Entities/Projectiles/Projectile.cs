using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource shootAudioSource;

    public Transform Owner { get; private set; }
    public string IgnoreTag { get { return Owner.tag; } }
    protected TurretData TurretData { get; private set; }
    protected Vector2 aimDirection { get; private set; }

    protected Transform ProjectileTransform;
    protected GameObject ProjectileGameObject;
    protected HealthSystem enemyHealthSystem;

    protected void Awake()
    {
        ProjectileTransform = transform;
        ProjectileGameObject = gameObject;
    }

    protected virtual void OnEnable()
    {
        if (shootAudioSource.enabled)
            shootAudioSource.Play();
        ProjectileTransform.rotation = Quaternion.AngleAxis(Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg, Vector2.up);
    }

    public Projectile SetData(Vector2 direction, Transform owner, TurretData data)
    {
        aimDirection = direction;
        Owner = owner;
        TurretData = data;
        return this;
    }

    protected bool IsCrit(int chance)
    {
        return Random.Range(0, 100) < chance;
    }
}
