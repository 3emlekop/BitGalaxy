using System;

[Serializable]
public class Save
{
    public string name;
    public FractionData fractionData;
    public ShipData playerShip;

    public Save(string name)
    {
        this.name = name;
        fractionData = new FractionData();
        playerShip = new ShipData(fractionData, 0);
    }
}