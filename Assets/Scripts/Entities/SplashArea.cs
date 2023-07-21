using UnityEngine;

public class SplashArea : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Color startColor;
    private Color nextColor;

    private HealthSystem targetHealth;
    public int damage;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        startColor = sprite.color;
    }

    private void FadeOut()
    {
        if (nextColor.a <= 0.01)
        {
            gameObject.SetActive(false);
            return;
        }

        nextColor.a -= Time.fixedDeltaTime;
        sprite.color = nextColor;
    }

    private void FixedUpdate()
    {
        FadeOut();
    }

    private void OnEnable()
    {
        nextColor = startColor;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HealthSystem>(out targetHealth))
            targetHealth.TakeDamage(damage, false);
        else
            collision.gameObject.SetActive(false);
    }
}
