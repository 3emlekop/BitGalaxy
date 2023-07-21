using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static DeviceData[] placedDevices = new DeviceData[3];
    public static TurretData[] placedTurrets = new TurretData[4];

    public static TurretData[] inventoryTurrets = new TurretData[100];
    public static DeviceData[] inventoryDevices = new DeviceData[100];
    public static ModuleData[] inventoryModules = new ModuleData[100];

    public static void Save()
    {
        string saveFolderPath = Path.Combine(Application.persistentDataPath, "savedgame");
        if (!Directory.Exists(saveFolderPath))
            Directory.CreateDirectory(saveFolderPath);

        string[] savePaths = new string[]
        {
            Path.Combine(saveFolderPath, "inventory/turrets"),
            Path.Combine(saveFolderPath, "inventory/devices"),
            Path.Combine(saveFolderPath, "inventory/modules"),
            Path.Combine(saveFolderPath, "turrets"),
            Path.Combine(saveFolderPath, "devices")
        };

        foreach (string path in savePaths)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        BinaryFormatter formatter = new BinaryFormatter();

        for (byte i = 0; i < savePaths.Length; i++)
        {
            byte length = 0;
            object[] toSave = null;
            switch (i)
            {
                case 0:
                    length = (byte)inventoryTurrets.Length;
                    toSave = Formatter<TurretData>.ArrayCompress(inventoryTurrets, true);
                    break;
                case 1:
                    length = (byte)inventoryDevices.Length;
                    toSave = Formatter<DeviceData>.ArrayCompress(inventoryDevices, true);
                    break;
                case 2:
                    length = (byte)inventoryModules.Length;
                    toSave = Formatter<ModuleData>.ArrayCompress(inventoryModules, true);
                    break;
                case 3:
                    length = (byte)placedTurrets.Length;
                    toSave = placedTurrets;
                    break;
                case 4:
                    length = (byte)placedDevices.Length;
                    toSave = placedDevices;
                    break;
                default: break;
            }

            for (byte j = 0; j < length; j++)
            {
                FileStream stream = File.Create(String.Concat(savePaths[i], "/item", j.ToString(), ".txt"));
                if (toSave != null)
                {
                    formatter.Serialize(stream, JsonUtility.ToJson(toSave[j]));
                    if (toSave[j] != null) // temp
                        Debug.Log("SAVED: " + toSave[j].ToString()); // temp
                }
                stream.Close();
            }
        }
    }

    public static void Load()
    {
        string saveFolderPath = Path.Combine(Application.persistentDataPath, "savedgame");
        if (!Directory.Exists(saveFolderPath))
            return;

        string[] savePaths = new string[]
        {
            Path.Combine(saveFolderPath, "inventory/turrets"),
            Path.Combine(saveFolderPath, "inventory/devices"),
            Path.Combine(saveFolderPath, "inventory/modules"),
            Path.Combine(saveFolderPath, "turrets"),
            Path.Combine(saveFolderPath, "devices")
        };

        foreach (string path in savePaths)
        {
            if (!Directory.Exists(path))
                return;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        for (byte i = 0; i < savePaths.Length; i++)
        {
            byte length = 0;
            object[] toOverwrite = null;
            switch (i)
            {
                case 0:
                    length = (byte)inventoryTurrets.Length;
                    toOverwrite = inventoryTurrets;
                    break;
                case 1:
                    length = (byte)inventoryTurrets.Length;
                    toOverwrite = inventoryDevices;
                    break;
                case 2:
                    length = (byte)inventoryTurrets.Length;
                    toOverwrite = inventoryModules;
                    break;
                case 3:
                    length = (byte)placedTurrets.Length;
                    toOverwrite = placedTurrets;
                    break;
                case 4:
                    length = (byte)placedDevices.Length;
                    toOverwrite = placedDevices;
                    break;
                default: break;
            }

            for (byte j = 0; j < length; j++)
            {
                FileStream stream = File.Open(String.Concat(savePaths[i], "/item", j.ToString(), ".txt"), FileMode.Open);
                string json = (string)formatter.Deserialize(stream);

                if (json.Length < 30)
                {
                    stream.Close();
                    continue;
                }

                Debug.Log("LOADED: " + json);
                if (toOverwrite != null)
                {
                    toOverwrite[j] = toOverwrite is TurretData[] ? ScriptableObject.CreateInstance<TurretData>() :  
                    toOverwrite is DeviceData[] ? ScriptableObject.CreateInstance<DeviceData>() : 
                    toOverwrite is ModuleData[] ? ScriptableObject.CreateInstance<ModuleData>() : null;
                    JsonUtility.FromJsonOverwrite(json, toOverwrite[j]);
                }
                stream.Close();
            }
        }
    }

    /// <summary>
    /// Adds new item to saved items without affecting the reserved for placed items slots.
    /// </summary>
    /// <param name="item"></param>
    public static byte AddItem(ItemData item)
    {
        if (item == null)
            return 0;

        object[] toAdd = null;

        toAdd = item is TurretData ? inventoryTurrets :
        item is DeviceData ? inventoryDevices :
        item is ModuleData ? inventoryModules : null;

        for (byte i = 0; i < toAdd.Length; i++)
        {
            if (toAdd[i] == null)
            {
                toAdd[i] = item;
                return i;
            }
        }
        return 0;
    }
}
