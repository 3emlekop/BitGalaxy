using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public enum GameMode
    {
        Campaign,
        Classic,
        Challenge,
        Sandbox,
        Endless,
    }

    private static readonly string DefaultSavePath = Path.Combine(Application.persistentDataPath, "SaveFiles");

    private static void InititializeSaveFiles()
    {
        if (!File.Exists(DefaultSavePath))
            Directory.CreateDirectory(DefaultSavePath);

        foreach (string gameModeName in Enum.GetNames(typeof(GameMode)))
            if (!File.Exists(Path.Combine(DefaultSavePath, gameModeName)))
                Directory.CreateDirectory(Path.Combine(DefaultSavePath, gameModeName));
    }

    public static void SaveToJson(Save save, GameMode gameMode)
    {
        File.WriteAllText(Path.Combine(DefaultSavePath, Enum.GetName(typeof(GameMode), gameMode), $"{save.name}.json"), JsonUtility.ToJson(save));
    }

    public static Save CreateNewJsonSave(string name, GameMode gameMode)
    {
        Save newSave = null;
        switch (gameMode)
        {
            case GameMode.Campaign:
                newSave = new CampaignSave(name);
                break;
            case GameMode.Classic:
                newSave = new ClassicSave(name);
                break;
            case GameMode.Challenge:
                return null;
            case GameMode.Sandbox:
                return null;
        }
        File.WriteAllText(Path.Combine(DefaultSavePath, Enum.GetName(typeof(GameMode), gameMode), $"{name}.json"), JsonUtility.ToJson(newSave));
        return newSave;
    }

    public static Save[][] LoadSaveMatrixFromJson()
    {
        InititializeSaveFiles();
        List<Save[]> gameModeSaves = new List<Save[]>();
        for (byte i = 0; i < Enum.GetValues(typeof(GameMode)).Length; i++)
        {
            string gameModeSaveDirectory = Path.Combine(DefaultSavePath, Enum.GetName(typeof(GameMode), (GameMode)i));

            if (!Directory.Exists(gameModeSaveDirectory))
                Debug.LogError($"Directory not found: {gameModeSaveDirectory}");

            string[] files = Directory.GetFiles(gameModeSaveDirectory, "*.json");
            Save[] savesArray = new Save[SaveMenu.instance.maxSaveSlotCount];
            if (files.Length == 0)
            {
                gameModeSaves.Add(savesArray);
                continue;
            }
            for(byte j = 0; j < files.Length; j++)
                savesArray[j] = JsonUtility.FromJson<Save>(File.ReadAllText(files[j]));
            gameModeSaves.Add(savesArray);
        }
        return gameModeSaves.ToArray();
    }

    public static void DeleteSaveFile(byte id, GameMode gameMode)
    {
        string gameModeSaveDirectory = Path.Combine(DefaultSavePath, Enum.GetName(typeof(GameMode), gameMode));

        if (!Directory.Exists(gameModeSaveDirectory))
            Debug.LogError($"Directory not found: {gameModeSaveDirectory}");

        string[] files = Directory.GetFiles(gameModeSaveDirectory, "*.json");
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