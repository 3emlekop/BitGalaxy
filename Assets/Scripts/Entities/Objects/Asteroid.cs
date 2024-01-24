using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private bool accutateRotation;

    private Vector3 nextPosition = Vector3.zero;
    private Transform asteroid;
    private GameObject asteroidObject;
    private HealthSystem ownerHealthSystem;
    private HealthSystem healthSystem;
    private int rotation;
    private CameraShake cameraShake;

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();

        asteroid = transform;
        asteroidObject = gameObject;
        ownerHealthSystem = GetComponent<HealthSystem>();
        ownerHealthSystem.onDeath += PlayDeathSound;

        if(accutateRotation)
            rotation += Random.Range(-30, 30);
        else
            rotation = 90 * Random.Range(0, 4);
        asteroid.Rotate(0, 0, rotation);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayDeathSound(bool withLoot)
    {
        cameraShake.PlayExplosionSound(deathSound, 1);
    }

    private void Move()
    {
        if (asteroid.position.y < -7)
        {
            asteroidObject.SetActive(false);
            return;
        }

        nextPosition.y = asteroid.position.y - Time.deltaTime * speed;
        nextPosition.x = asteroid.position.x;
        asteroid.position = nextPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.isTrigger)
            return;

        if (collision.gameObject.TryGetComponent<HealthSystem>(out healthSystem))
        {
            healthSystem.TakeDamage(ownerHealthSystem.CurrentHealth, false);
            ownerHealthSystem.TakeDamage(5, false);
        }
    }
}
