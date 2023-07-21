using Unity.VisualScripting;
using UnityEngine;

public interface ITurretOwner
{
    public void SetTurretData(byte index, TurretData data);
}

public class Ship : MonoBehaviour, ITurretOwner
{
    [SerializeField] private Transform turretParent;

    public Turret[] turrets { get; private set; }
    private void Awake()
    {
        turrets = GetTurrets();
    }

    public void SetTurretData(byte index, TurretData data)
    {
        if (index > GetTurretCount() - 1)
            return;

        turrets[index].SetData(data);
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

        Turret[] turrets = new Turret[turretCount];

        for (byte i = 0; i < turretCount; i++)
        {
            turrets[i] = turretParent.GetChild(i).GetComponent<Turret>();
        }

        return turrets;
    }
}