using UnityEngine;

public sealed class InventorySystem : MonoBehaviour
{
    private delegate void UpdateTurretSlot(byte index);
    UpdateTurretSlot updateTurretSlot;

    [SerializeField] private ShipChoosing shipChoosing;
    [SerializeField] private DeviceItem[] devices;

    [SerializeField] private GameObject turretPlaces;
    [SerializeField] private GameObject devicePlaces;

    [SerializeField] public TurretItem turretTemplate;
    [SerializeField] public DeviceItem deviceTemplate;
    [SerializeField] public ModuleItem moduleTemplate;

    [SerializeField] private Transform turretsTab;
    [SerializeField] private Transform devicesTab;
    [SerializeField] private Transform modulesTab;

    private TurretItem chosenTurret;
    private DeviceItem chosenDevice;
    private ModuleItem chosenModule;

    private void Awake()
    {
        ApplySavedDevices();
        LoadInventory();
    }

    /// <summary>
    /// Adds item to inventory and saves it if needed.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="save"></param>
    public InventoryItem AddItem(ItemData item, bool save)
    {
        if (item == null)
            return null;

        if (item is TurretData turret)
        {
            TurretItem newItem = Instantiate(turretTemplate, turretsTab);
            newItem.SetTurretData(turret);
            newItem.gameObject.SetActive(true);
            newItem.isBought = true;
            if (save)
                newItem.itemIndex = SaveSystem.AddItem(item);
            return newItem;
        }
        else if (item is DeviceData device)
        {
            DeviceItem newItem = Instantiate(deviceTemplate, devicesTab);
            newItem.SetDeviceData(device);
            newItem.gameObject.SetActive(true);
            newItem.isBought = true;
            if (save)
                newItem.itemIndex = SaveSystem.AddItem(item);
            return newItem;
        }
        else if (item is ModuleData module)
        {
            ModuleItem newItem = Instantiate(moduleTemplate, modulesTab);
            newItem.SetModuleData(module);
            newItem.gameObject.SetActive(true);
            newItem.isBought = true;
            if (save)
                newItem.itemIndex = SaveSystem.AddItem(item);
            return newItem;
        }
        else return null;
    }

    /// <summary>
    /// Loads all the items from static local save system. 
    /// Places some items in slots if needed.
    /// </summary>
    private void LoadInventory()
    {
        if (SaveSystem.inventoryTurrets.Length == 0
            || SaveSystem.inventoryDevices.Length == 0
            || SaveSystem.inventoryModules.Length == 0)
            return;

        object[] inventories = new object[]
        {
            SaveSystem.inventoryTurrets,
            SaveSystem.inventoryDevices,
            SaveSystem.inventoryModules
        };

        foreach (object[] inv in inventories)
        {
            for (byte i = 0; i < inv.Length; i++)
            {
                if (inv[i] == null)
                    continue;

                var item = AddItem((ItemData)inv[i], false);
                item.itemIndex = i;
            }
        }
    }

    public void ChooseItem(TurretItem item) { chosenTurret = item; updateTurretSlot = PlaceTurret; }

    public void ChooseItem(DeviceItem item) { chosenDevice = item; }

    public void ChooseItem(ModuleItem item) { chosenModule = item; updateTurretSlot = PlaceModule; }

    public void TurretSlotInterract(int index)
    {
        updateTurretSlot((byte)index);
    }

    public void PlaceTurret(byte index)
    {
        turretPlaces.SetActive(false);
        Time.timeScale = 1f;

        if (chosenTurret == null)
            return;

        ClearTurretSlot(index);
        SaveSystem.placedTurrets[index] = chosenTurret.GetData();
        SaveSystem.inventoryTurrets[chosenTurret.itemIndex] = null;
        Debug.Log("index of " + chosenTurret.itemIndex + " was deleted"); // temp
        shipChoosing.UpdateTurrets();

        Destroy(chosenTurret.gameObject);
    }

    public void PlaceDevice(int index)
    {
        devicePlaces.SetActive(false);
        Time.timeScale = 1f;

        if (chosenDevice == null)
            return;

        ClearDeviceSlot((byte)index);
        SaveSystem.placedDevices[index] = chosenDevice.GetData();
        SaveSystem.inventoryDevices[chosenDevice.itemIndex] = null;
        devices[index].SetDeviceData(chosenDevice.GetData());

        ShipChoosing.instance.ApplyTextStats(ShipChoosing.instance.currentShip);
        Destroy(chosenDevice.gameObject);
    }


    public void PlaceModule(byte index)
    {
        turretPlaces.SetActive(false);
        Time.timeScale = 1f;

        if (chosenModule == null)
            return;

        if (SaveSystem.placedTurrets[index] != null)
        {
            if (SaveSystem.placedTurrets[index].module != null)
                return;

            SaveSystem.placedTurrets[index].module = chosenModule.GetData();
            SaveSystem.inventoryModules[chosenModule.itemIndex] = null;
            Destroy(chosenModule.gameObject);
            shipChoosing.UpdateTurrets();
        }

    }

    /// <summary>
    /// Applies the saved devices slots data to in-game slots.
    /// </summary>
    private void ApplySavedDevices()
    {
        for (byte i = 0; i < devices.Length; i++)
            devices[i].SetDeviceData(SaveSystem.placedDevices[i]);
    }

    /// <summary>
    /// Resets all the applied items and moves them to inventory
    /// </summary>
    public void ResetItems()
    {
        for (byte i = 0; i < SaveSystem.placedDevices.Length + SaveSystem.placedTurrets.Length; i++)
        {
            if (i < SaveSystem.placedDevices.Length)
            {
                AddItem(SaveSystem.placedDevices[i], true);
                SaveSystem.placedDevices[i] = null;
            }
            else
            {
                AddItem(SaveSystem.placedTurrets[i - SaveSystem.placedDevices.Length], true);
                SaveSystem.placedTurrets[i - SaveSystem.placedDevices.Length] = null;
            }
        }

        foreach (var device in devices)
            device.SetDeviceData(null);
        shipChoosing.UpdateTurrets();
        shipChoosing.ApplyTextStats(ShipChoosing.instance.currentShip);
    }

    public void ClearTurretSlot(byte index)
    {
        if (SaveSystem.placedTurrets[index] == null)
            return;

        //TODO: Move TurretsUpdate() here from ShipChoosing 
        AddItem(SaveSystem.placedTurrets[index], true);
    }

    public void ClearDeviceSlot(byte index)
    {
        if (SaveSystem.placedDevices[index] == null)
            return;

        devices[index].SetDeviceData(null);
        AddItem(SaveSystem.placedDevices[index], true);
    }
}
