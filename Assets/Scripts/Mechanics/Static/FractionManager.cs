using UnityEngine;

public class FractionManager : MonoBehaviour
{
    private static readonly FractionData[] defaultFractions = new FractionData[3]
        {
            new FractionData("Trader", new byte[3] {0,0,0}, 1),
            new FractionData("Steel", new byte[3] {0,0,0}, 2),
            new FractionData("Evolution", new byte[3] {0,0,0}, 3),
        };

    //private FractionData[] userFractoins;

    public static string[] GetDefaultFractionNames()
    {
        return new string[3]
        {
            defaultFractions[0].Name,
            defaultFractions[1].Name,
            defaultFractions[2].Name,
        };
    }
}
