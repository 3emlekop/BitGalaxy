[System.Serializable]
public class FractionData
{
    public string Name { get; private set; }
    private byte[] rgb = new byte[3];
    public byte[] RGB => rgb;
    public byte IconId { get; private set; }

    public FractionData(string name, byte[] color, byte iconId)
    {
        Name = name;
        if(color.Length == 3)
            rgb = color;
        IconId = iconId;
    }

    public FractionData()
    {
        Name = "Trader";
        IconId = 0;
    }
}
