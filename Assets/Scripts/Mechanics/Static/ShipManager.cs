using UnityEngine;
using System.Collections.Generic;
using System;

public class ShipManager : MonoBehaviour
{
    public static ShipManager instance;

    public Dictionary<string, GameObject> shipPrefabs = new Dictionary<string, GameObject>();

    private GameObject loadedShip;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        LoadShipPrefabs();
    }

    void LoadShipPrefabs()
    {
        foreach (var fraction in FractionManager.GetDefaultFractionNames())
        {
            for (byte i = 0; i < 9; i++)
            { 
                if(i > 6 && fraction == "Steel")
                    break;

                try
                {
                    loadedShip = Resources.Load<GameObject>($"Prefabs/Ships/{fraction}Ship{(i < 10 ? "0" + i : i)}");
                    shipPrefabs[loadedShip.name] = loadedShip;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to load ship prefab: Prefabs/Ships/{fraction}Ship{(i < 10 ? "0" + i : i)}. Error: {e.Message}");
                }
            }
        }
    }

    public GameObject InstantiateShip(ShipData shipData)
    {
        if (shipData == null)
            throw new Exception("\'shipData\' argument for InstantiateShip() in ShipManager class is null");

        if (shipPrefabs.ContainsKey(shipData.ShipId))
        {
            GameObject shipPrefab = shipPrefabs[shipData.ShipId];
            GameObject instantiatedShip = Instantiate(shipPrefab, Vector3.zero, Quaternion.identity);

            // Apply ship data properties to the instantiated ship

            return instantiatedShip;
        }
        else
        {
            Debug.LogError($"No prefab found for ship ID: {shipData?.ShipId}");
            return null;
        }
    }
}
