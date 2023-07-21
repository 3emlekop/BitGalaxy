using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour
{
    private delegate void BuyItem();
    BuyItem buyItem;

    [SerializeField] private GameObject turretPlaces;
    [SerializeField] private GameObject devicePlaces;
    [SerializeField] private InventorySystem inventory;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI nameField;
    [SerializeField] private TextMeshProUGUI descriptionField;
    [SerializeField] private TextMeshProUGUI statsField;
    [SerializeField] private TextMeshProUGUI priceField;

    [Header("Visuals")]
    [SerializeField] private Image itemImage;
    [SerializeField] private Image outlineImage;
    [SerializeField] private Image moduleImage;

    [Header("Buttons")]
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject sellButton;
    [SerializeField] private GameObject installTurretButton;
    [SerializeField] private GameObject installDeviceButton;
    [SerializeField] private GameObject installModuleButton;

    private Color transparentColor = new Color(1, 1, 1, 0);

    private GameObject chosenObject;
    private TurretItem chosenTurret;
    private DeviceItem chosenDevice;
    private ModuleItem chosenModule;
    private Transform menuTransform;

    private Vector3 menuStartPos;
    private Vector3 menuCurrentPos;
    private object[] toSell;

    void Start()
    {
        menuTransform = transform;
        menuStartPos = menuTransform.position;
        menuCurrentPos = menuStartPos;

        Close();
    }
    public void OpenMenu(TurretItem item)
    {
        TurretData data = item.GetData();
        SetMainProperties(data, item.isBought, 0);
        ShowModule(data);

        statsField.text =
            $"Damage:\t\t{data.damage}\n" +
            $"FireRate:\t\t{Mathf.RoundToInt(data.fireRate)}/sec\n" +
            $"Accuracy:\t{Mathf.RoundToInt(data.accuracy * 100)}%\n" +
            $"Shoots:\t\t{data.shootsCount}\n" +
            $"Crit Chance:\t{Mathf.RoundToInt(data.critChance * 100)}%";

        chosenObject = item.gameObject;
        chosenTurret = item;
        toSell = SaveSystem.inventoryTurrets;
        buyItem = BuyTurret;
    }
    public void OpenMenu(DeviceItem item)
    {
        DeviceData data = item.GetData();
        SetMainProperties(data, item.isBought, 1);

        statsField.text = $"Special ability: {data.ability.ToString()}\n";
        statsField.text += $"Cooldown: {data.cooldown} sec\n";
        statsField.text += data.health == 0 ? string.Empty : $"Health: {data.health}\n";
        statsField.text += data.defence == 0 ? string.Empty : $"Defence: {data.defence * 100}%\n";
        statsField.text += data.repairStrength == 0 ? string.Empty : $"Repair: {data.repairStrength} hp/{data.cooldown} sec\n";

        chosenObject = item.gameObject;
        chosenDevice = item;
        toSell = SaveSystem.inventoryDevices;
        buyItem = BuyDevice;
    }
    public void OpenMenu(ModuleItem item)
    {
        ModuleData data = item.GetData();
        SetMainProperties(data, item.isBought, 2);

        statsField.text = data.damage == 0 ? string.Empty : $"Damage: {data.damage}\n";
        statsField.text += data.fireRate == 0 ? string.Empty : $"Fire rate: {data.fireRate}/sec\n";
        statsField.text += data.accuracy == 0 ? string.Empty : $"Accuracy: {data.accuracy * 100}%\n";
        statsField.text += data.critChance == 0 ? string.Empty : $"Crit chance: {data.critChance * 100}%\n";
        statsField.text += data.autoTargetForce == 0 ? string.Empty : $"Targeting force: {data.autoTargetForce * 100}%\n";
        statsField.text += data.shootsCount == 0 ? string.Empty : $"Shoots count: {data.shootsCount}\n";
        statsField.text += data.splashRadius == 0 ? string.Empty : $"Splash radius: {data.splashRadius}\n";
        statsField.text += data.bulletSpeed == 0 ? string.Empty : $"Bullet speed: {data.bulletSpeed}\n";

        chosenObject = item.gameObject;
        chosenModule = item;
        toSell = SaveSystem.inventoryModules;
        buyItem = BuyModule;
    }

    private void SetMainProperties(ItemData data, bool isBought, byte id)
    {
        gameObject.SetActive(true);
        ShowInterface(isBought, id);

        itemImage.sprite = data.sprite;
        outlineImage.sprite = data.sprite;
        moduleImage.color = transparentColor;

        nameField.text = data.name;
        nameField.color = data.outlineColor;
        descriptionField.text = data.description;
        priceField.text = isBought ? $"PRICE: {data.price}" : $"PRICE: {data.price * 3}";
    }

    public void InstallTurret()
    {
        Close();
        turretPlaces.SetActive(true);
        inventory.ChooseItem(chosenTurret);
        Time.timeScale = 0;
    }

    public void InstallDevice()
    {
        Close();
        devicePlaces.SetActive(true);
        inventory.ChooseItem(chosenDevice);
        Time.timeScale = 0;
    }

    public void InstallModule()
    {
        Close();
        turretPlaces.SetActive(true);
        inventory.ChooseItem(chosenModule);
        Time.timeScale = 0;
    }

    public void Sell()
    {
        PlayerEconomy.Instance.AddCoins(chosenObject.GetComponent<InventoryItem>().GetData().price);
        toSell[chosenObject.GetComponent<InventoryItem>().itemIndex] = null;
        Destroy(chosenObject);
        Close();
    }

    public void Close()
    {
        nameField.text = string.Empty;
        descriptionField.text = string.Empty;
        statsField.text = string.Empty;
        outlineImage.color = Color.white;

        installTurretButton.SetActive(false);
        installDeviceButton.SetActive(false);
        installModuleButton.SetActive(false);

        gameObject.SetActive(false);
    }

    private void BuyTurret() { inventory.AddItem(chosenTurret.GetData(), true); }
    private void BuyDevice() { inventory.AddItem(chosenDevice.GetData(), true); }
    private void BuyModule() { inventory.AddItem(chosenModule.GetData(), true); }

    public void Buy()
    {
        if(PlayerEconomy.Instance.RemoveCoins(chosenObject.GetComponent<InventoryItem>().GetData().price * 3))
            buyItem();

        Close();
    }

    private void ShowInterface(bool isBought, byte id)
    {
        buyButton.SetActive(!isBought);
        sellButton.SetActive(isBought);

        switch (id)
        {
            case 0:
                installTurretButton.SetActive(isBought);
                break;
            case 1:
                installDeviceButton.SetActive(isBought);
                break;
            case 2:
                installModuleButton.SetActive(isBought);
                break;

            default: break;
        }

        menuCurrentPos.y = isBought ? menuStartPos.y : menuStartPos.y + 10;
        menuTransform.position = menuCurrentPos;
    }

    private void ShowModule(TurretData data)
    {
        if (data.module != null)
        {
            moduleImage.sprite = data.module.sprite;
            moduleImage.color = Color.white;
            outlineImage.color = data.outlineColor;
        }
        else
            moduleImage.color = transparentColor;
    }
}