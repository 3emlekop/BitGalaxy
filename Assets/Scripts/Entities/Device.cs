using UnityEngine;

public class Device : MonoBehaviour
{
    private HealthSystem healthSystem;
    private DeviceData data;
    private GameObject abilityItem;
    private Transform parentShip;
    private Transform item;
    private Vector3 offset;

    public void SetData(DeviceData data, GameObject abilityItem, Transform ship, float offset)
    {
        if (data == null)
            return;

        item = abilityItem.transform;
        this.offset.y = offset;
        this.data = data;
        this.abilityItem = abilityItem;

        parentShip = ship;
        healthSystem = ship.GetComponent<HealthSystem>();
        ApplyStats();

        Timer.Create(parentShip, CreateItem, offset * 3, false);
        Timer.Create(transform, ActivateItem, data.cooldown, true);
    }
    private void CreateItem()
    {
        if (abilityItem == null)
            return;

        abilityItem = Instantiate(abilityItem, parentShip);
        abilityItem.transform.position = parentShip.position + offset;
    }

    private void ActivateItem()
    {
        if (abilityItem != null && !abilityItem.activeSelf)
        {
            item.position = parentShip.position + offset;
            abilityItem.SetActive(true);
        }
        healthSystem.Repair(data.repairStrength);
    }

    private void ApplyStats()
    {
        healthSystem.AddStartHealth(data.health);
        healthSystem.AddDefence(data.defence);
    }
}
