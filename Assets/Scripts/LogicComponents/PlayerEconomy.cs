using TMPro;
using UnityEngine;

public class PlayerEconomy : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] coinsText = new TextMeshProUGUI[2];
    [SerializeField] private TextMeshProUGUI[] crystalsText = new TextMeshProUGUI[2];
    public int Coins { get; private set; }
    public int Crystals { get; private set; }

    public static PlayerEconomy Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        if (PlayerPrefs.HasKey("coins"))
            Coins = PlayerPrefs.GetInt("coins");
        else
            Coins = 150;

        if(PlayerPrefs.HasKey("crystals"))
            Crystals = PlayerPrefs.GetInt("crystals");
        else
            Crystals = 10;

        UpdateStats();
    }

    public void AddCoins(int value)
    {
        if (value < 0)
            return;

        Coins += value;
        PlayerPrefs.SetInt("coins", Coins);
        UpdateStats();
    }

    public bool RemoveCoins(int value)
    {
        if (Coins - value >= 0)
        {
            Coins -= value;
            PlayerPrefs.SetInt("coins", Coins);
            UpdateStats();
            return true;
        }
        return false;
    }

    public void AddCrystals(int value)
    {
        if (value < 0)
            return;

        Crystals += value;
        PlayerPrefs.SetInt("crystals", Crystals);
        UpdateStats();
    }

    public bool RemoveCrystals(int value)
    {
        if (Crystals - value >= 0)
        {
            Crystals -= value;
            PlayerPrefs.SetInt("crystals", Crystals);
            UpdateStats();
            return true;
        }
        return false;
    }

    public void UpdateStats()
    {
        for (byte i = 0; i < 2; i++)
        {
            coinsText[i].text = NumberConvert(Coins);
            crystalsText[i].text = NumberConvert(Crystals);
        }
    }

    private string NumberConvert(int value)
    {
        if (value >= 1000 && value < 1000000)
            return ((float)value / 1000).ToString() + "K";
        else if (value > 1000000)
            return ((float)value / 1000000).ToString() + "M";
        else return value.ToString();
    }
}
