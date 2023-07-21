using UnityEngine;

public class RepairDrone : MonoBehaviour
{
    [SerializeField] private SpriteRenderer healingLaser;

    public float healCooldown = 1;
    public byte healStrength = 1;
    private float counter;

    private Transform ship;
    private Transform drone;
    private HealthSystem healthSystem;

    private Vector3 dronePosition;
    private Vector3 direction;
    private Color transparentColor = new Color(1, 1, 1, 0);

    private void Start()
    {
        drone = transform;
        ship = drone.parent;
        healthSystem = ship.GetComponent<HealthSystem>();
        Timer.Create(drone, Repair, healCooldown, true);
    }

    private void Update()
    {
        if (counter > 2 * Mathf.PI)
            counter = 0;
        else
            counter += Time.deltaTime;

        dronePosition.x = Mathf.Lerp(dronePosition.x, ship.position.x + Mathf.Cos(counter) / 2, 0.1f);
        dronePosition.y = Mathf.Lerp(dronePosition.y, ship.position.y + Mathf.Sin(counter) / 2, 0.1f);
        drone.position = dronePosition;
        direction = ship.position - dronePosition;
        drone.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x)
            * Mathf.Rad2Deg - 90f, Vector3.forward);

        if (healingLaser.color.a > 0.1f)
            healingLaser.color = Color.Lerp(healingLaser.color, transparentColor, 0.2f);
    }

    private void Repair()
    {
        healthSystem.Repair(healStrength);
        LaserFlash();
    }

    private void LaserFlash()
    {
        healingLaser.color = Color.white;
    }
}
