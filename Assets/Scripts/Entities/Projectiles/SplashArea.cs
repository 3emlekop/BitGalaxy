using System.Collections;
using UnityEngine;

public class SplashArea : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public int Damage { get; set; }
    public float Radius
    {
        get
        {
            return scale.x;
        }
        set
        {
            scale.x = value;
            scale.y = value;
            scale.z = value;
        }
    }
    public string IgnoreTag { get; set; }
    public Color StartColor { get; set; }

    #region references
    private Transform splashTransform;
    private GameObject splashGameObject;
    private CameraShake cameraShake;
    private SpriteRenderer sprite;
    #endregion

    private HealthSystem targetHealth;
    private Vector3 scale;
    private Color currentColor;
    private bool firstSpawn = true;

    private void Awake()
    {
        splashTransform = transform;
        splashGameObject = gameObject;
        cameraShake = Camera.main.GetComponent<CameraShake>();
        sprite = GetComponent<SpriteRenderer>();

        StartColor = sprite.color;
    }

    private void OnEnable()
    {
        currentColor = StartColor;
        StartCoroutine(FadeOut());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(IgnoreTag))
            return;

        if (collision.TryGetComponent<HealthSystem>(out targetHealth))
            targetHealth.TakeDamage(Damage, false);
        else if (collision.CompareTag("Bullet"))
            collision.gameObject.SetActive(false);
    }

    private IEnumerator FadeOut()
    {
        for (byte i = 0; i < 10; i++)
        {
            currentColor.a -= Time.fixedDeltaTime;
            sprite.color = currentColor;
            yield return null;
        }
        gameObject.SetActive(false);
    }

    public void Splash(Vector2 pos)
    {
        if (firstSpawn)
        {
            firstSpawn = false;
            return;
        }

        splashTransform.position = pos;
        splashGameObject.SetActive(true);

        if (audioSource.enabled)
            audioSource.Play();

        if (cameraShake != null)
            cameraShake.Shake(0.2f, Radius / 50);
    }
}
