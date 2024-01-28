using UnityEngine;

[System.Serializable]
public class ShipData
{
    public string ShipId { get; private set; }
    [SerializeField] private InventoryData inventory;
    [SerializeField] private TurretData[] equippedTurrets;
    [SerializeField] private DeviceData[] equippedDevices;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private byte defence;
    [SerializeField] private byte evasion;

    public InventoryData Inventory => inventory;
    public TurretData[] EquippedTurrets => equippedTurrets;
    public DeviceData[] EquippedDevices => equippedDevices;
    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    public byte Defence => defence;
    public byte Evasion => evasion;

    public ShipData(FractionData fractionData, byte shipId)
    {
        ShipId = fractionData.Name + "Ship" + (shipId < 10 ? "0" + shipId : shipId.ToString());
        GameObject shipPrefab = ResourceManager.instance.shipPrefabs[ShipId];
        var healthSystem = shipPrefab.GetComponent<HealthSystem>();

        inventory = new InventoryData();
        equippedTurrets = new TurretData[shipPrefab.GetComponent<Ship>().GetTurretCount()];
        equippedDevices = new DeviceData[3];

        maxHealth = healthSystem.StartHealth;
        defence = healthSystem.Defence;
        evasion = healthSystem.Evasion;
        currentHealth = maxHealth;
    }
}