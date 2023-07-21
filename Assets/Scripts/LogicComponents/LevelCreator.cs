using UnityEngine;
public class LevelCreator : MonoBehaviour
{
    [SerializeField] public Ship[] enemyShips;
    [SerializeField] public Ship[] bossShips;
    [SerializeField] public Asteroid[] asteroids;
    [SerializeField] private GameManager gameManager;

    private enum Area
    {
        Void,
        AsteroidField,
        Boss
    }


    private Area area = Area.Void;
    private float spawnTime = 1.5f;

    private Transform creator;

    private int roundScore;

    Vector2 enemySpawnPos = new Vector2(-5, 0);
    Vector2 asteroidSpawnPos = new Vector2(0, 8);

    public static LevelCreator instance;

    private void Start()
    {
        if (instance == null)
            instance = this;

        creator = transform;

        Timer.Create(creator, SetNextArea, 10f, true);
    }

    private void FixedUpdate()
    {
        if (spawnTime > 0)
            spawnTime -= Time.fixedDeltaTime;
        else
        {
            spawnTime = Mathf.Clamp(6 - Mathf.Sqrt(roundScore / 20), 1, 6);
            SpawnObstacle();
        }
    }

    private void SetNextArea() { area = GetNextArea(); }

    private Area GetNextArea()
    {
        byte chance = (byte)Random.Range(0, 100);

        if (ScorePointer.RoundPoints > 1000)
            return Area.Void;

        if (chance >= 0 && chance <= 50)
            return Area.Void;

        if (chance > 50 && chance <= 100)
            return Area.Void;
        //return Area.AsteroidField;

        return Area.Void;
    }

    private void SpawnObstacle()
    {
        switch (area)
        {
            case Area.Void:
                gameManager.SpawnShip(enemyShips[GetDifficulty()], GetEnemySpawn(), true);
                break;
            case Area.AsteroidField:
                //spawn asteroids
                break;
            case Area.Boss:
                //spawn boss, destroy all ships
                break;
            default: break;
        }
    }

    private Vector2 GetEnemySpawn()
    {
        enemySpawnPos.x = Random.Range(-5, 5);
        enemySpawnPos.y = 7;
        return enemySpawnPos;
    }

    private byte GetDifficulty()
    {
        roundScore = ScorePointer.RoundPoints;
        byte currentCeil = (byte)(Mathf.CeilToInt(roundScore / 60) + 1);
        byte currentFloor = (byte)(Mathf.CeilToInt(roundScore / 200));
        return (byte)Mathf.Clamp(Random.Range(0, currentCeil), 0, enemyShips.Length - 1);
    }
}
