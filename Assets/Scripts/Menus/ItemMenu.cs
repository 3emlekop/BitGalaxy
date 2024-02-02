using System;
using TMPro;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    [Header("Text references")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private TextMeshProUGUI gradeText;
    [SerializeField] private TextMeshProUGUI priceText;

    [Header("Object references")]
    [SerializeField] private Item itemPreview;
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject turretPlaces;
    [SerializeField] private GameObject devicePlaces;

    private int currentItemId;

    public void ApplyInfo(ItemData itemData, int itemId)
    {
        itemPreview.SetData(itemData);
        currentItemId = itemId;
        nameText.text = itemData.Name;
        descriptionText.text = itemData.Description;
        gradeText.text = string.Empty;
        switch(itemData.ItemType)
        {
            case ItemType.Turrets:
                UpdateStatsText((TurretData)itemData);
                break;
            case ItemType.Devices:
                UpdateStatsText((DeviceData)itemData);
                break;
            case ItemType.Modules:
                UpdateStatsText((ModuleData)itemData);
                break;
        }
        priceText.text = itemData.Price.ToString();
    }

    private void UpdateStatsText(TurretData turret)
    {
        gradeText.text = "Grade: " + Enum.GetName(typeof(Grade), turret.Grade);
        //turret stats display
    }

    private void UpdateStatsText(DeviceData device)
    {
        //device stats display
    }

    private void UpdateStatsText(ModuleData module)
    {
        //module stats display
    }

    public void Sell()
    {
        inventory.RemoveItem(currentItemId);
    }

    public void Buy()
    {
        Inventory.playerInventoryInstance.AddItem(itemPreview.Data);
    }

    public void Equip()
    {
        if(itemPreview.Data.ItemType == ItemType.Devices)
            devicePlaces.SetActive(true);
        else
            turretPlaces.SetActive(true);
    }
}
