using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    public Dictionary<string, GameObject> shipPrefabs = new Dictionary<string, GameObject>();
    public List<ItemData> items = new List<ItemData>();

    private readonly string[] itemTypes = new string[]
    {
        "Turrets", "Devices", "Modules"
    };
    private GameObject loadedShip;
    private string itemsPath;

    void Awake()
    {
        itemsPath = Path.Combine(Application.persistentDataPath, "Items");

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadShipPrefabs();
        DontDestroyOnLoad(gameObject);
        CheckDirectories();
        LoadItemsData();
    }

    private void CheckDirectories()
    {
        if(!Directory.Exists(itemsPath))
            Directory.CreateDirectory(itemsPath);

        for(byte i = 0; i < itemTypes.Length; i++)
        {
            if(!Directory.Exists(Path.Combine(itemsPath, itemTypes[i])))
                Directory.CreateDirectory(Path.Combine(itemsPath, itemTypes[i]));
        }

        File.WriteAllText(Path.Combine(itemsPath, itemTypes[0], "turret.json"), JsonUtility.ToJson(new TurretData()));
        File.WriteAllText(Path.Combine(itemsPath, itemTypes[1], "device.json"), JsonUtility.ToJson(new DeviceData()));
        File.WriteAllText(Path.Combine(itemsPath, itemTypes[2], "module.json"), JsonUtility.ToJson(new ModuleData()));
    }

    private void LoadItemsData()
    {
        for(byte i = 0; i < itemTypes.Length; i++)
        {
            string[] files = Directory.GetFiles(Path.Combine(itemsPath, itemTypes[i]), "*.json");
            foreach(var file in files)
                items.Add(JsonUtility.FromJson<ItemData>(file));
        }
    }

    private void LoadShipPrefabs()
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
