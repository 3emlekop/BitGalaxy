using UnityEngine;

public class Ray : Projectile
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private float destroyTimer = 1f;
    [SerializeField] private bool isHealing;

    private Vector2 startPos;
    private Vector2 direction;
    private RaycastHit2D hit;
    private float timer;
    private int mask;

    new private void Awake()
    {
        base.Awake();
        if(IgnoreTag == "Player")
            mask = 1 << 10;
        else
            mask = 1 << 11;

        mask = ~mask;
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
            ProjectileGameObject.SetActive(false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        timer = destroyTimer;
        ShootRaycast();
    }

    private void ShootRaycast()
    {
        startPos = ProjectileTransform.position;
        direction = ProjectileTransform.TransformDirection(Vector2.up);
        hit = Physics2D.Raycast(startPos, direction, 5, mask);
        line.SetPosition(0, startPos);

        if (hit)
        {
            line.SetPosition(1, hit.point);
            Debug.Log($"start: {startPos} direction {direction} end: {hit.point} hit: {hit.collider.tag}");

            if (hit.collider.CompareTag(IgnoreTag) || hit.collider.isTrigger)
                return;

            if (hit.collider.TryGetComponent<HealthSystem>(out enemyHealthSystem))
                enemyHealthSystem.TakeDamage(TurretData.Damage, IsCrit(TurretData.CritChance));
        }
        else
            line.SetPosition(1, startPos + (Vector2)ProjectileTransform.up * 5);
    }
}
