using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] private Turret[] turrets;
    [SerializeField] private Device[] devices;

    public void SetTurretData(int id, TurretData data)
    {
        turrets[id].SetData((ItemData)data);
    }

    public void SetDeviceData(int id, DeviceData data, Transform ship, GameObject abilityItem, float offset)
    {
        devices[id].SetData(abilityItem, ship, offset, data);
    }
}