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

        File.WriteAllText(Path.Combine(itemsPath, itemTypes[0], "turret.json"), JsonUtility.ToJson(new TurretData()));
        File.WriteAllText(Path.Combine(itemsPath, itemTypes[1], "device.json"), JsonUtility.ToJson(new DeviceData()));
        File.WriteAllText(Path.Combine(itemsPath, itemTypes[2], "module.json"), JsonUtility.ToJson(new ModuleData()));
    }

    public ItemData[] GetAllItems()
    {
        List<ItemData> items = new List<ItemData>();
        for(byte i = 0; i < itemTypes.Length; i++)
        {
            string[] files = Directory.GetFiles(Path.Combine(itemsPath, itemTypes[i]), "*.json");
            foreach(var file in files)
                items.Add(JsonUtility.FromJson<ItemData>(file));
        }
        return items.ToArray();
    }

    public Ship GetShip(string name)
    {
        return Resources.Load<GameObject>($"Prefabs/Ships/{name}").GetComponent<Ship>();
    }

    public Sprite GetSprite(string path)
    {
        return Resources.Load<Sprite>(Path.Combine("Textures", path));
    }

    public Projectile GetProjectile(string name)
    {
        return Resources.Load<GameObject>("Prefabs/Projectiles/" + name).GetComponent<Projectile>();
    }

    public TurretData GetTurret(string name)
    {
        return JsonUtility.FromJson<TurretData>(Path.Combine(itemsPath, itemTypes[0], name));
    }

    public DeviceData GetDevice(string name)
    {
        return JsonUtility.FromJson<DeviceData>(Path.Combine(itemsPath, itemTypes[1], name));
    }

    public ModuleData GetModule(string name)
    {
        return JsonUtility.FromJson<ModuleData>(Path.Combine(itemsPath, itemTypes[2], name));
    }
}
