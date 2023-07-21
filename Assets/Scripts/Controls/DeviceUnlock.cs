using TMPro;
using UnityEngine;

public class DeviceUnlock : MonoBehaviour
{
    [SerializeField] private GameObject offerMenu;
    [SerializeField] private GameObject[] buyDeviceButtons;
    [SerializeField] private GameObject[] devicePlaces;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private byte[] prices = new byte[3];
    private byte chosenDeviceIndex = 0;

    private void Start()
    {
        foreach(var place in devicePlaces)
            place.SetActive(false);

        for (byte i = 0; i < buyDeviceButtons.Length; i++)
            if (PlayerPrefs.HasKey("device" + i.ToString()))
            {
                buyDeviceButtons[i].SetActive(false);
                devicePlaces[i].SetActive(true);
            }
    }

    public void OpenOfferMenu(int slotIndex)
    {
        priceText.text = $"PRICE: {prices[slotIndex]}";
        offerMenu.SetActive(true);
        chosenDeviceIndex = (byte)slotIndex;
    }

    public void CloseOfferMenu()
    {
        priceText.text = string.Empty;
        offerMenu.SetActive(false);
    }

    public void Buy()
    {
        if (PlayerEconomy.Instance.RemoveCrystals(prices[chosenDeviceIndex]))
        {
            buyDeviceButtons[chosenDeviceIndex].SetActive(false);
            PlayerPrefs.SetString("device" + chosenDeviceIndex.ToString(), "bought");
            devicePlaces[chosenDeviceIndex].SetActive(true);
            CloseOfferMenu();
        }
    }
}
