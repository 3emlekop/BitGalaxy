using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public Save SaveData { get; private set; }

    private static readonly Dictionary<SaveParser.GameMode, byte> gameModeSceneIds = new Dictionary<SaveParser.GameMode, byte>
    {
        {SaveParser.GameMode.Campaign, 1},
        {SaveParser.GameMode.Classic, 3},
        {SaveParser.GameMode.Endless, 5},
        {SaveParser.GameMode.Challenge, 6},
        {SaveParser.GameMode.Sandbox, 7},
    };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static int GetGameModeSceneId(SaveParser.GameMode gameMode)
    {
        return gameModeSceneIds[gameMode];
    }

    public static void Load(SaveParser.GameMode gameMode, string saveName)
    {
        SceneManager.LoadScene(gameModeSceneIds[gameMode]);
        instance.SaveData = SaveParser.LoadFromJson(saveName, gameMode);
    }

    public static void Load(SaveParser.GameMode gameMode)
    {
        SceneManager.LoadScene(gameModeSceneIds[gameMode]);
        instance.SaveData = SaveParser.LoadFromJson("save", gameMode);
    }
}
