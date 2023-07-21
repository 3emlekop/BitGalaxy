using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private delegate void Behave();
    Behave behave;

    [SerializeField] private ParticleSystem deathParticlePrefab;
    private ParticleSystem deathParticle;

    [SerializeField] private SplashArea splashAreaPrefab;
    private SplashArea splashArea;

    public Transform shipsParent;
    public Direction aimDirection;
    public string ignoreTag;
    public TurretData data;

    private Transform particle;
    private Transform bulletTransform;
    private Transform target;
    private HealthSystem healthSystem;
    private SplashHandler splash;
    private Vector2 position;
    private Vector3 startScale;
    private Vector3 direction;
    private float randomAngle;
    private float rotation;
    private float startDestroyTimer;
    private float destroyTimer;

    private void Awake()
    {
        bulletTransform = transform;

        deathParticle = Instantiate(deathParticlePrefab);
        particle = deathParticle.transform;

        startDestroyTimer = data.destroyTime;

        if (data.isLaser)
        {
            behave = FadeOut;
            startScale = bulletTransform.localScale;
        }
        else
            behave = MoveForward;

        if (data.isSplash)
        {
            splashArea = Instantiate(splashAreaPrefab);

            splash = bulletTransform.AddComponent<SplashHandler>();
            splash.SetStats(data.splashRadius, data.damage);
            splash.SetSplashArea(splashArea, bulletTransform);
        }

        rotation = (float)aimDirection > 0 ? 0 : 180;
    }

    private void FixedUpdate()
    {
        behave();
        if (destroyTimer > 0)
            destroyTimer -= Time.fixedDeltaTime;
        else
            gameObject.SetActive(false);

    }

    private void MoveForward()
    {
        bulletTransform.position += bulletTransform.up * data.bulletSpeed * Time.deltaTime;
    }

    private void FadeOut()
    {
        if (bulletTransform.localScale.x < 0.01f)
            bulletTransform.gameObject.SetActive(false);

        position.x = Mathf.Lerp(bulletTransform.localScale.x, 0, Time.deltaTime * 10f);
        position.y = startScale.y;
        bulletTransform.localScale = position;
    }

    private void TargetLock()
    {
        target = null;

        foreach (Transform ship in shipsParent)
        {
            if (ship.CompareTag(ignoreTag))
                continue;

            if (!ship.gameObject.activeSelf)
                continue;

            target = ship;
            behave = MoveToTarget;
            break;
        }
    }

    private void MoveToTarget()
    {
        if (target.IsUnityNull())
        {
            behave = MoveForward;
            return;
        }

        direction = target.position - bulletTransform.position;
        bulletTransform.rotation = Quaternion.RotateTowards(bulletTransform.rotation, Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x)
            * Mathf.Rad2Deg - 90f, Vector3.forward), data.autoTargetForce * 5);

        bulletTransform.position += transform.up * Time.deltaTime * data.bulletSpeed;
    }

    public void SetDirection(Direction direction) { aimDirection = direction; }

    public void SetIgnoreTag(string tag) { if (tag != "") ignoreTag = tag; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ignoreTag) || collision.isTrigger)
            return;

        if (!data.isLaser && data.destroyOnCollision)
            bulletTransform.gameObject.SetActive(false);

        if (data.isSplash)
            return;

        if (collision.TryGetComponent<HealthSystem>(out healthSystem))
            healthSystem.TakeDamage(data.damage, GetCrit());
    }

    private bool GetCrit() { return Random.Range(0, 101) < data.critChance * 100; }

    private void OnEnable()
    {
        destroyTimer = startDestroyTimer;

        if (data.isAutoTaget)
            TargetLock();

        if (data.isLaser)
            bulletTransform.localScale = startScale;

        randomAngle = Random.Range(-100f, 100f) * (1 - data.accuracy);
        bulletTransform.rotation = Quaternion.Euler(0, 0, rotation + randomAngle);
    }

    private void OnDisable()
    {
        if (data.isLaser)
            return;

        if (data.isSplash)
            splash.CreateSplash();

        if (particle != null)
        {
            particle.position = bulletTransform.position;
            particle.rotation = bulletTransform.rotation;
            deathParticle.Play();
        }
    }

    public void SetData(TurretData data) { this.data = data; }

    public void SetShipsParent(Transform transform) { shipsParent = transform; }
}
