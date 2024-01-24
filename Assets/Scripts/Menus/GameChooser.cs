using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameChooser : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image gameModePreviewImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private SaveManager saveMenu;
    [SerializeField] private CameraMover cameraMover;

    [Header("Values")]
    [SerializeField] private Sprite[] gameModeSprites;

    private string[] gameModeNames;
    private readonly string CurrentGameModeSaveKey = "CurrentGameMode";
    private byte currentGameMode;
    private Vector2 saveMenuPos = new Vector2(-10, 10);

    private void Start()
    {
        gameModeNames = Enum.GetNames(typeof(SaveParser.GameMode));
        currentGameMode = (byte)PlayerPrefs.GetInt(CurrentGameModeSaveKey);
        UpdateGameModePreview();
    }

    public void Next()
    {
        currentGameMode = currentGameMode + 1 >= gameModeNames.Length ? (byte)0 : (byte)(currentGameMode + 1);
        UpdateGameModePreview();
    }

    public void Prev()
    {
        currentGameMode = currentGameMode - 1 < 0 ? (byte)(gameModeNames.Length - 1) : (byte)(currentGameMode - 1);
        UpdateGameModePreview();
    }

    public void Play()
    {
        saveMenu.UpdateButtons();

        if (currentGameMode == (int)SaveParser.GameMode.Endless ||
            currentGameMode == (int)SaveParser.GameMode.Sandbox)
        {
            saveMenu.LoadSelectedSaveFile();
            return;
        }

        cameraMover.MoveToPoint(saveMenuPos);
    }

    private void UpdateGameModePreview()
    {
        if (currentGameMode >= gameModeSprites.Length)
            gameModePreviewImage.sprite = null;
        else
            gameModePreviewImage.sprite = gameModeSprites[currentGameMode];

        titleText.text = gameModeNames[currentGameMode];
        SaveManager.selectedGameMode = (SaveParser.GameMode)currentGameMode;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(CurrentGameModeSaveKey, currentGameMode);
    }
}