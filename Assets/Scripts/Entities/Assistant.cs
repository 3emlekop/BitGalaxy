using System.Collections;
using UnityEngine;

public class Assistant : MonoBehaviour
{
    [SerializeField] private TurretData[] turretData = new TurretData[2];
    private Transform shipsParent;
    private Ship ship;
    private Transform shipTransform;
    private Vector3 nextPoint = new Vector3();

    private void Start()
    {
        shipTransform = transform;
        shipsParent = transform.parent.parent;
        shipTransform.SetParent(shipsParent);
        ship = GetComponent<Ship>();

        for (byte i = 0; i < turretData.Length; i++)
            ship.SetTurretData(i, turretData[i]);

        Move();
        Timer.Create(shipTransform, Move, 5, true);
    }

    private void Move()
    {
        nextPoint.x = Random.Range(-2f, 2f);
        nextPoint.y = Random.Range(-3f, -4f);
        StartCoroutine(GoToPoint(nextPoint));
    }

    private IEnumerator GoToPoint(Vector3 point)
    {
        while (Mathf.Abs(shipTransform.position.x - point.x) > 0.01f &&
            Mathf.Abs(shipTransform.position.y - point.y) > 0.01f)
        {
            shipTransform.position = Vector3.Lerp(shipTransform.position, point, 0.02f);
            yield return null;
        }
    }
}