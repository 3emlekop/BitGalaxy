using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private enum Type
    {
        small,
        smallCrystal,
        huge,
        hugeCrystal
    }

    [SerializeField] private Type type;
    [SerializeField] private float speed;

    private Vector3 nextPosition = Vector3.zero;
    private Transform asteroid;

    private void Awake()
    {
        asteroid = transform;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        nextPosition.y = asteroid.position.y - Time.deltaTime * speed;
        nextPosition.x = asteroid.position.x;
        asteroid.position = nextPosition;
    }
}
