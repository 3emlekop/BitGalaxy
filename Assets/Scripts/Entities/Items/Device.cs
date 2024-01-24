using UnityEngine;
using UnityEngine.SceneManagement;

public class Device : MonoBehaviour
{
    public GameObject abilityItem;
    public Transform parentShip;
    public DeviceData deviceData;

    private HealthSystem healthSystem;
    private Transform item;
    private Vector3 offset;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            return;

        if(abilityItem != null && parentShip != null)
            SetData(abilityItem, parentShip);
    }

    public void SetData(GameObject abilityItem, Transform ship, float offset, DeviceData data)
    {
        if (data == null)
            return;

        this.offset.y = offset;
        this.deviceData = data;

        if (abilityItem != null)
        {
            item = abilityItem.transform;
            this.abilityItem = abilityItem;
        }

        parentShip = ship;
        healthSystem = ship.GetComponent<HealthSystem>();
        ApplyStats();

        if (data.Cooldown != 0)
        {
            Timer.Create(parentShip, CreateItem, offset * 3, false);
            Timer.Create(transform, ActivateItem, data.Cooldown, true);
        }
    }

    private void SetData(GameObject abilityItem, Transform ship)
    {
        offset.y = 0.3f;
        item = abilityItem.transform;
        this.abilityItem = abilityItem;
        parentShip = ship;

        Timer.Create(parentShip, CreateItem, 2, false);
        Timer.Create(transform, ActivateItem, 10, true);
    }

    protected void CreateItem()
    {
        if (abilityItem == null)
            return;

        abilityItem = Instantiate(abilityItem, parentShip);
        abilityItem.transform.position = parentShip.position + offset;
    }

    protected void ActivateItem()
    {
        if (abilityItem != null && !abilityItem.activeSelf)
        {
            item.position = parentShip.position + offset;
            abilityItem.SetActive(true);
        }

        if(healthSystem != null)
            healthSystem.Heal(deviceData.RepairStrengthModifier);
    }

    protected void ApplyStats()
    {
        healthSystem.AddStartHealth(deviceData.StartHealthModifier);
        healthSystem.AddDefence(deviceData.DefenceModifier);
    }
}
