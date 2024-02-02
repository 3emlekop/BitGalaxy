using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    private readonly string[] itemTypes = new string[]
    {
        "Turrets", "Devices", "Modules"
    };
    private string itemsPath;

    private void Awake()
    {
        itemsPath = Path.Combine(Application.persistentDataPath, "Items");

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        CheckDirectories();
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

        /*
        File.WriteAllText(Path.Combine(itemsPath, itemTypes[0], "turret.json"), JsonUtility.ToJson(new TurretData()));
        File.WriteAllText(Path.Combine(itemsPath, itemTypes[1], "device.json"), JsonUtility.ToJson(new DeviceData()));
        File.WriteAllText(Path.Combine(itemsPath, itemTypes[2], "module.json"), JsonUtility.ToJson(new ModuleData()));
        */
    }

    public ItemData[] GetAllItems()
    {
        List<ItemData> items = new List<ItemData>();
        for(byte i = 0; i < itemTypes.Length; i++)
        {
            string[] files = Directory.GetFiles(Path.Combine(itemsPath, itemTypes[i]), "*.json");
            foreach(var file in files)
            {
                switch(i)
                {
                    case 0:
                        items.Add(JsonUtility.FromJson<TurretData>(File.ReadAllText(file)));
                        break;
                    case 1:
                        items.Add(JsonUtility.FromJson<DeviceData>(File.ReadAllText(file)));
                        break;
                    case 2:
                        items.Add(JsonUtility.FromJson<ModuleData>(File.ReadAllText(file)));
                        break;
                }
            }
        }
        return items.ToArray();
    }

    public Ship GetShip(string name)
    {
        return Resources.Load<GameObject>($"Prefabs/Ships/{name}").GetComponent<Ship>();
    }

    public Sprite GetSprite(string path, string itemName)
    {
        return Resources.Load<Sprite>(Path.Combine("Textures", path, itemName));
    }

    public Projectile GetProjectile(string name)
    {
        return Resources.Load<GameObject>("Prefabs/Projectiles/" + name).GetComponent<Projectile>();
    }

    public GameObject GetAbilityObject(string name)
    {
        return Resources.Load<GameObject>("Prefabs/AbilityItems/" + name);
    }

    public TurretData GetTurret(string name)
    {
        if(name == string.Empty)
            return null;

        return JsonUtility.FromJson<TurretData>(File.ReadAllText(Path.Combine(itemsPath, itemTypes[0], name)));
    }

    public DeviceData GetDevice(string name)
    {
        if(name == string.Empty)
            return null;

        return JsonUtility.FromJson<DeviceData>(File.ReadAllText(Path.Combine(itemsPath, itemTypes[1], name)));
    }

    public ModuleData GetModule(string name)
    {
        if(name == string.Empty)
            return null;

        return JsonUtility.FromJson<ModuleData>(File.ReadAllText(Path.Combine(itemsPath, itemTypes[2], name)));
    }
}
