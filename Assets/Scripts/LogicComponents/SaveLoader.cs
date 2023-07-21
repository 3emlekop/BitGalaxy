using UnityEngine;

public sealed class SaveLoader : MonoBehaviour
{
    public static SaveLoader instance { get; private set; }
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        if (instance == null)
        {
            instance = this;
            SaveSystem.Load();
        }
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this);
    }

    private void OnApplicationQuit()
    {
        SaveSystem.Save();
    }
}
