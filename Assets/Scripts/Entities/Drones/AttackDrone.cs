using UnityEngine;

public class AttackDrone : MonoBehaviour
{
    [SerializeField] private Bullet bullet;

    public float fireCooldown;
    private float counter;

    private Transform ship;
    private Transform drone;
    private float yOffset = 1f;

    private Vector3 dronePosition;
    private ObjectPooling poolInstance;

    private void Start()
    {
        drone = transform;
        ship = drone.parent;
        yOffset = Random.Range(0.5f, 1f);

        Timer.Create(drone, Shoot, fireCooldown, true);

        poolInstance = ObjectPooling.Instance;
        bullet.Owner.tag = drone.tag;
        poolInstance.AddPool("Attack", bullet.gameObject, 6);
    }

    private void Update()
    {
        if (counter > 2 * Mathf.PI)
            counter = 0;
        else
            counter += Time.deltaTime;

        dronePosition.x = Mathf.Lerp(dronePosition.x, ship.position.x + Mathf.Cos(counter) / 2, 0.1f);
        dronePosition.y = Mathf.Lerp(dronePosition.y, ship.position.y + yOffset, 0.1f);
        drone.position = dronePosition;
    }

    private void Shoot()
    {
        poolInstance.SpawnFromPool("Attack", drone.position);
    }
}
