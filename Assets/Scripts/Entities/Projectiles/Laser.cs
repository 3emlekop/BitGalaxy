using System.Collections;
using UnityEngine;

public class Laser : Projectile
{
    [SerializeField] private bool isHealing;
    [SerializeField] private float fadeOutSpeed = 10f;

    private HealthSystem ownerHealthSystem;
    private Vector3 startScale;
    private Vector3 currentScale;
    private byte damage;

    new private void Awake()
    {
        if(isHealing)
            ownerHealthSystem = Owner.GetComponent<HealthSystem>();

        base.Awake();
        startScale = ProjectileTransform.localScale;
        currentScale.y = startScale.y;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(FadeOut());    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(IgnoreTag) || collision.isTrigger)
            return;

        if (collision.TryGetComponent<HealthSystem>(out enemyHealthSystem))
        {
            damage = enemyHealthSystem.TakeDamage(TurretData.Damage, IsCrit(TurretData.CritChance));
            if(isHealing)
                ownerHealthSystem.Heal(damage);
        }
    }

    private IEnumerator FadeOut()
    {
        currentScale.x = startScale.x;
        while (currentScale.x > 0.01f)
        {
            currentScale.x = Mathf.Lerp(currentScale.x, 0, Time.deltaTime * fadeOutSpeed);
            ProjectileTransform.localScale = currentScale;
            yield return null;
        }
        ProjectileGameObject.SetActive(false);
    }
}
