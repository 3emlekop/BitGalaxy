using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static string SaveName { get; private set; }
    private Save saveData;

    private static Dictionary<SaveParser.GameMode, byte> gameModeSceneIds = new Dictionary<SaveParser.GameMode, byte>
    {
        {SaveParser.GameMode.Campaign, 1},
        {SaveParser.GameMode.Classic, 3},
        {SaveParser.GameMode.Endless, 5},
        {SaveParser.GameMode.Challenge, 6},
        {SaveParser.GameMode.Sandbox, 7},
    };

    public static int GetGameModeSceneId(SaveParser.GameMode gameMode)
    {
        return gameModeSceneIds[gameMode];
    }

    public static void Load(SaveParser.GameMode gameMode, string saveName)
    {
        SceneManager.LoadScene(gameModeSceneIds[gameMode]);
        SaveName = saveName;
    }

    public static void Load(SaveParser.GameMode gameMode)
    {
        SceneManager.LoadScene(gameModeSceneIds[gameMode]);
    }
}
