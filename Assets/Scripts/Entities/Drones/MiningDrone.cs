using UnityEngine;

public class MiningDrone : MonoBehaviour
{
    [SerializeField] private Laser laser;

    public float fireCooldown;

    private Transform ship;
    private Transform drone;

    private float xOffset = 0;
    private float yOffset = -1f;

    private Vector3 dronePosition;
    private ObjectPooling poolInstance;

    private void Awake()
    {
        drone = transform;
        ship = drone.parent;
        laser.Owner.tag = drone.tag;

        xOffset = Random.Range(-0.5f, 0.5f);
        yOffset = Random.Range(0.5f, 1f);

        Timer.Create(drone, Shoot, fireCooldown, true);

        poolInstance = ObjectPooling.Instance;
        poolInstance.AddPool("Miner", laser.gameObject, 2);
    }

    private void Update()
    {
        dronePosition.x = Mathf.Lerp(dronePosition.x, ship.position.x + xOffset, 0.1f);
        dronePosition.y = Mathf.Lerp(dronePosition.y, ship.position.y - yOffset, 0.1f);
        drone.position = dronePosition;
    }

    private void Shoot()
    {
        poolInstance.SpawnFromPool("Miner", dronePosition);
    }
}
