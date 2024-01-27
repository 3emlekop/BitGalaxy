using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameChooser : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private CameraMover cameraMover;


    private readonly string CurrentGameModeSaveKey = "CurrentGameMode";
    private byte currentGameMode;
    private Vector2 saveMenuPos = new Vector2(0, 10);

    public void Load()
    {
        saveManager.UpdateButtons();
        cameraMover.MoveToPoint(saveMenuPos);
    }

    public void Continue()
    {
        saveManager.SelectSaveSlot(0);
        saveManager.LoadSelectedSaveFile();
    }

    public void NewGame()
    {
        
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(CurrentGameModeSaveKey, currentGameMode);
    }
}