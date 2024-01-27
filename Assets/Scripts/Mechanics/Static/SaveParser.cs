using System;
using System.IO;
using UnityEngine;

public class SaveParser : MonoBehaviour
{
    private static readonly string DefaultSavePath = Path.Combine(Application.persistentDataPath, "SaveFiles");

    private static void CheckDirectory()
    {
        if(Directory.Exists(DefaultSavePath) == false)
            Directory.CreateDirectory(DefaultSavePath);
    }

    public static void SaveToJson(Save save)
    {
        File.WriteAllText(Path.Combine(DefaultSavePath, $"{save.name}.json"), JsonUtility.ToJson(save));
    }

    public static Save CreateNewSaveFile(string name)
    {
        var newSave = new Save(name);
        SaveToJson(newSave);
        return newSave;
    }

    public static Save LoadFromJson(string fileName)
    {
        return JsonUtility.FromJson<Save>(File.ReadAllText(Path.Combine(DefaultSavePath, fileName)));
    }

    public static string[] LoadSaveNames(byte maxSaveSlotCount)
    {
        CheckDirectory();
        string[] paths = Directory.GetFiles(DefaultSavePath, "*.json");
        string[] fileNames = new string[maxSaveSlotCount];

        for (byte j = 0; j < paths.Length; j++)
            fileNames[j] = Path.GetFileNameWithoutExtension(paths[j]);
        return fileNames;
    }

    public static void DeleteSaveFile(byte id)
    {
        string[] files = Directory.GetFiles(DefaultSavePath, "*.json");
        if(files.Length == 0)
            Debug.LogError("Save directory doesn't contain any files to delete");

        try
        {
            File.Delete(files[id]);
        }
        catch (Exception exception)
        {
            Debug.LogError($"Deleting save file has thrown an exception: {exception}");
        }
    }

    public static void ClearSaveFiles()
    {
        try
        {
            Directory.Delete(DefaultSavePath, true);
        }
        catch (Exception exception)
        {
            Debug.LogError($"Deleting directory has thrown an exception: {exception}");
        }
    }
}