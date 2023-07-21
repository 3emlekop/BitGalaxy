using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum Behaviours
    {
        random, follower, distant, nearby, fast, defender
    }

    [SerializeField] public TurretData[] availableTurrets;
    [SerializeField] private Behaviours behaviour;
    private Transform shipsParent;

    private System.Random rnd = new System.Random();

    private Vector3 nextMovePoint = new Vector3();
    private Transform enemy;
    private Ship ship;
    private int moveTime = 1;

    private Transform ally;
    void Start()
    {
        enemy = transform;
        shipsParent = transform.parent;
        ship = GetComponent<Ship>();

        switch (behaviour)
        {
            case Behaviours.random:
                SetRandomPoint();
                moveTime = rnd.Next(6, 12);
                Timer.Create(enemy, SetRandomPoint, moveTime, true);
                break;
            case Behaviours.nearby:
                Timer.Create(enemy, SetNearbyPoint, moveTime, true);
                break;
            case Behaviours.fast:
                SetFastRandomPoint();
                moveTime = 1;
                Timer.Create(enemy, SetFastRandomPoint, moveTime, true);
                break;
            case Behaviours.distant:
                SetDistantPoint();
                break;
            case Behaviours.follower:
                SetEnemyPoint();
                moveTime = 1;
                Timer.Create(enemy, SetEnemyPoint, moveTime, true);
                break;
            case Behaviours.defender:
                SetAllyPoint();
                moveTime = 3;
                Timer.Create(enemy, SetAllyPoint, moveTime, true);
                break;
            default:
                enemy.gameObject.SetActive(false); 
                break;
        }

        for (byte i = 0; i < ship.GetTurretCount(); i++)
            ship.SetTurretData(i, availableTurrets[rnd.Next(0, availableTurrets.Length)]);
    }

    private void SetRandomPoint()
    {
        nextMovePoint.x = (float)rnd.Next(-20, 20) / 10;
        nextMovePoint.y = (float)rnd.Next(-5, 45) / 10;
        StartCoroutine(MoveToPoint(nextMovePoint));
    }

    private void SetFastRandomPoint()
    {
        nextMovePoint.x = (float)rnd.Next(-20, 20) / 10;
        nextMovePoint.y = (float)rnd.Next(20, 30) / 10;
        StartCoroutine(MoveToPoint(nextMovePoint));
    }

    private void SetNearbyPoint()
    {
        nextMovePoint.x = (float)rnd.Next(-20, 20) / 10;
        nextMovePoint.y = (float)rnd.Next(-15, 0) / 10;
        StartCoroutine(MoveToPoint(nextMovePoint));
    }

    private void SetDistantPoint()
    {
        nextMovePoint.x = (float)rnd.Next(-15, 15) / 10;
        nextMovePoint.y = (float)rnd.Next(40, 45) / 10;
        StartCoroutine(MoveToPoint(nextMovePoint));
    }

    private void SetEnemyPoint()
    {
        nextMovePoint.x = shipsParent.GetChild(0).position.x;
        nextMovePoint.y = (float)rnd.Next(10, 45) / 10;
        StartCoroutine(MoveToPoint(nextMovePoint));

    }

    private void SetAllyPoint()
    {
        foreach (Transform ship in shipsParent)
            if (ship.CompareTag(enemy.tag) && ship.gameObject.activeSelf)
            {
                ally = ship;
                break;
            }

        if (ally == null)
        {

            nextMovePoint.x = 0;
            nextMovePoint.y = 0;
            return;
        }

        nextMovePoint.x = ally.position.x + (Random.Range(-10, 11) / 10);
        nextMovePoint.y = ally.position.y - 1;
        StartCoroutine(MoveToPoint(nextMovePoint));
    }

    private IEnumerator MoveToPoint(Vector3 point)
    {
        while (Mathf.Abs(enemy.position.x - point.x) > 0.01f &&
            Mathf.Abs(enemy.position.y - point.y) > 0.01f)
        {
            enemy.position = Vector3.Lerp(enemy.position, point, Time.fixedDeltaTime * Time.timeScale);
            yield return null;
        }
    }

    private void OnEnable()
    {
        if (nextMovePoint != Vector3.zero)
            StartCoroutine(MoveToPoint(nextMovePoint));
    }

    private byte GetEnemyDifficulty()
    {
        var difficulty = (byte)(ScorePointer.RoundPoints/1000);
        return 0;
    }
}
