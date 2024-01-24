[System.Serializable]
public class ShipData
{
    public string ShipId { get; private set; }
    public InventoryData Inventory { get; private set; }
    public TurretData[] EquippedTurrets { get; private set; }
    public DeviceData[] EquippedDevices { get; private set; }
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public byte Defence { get; private set; }
    public byte Evasion { get; private set; }

    public ShipData(FractionData fractionData, byte shipId)
    {
        Inventory = new InventoryData();
        EquippedTurrets = new TurretData[6];
        EquippedDevices = new DeviceData[3];
        ShipId = fractionData.Name + "Ship" + (shipId < 10 ? "0" + shipId : shipId);
        
        HealthSystem healthSystem = ShipManager.instance.shipPrefabs[ShipId].GetComponent<HealthSystem>();
        MaxHealth = healthSystem.StartHealth;
        Defence = healthSystem.Defence;
        Defence = healthSystem.Evasion;
        CurrentHealth = MaxHealth;
    }
}