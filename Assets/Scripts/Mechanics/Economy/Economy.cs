using TMPro;
using UnityEngine;

public class Economy : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] coinsText = new TextMeshProUGUI[4];
    [SerializeField] private TextMeshProUGUI[] crystalsText = new TextMeshProUGUI[4];
    public int Coins { get; private set; }
    public int Crystals { get; private set; }

    public static Economy Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        if (PlayerPrefs.HasKey("coins"))
            Coins = PlayerPrefs.GetInt("coins");
        else
            Coins = 999999;

        if(PlayerPrefs.HasKey("crystals"))
            Crystals = PlayerPrefs.GetInt("crystals");
        else
            Crystals = 999999;

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
        for (byte i = 0; i < coinsText.Length; i++)
        {
            coinsText[i].text = NumberConvert(Coins);
            crystalsText[i].text = NumberConvert(Crystals);
        }
    }

    public string NumberConvert(int value)
    {
        if (value >= 1000 && value < 1000000)
        {
            string s = ((float)value / 1000).ToString();
            return s.Length > 5 ? s.Remove(5) + "K" : s + "K"; 
        }
        else if (value > 1000000)
        {
            string s = ((float)value / 1000000).ToString();
            return s.Length > 5 ? s.Remove(5) + "M" : s + "M";
        }
        else return value.ToString();
    }
}
