using UnityEngine;

[System.Serializable]
public class FractionData
{
    public string Name { get; private set; }
    [SerializeField] private byte[] rgb = new byte[3] { 0, 0, 0 };
    public byte[] RGB => rgb;
    [SerializeField] private byte iconId;

    public byte IconId
    {
        get { return iconId; }
        private set { iconId = value; }
    }

    public FractionData(string name, byte[] color, byte iconId)
    {
        Name = name;
        if (color.Length == 3)
            rgb = color;
        IconId = iconId;
    }

    public FractionData()
    {
        Name = "Trader";
        IconId = 0;
    }
}