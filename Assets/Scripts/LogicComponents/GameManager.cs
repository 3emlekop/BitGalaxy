using Unity.VisualScripting;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [Header("Parents")]
    [SerializeField] private Transform shipsParent;
    [SerializeField] private Transform asteroidsParent;
    [SerializeField] private Transform bossesParent;

    [SerializeField] public Device[] playerDevices = new Device[3];
    [SerializeField] public GameObject[] abilityItems;

    private ObjectPooling poolInstance;
    private Ship player;

    private void Start()
    {
        SpawnPlayer();
        poolInstance = ObjectPooling.Instance;

        if (LevelCreator.instance == null)
            return;

        foreach (Ship ship in LevelCreator.instance.enemyShips)
            poolInstance.AddPool(ship.gameObject.name, ship.gameObject, 3, shipsParent);

        foreach (Asteroid asteroid in LevelCreator.instance.asteroids)
            poolInstance.AddPool(asteroid.gameObject.name, asteroid.gameObject, 10, asteroidsParent);
    }

    public Ship SpawnShip(Ship ship, Vector2 position, string name)
    {
        if (!ship.IsUnityNull())
        {
            ship.gameObject.SetActive(true);
            Ship newShip = Instantiate(ship, position, Quaternion.identity, shipsParent);
            newShip.gameObject.name = name;

            return newShip;
        }
        return null;
    }

    public Ship SpawnShip(Ship ship, Vector2 position, bool isPool)
    {
        if (GetActiveShipsCount() > 5)
            return null;

        if (!ship.IsUnityNull() && isPool)
        {
            Ship newShip = poolInstance.SpawnFromPool(ship.gameObject.name, position).GetComponent<Ship>();
            return newShip;
        }
        else
            return null;
    }

    public void SpawnAsteroid(Asteroid asteroid, Vector2 position)
    {
        if (!asteroid.IsUnityNull())
        {
            asteroid.gameObject.SetActive(true);
            Asteroid newAsteroid = Instantiate(asteroid, position, Quaternion.identity, asteroidsParent);
        }
    }

    public void SpawnAsteroid(Asteroid asteroid, Vector2 position, bool isPool)
    {
        if (!asteroid.IsUnityNull() && isPool)
            poolInstance.SpawnFromPool(asteroid.gameObject.name, position);
    }

    private void SpawnPlayer()
    {
        if(PlayerPrefs.HasKey("startShip"))
            player = SpawnShip(ShipChoosing.instance.playerShips[PlayerPrefs.GetInt("startShip")], Vector2.down * 7, "Player");
        else
            player = SpawnShip(ShipChoosing.instance.playerShips[0], Vector2.down * 7, "Player");


        for (byte i = 0; i < SaveSystem.placedTurrets.Length; i++)
            player.SetTurretData(i, SaveSystem.placedTurrets[i]);

        for (byte i = 0; i < playerDevices.Length; i++)
            if (SaveSystem.placedDevices[i] != null)
                playerDevices[i].SetData(SaveSystem.placedDevices[i],
                    abilityItems[(int)SaveSystem.placedDevices[i].ability], player.transform, 0.3f + (i * 0.1f));

        player.AddComponent<CircleCollider2D>();
        player.AddComponent<ItemCollector>();
    }

    public void RespawnPlayer()
    {
        foreach (Transform ship in shipsParent)
            ship.GetComponent<HealthSystem>().Die();

        player.transform.position = Vector2.down * 7;
        player.gameObject.SetActive(true);
    }

    private byte GetActiveShipsCount()
    {
        byte count = 0;
        foreach (Transform ship in shipsParent)
            if (ship.GameObject().activeSelf)
                count++;
        return count;
    }
}
