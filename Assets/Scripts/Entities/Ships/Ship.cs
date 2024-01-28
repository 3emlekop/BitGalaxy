using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

public class Ship : MonoBehaviour
{
    [SerializeField] private bool isActivatedTurrets = true;
    [SerializeField] private Transform turretParent;

    public Turret[] Turrets { get; private set; }

    protected Transform shipTransform;

    private void Awake()
    {
        shipTransform = transform;
        Turrets = GetTurrets();

        foreach (var turret in Turrets)
            turret.isActivated = isActivatedTurrets;
    }

    public void SetTurret(byte index, TurretData data)
    {
        if (index > GetTurretCount() - 1)
            return;

        Turrets[index].SetData(data);
    }

    public byte GetTurretCount()
    {
        if (turretParent.IsUnityNull())
            return 0;

        return (byte)turretParent.childCount;
    }

    public Turret[] GetTurrets()
    {
        byte turretCount = GetTurretCount();
        if (turretParent.IsUnityNull())
            Debug.LogError("Turret parent is not set to ship's [Serialize Field]");

        if (turretParent.childCount == 0)
            return null;

        Turret[] turrets = new Turret[turretCount];

        for (byte i = 0; i < turretCount; i++)
            turrets[i] = turretParent.GetChild(i).GetComponent<Turret>();

        return turrets;
    }

    public void SetTurretsEnabled(bool state)
    {
        foreach (var turret in Turrets)
            turret.IsActive = state;
    }

    public void MoveToPoint(Vector2 point)
    {
        StartCoroutine(MoveToPointCoroutine(point, false));
    }

    public void MoveToPoint(Vector2 point, bool disableOnEnd)
    {
        StartCoroutine(MoveToPointCoroutine(point, disableOnEnd));
    }

    private IEnumerator MoveToPointCoroutine(Vector2 point, bool disableOnEnd)
    {
        while (Vector2.Distance(point, shipTransform.position) > 0.05f)
        {
            shipTransform.position = Vector2.Lerp(shipTransform.position, point, Time.fixedDeltaTime);
            yield return null;
        }
        if (disableOnEnd)
            gameObject.SetActive(false);
    }

    public void ApplyData(ShipData data)
    {
    }
}