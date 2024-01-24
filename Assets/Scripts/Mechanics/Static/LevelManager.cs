using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static byte SaveSlotId { get; private set; }
    public static Save SaveData { get; private set; }

    private static Dictionary<SaveManager.GameMode, byte> gameModeSceneIds = new Dictionary<SaveManager.GameMode, byte>
    {
        {SaveManager.GameMode.Campaign, 1},
        {SaveManager.GameMode.Classic, 3},
        {SaveManager.GameMode.Endless, 5},
        {SaveManager.GameMode.Challenge, 6},
        {SaveManager.GameMode.Sandbox, 7},
    };

    public static int GetGameModeSceneId(SaveManager.GameMode gameMode)
    {
        return gameModeSceneIds[gameMode];
    }

    public static void Load(SaveManager.GameMode gameMode, byte saveSlotId, Save saveData)
    {
        SceneManager.LoadScene(gameModeSceneIds[gameMode]);
        SaveSlotId = saveSlotId;
        SaveData = saveData;
    }

    public static void Load(SaveManager.GameMode gameMode)
    {
        SceneManager.LoadScene(gameModeSceneIds[gameMode]);
    }
}
