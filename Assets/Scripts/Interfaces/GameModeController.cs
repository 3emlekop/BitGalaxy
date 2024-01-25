using UnityEngine;

public class GameModeController : MonoBehaviour
{
    protected Save saveData;

    protected void Awake()
    {
        saveData = SceneLoader.instance.SaveData;
    }
}