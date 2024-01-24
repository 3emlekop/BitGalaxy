using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private SplashArea splashAreaPrefab;
    [SerializeField] private ParticleSystem destroyParticlePrefab;
    [SerializeField] public float destroyTime = 1f;
    [SerializeField] private bool destroyOnCollision = true;

    private SplashArea splash;
    private ParticleSystem destroyParticle;
    private float timer;

    new private void Awake()
    {
        base.Awake();

        if(splashAreaPrefab == null)
            Debug.LogError("Bullet's splashAreaPrefab is not set. Bullet name: " + ProjectileGameObject.name);

        splash = Instantiate(splashAreaPrefab);
        splash.Damage = TurretData.Damage;
        splash.Radius = TurretData.SplashRadius;
        splash.gameObject.SetActive(false);
        destroyParticle = Instantiate(destroyParticlePrefab);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            ProjectileGameObject.SetActive(false);
        else
            ProjectileTransform.position += ProjectileTransform.up * TurretData.BulletSpeed * Time.deltaTime;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        timer = destroyTime;
    }

    private void OnDisable()
    {
        if (destroyParticle == null)
            return;

        if (TurretData.SplashRadius > 0)
            splash.Splash(ProjectileTransform.position);

        destroyParticle.transform.position = ProjectileTransform.position;
        destroyParticle.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(IgnoreTag) || collision.isTrigger)
            return;

        if (TurretData.SplashRadius > 0)
        {
            ProjectileGameObject.SetActive(false);
            return;
        }

        if (collision.TryGetComponent<HealthSystem>(out enemyHealthSystem))
            enemyHealthSystem.TakeDamage(TurretData.Damage, IsCrit(TurretData.CritChance));

        if (destroyOnCollision)
            ProjectileGameObject.SetActive(false);
    }
}
