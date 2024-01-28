using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private Dictionary<string, byte> sceneIdsDictionary = new Dictionary<string, byte>
    {
        {"MainMenu", 0},
        {"Hub", 1},
        {"Level", 2}
    };

    public static LevelLoader instance;

    public Save Save { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(string name, string saveName)
    {
        if (sceneIdsDictionary.ContainsKey(name))
        {
            Save = SaveParser.LoadFromJson(saveName);
            SceneManager.LoadScene(sceneIdsDictionary[name]);
        }
        else
            Debug.LogWarning($"There is no key {name} in scene IDs dictionary");
    }

    public void LoadLevel(string name)
    {
        if (sceneIdsDictionary.ContainsKey(name))
        {
            Save = SaveParser.LoadFromJson(Save.name);
            SceneManager.LoadScene(sceneIdsDictionary[name]);
        }
        else
            Debug.LogWarning($"There is no key {name} in scene IDs dictionary");
    }
}
