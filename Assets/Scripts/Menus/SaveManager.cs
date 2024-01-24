using System;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private Transform contentParent;
    [SerializeField] private Button saveSlotButtonPrefab;
    [SerializeField] private Button deleteButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Transform newSaveSlotButtonTransform;
    [SerializeField] private GameObject slotNamingMenu;
    [SerializeField] private TMP_InputField saveFileNameInput;
    [SerializeField] public byte maxSaveSlotCount = 10;

    public static SaveManager instance;
    public static SaveParser.GameMode selectedGameMode;
    private byte selectedSaveSlot = new byte();
    private string[][] saveNames;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);

        UpdateButtons();

        GameObject button;
        newSaveSlotButtonTransform.SetParent(null);
        for (byte i = 0; i < maxSaveSlotCount; i++)
        {
            button = Instantiate(saveSlotButtonPrefab, Vector2.zero, Quaternion.identity, contentParent).gameObject;
            button.GetComponent<SaveSlotButton>().SaveId = i;
            button.SetActive(false);
        }
        newSaveSlotButtonTransform.SetParent(contentParent);
    }

    public void UpdateButtons()
    {
        saveNames = SaveParser.LoadSaveNames(maxSaveSlotCount);

        selectedSaveSlot = 0;
        deleteButton.interactable = false;
        loadButton.interactable = false;

        for(byte i = 0; i < contentParent.childCount; i++)
        {
            contentParent.GetChild(i).GetComponent<Button>().interactable = true;
            contentParent.GetChild(i).gameObject.SetActive(false);
        }
        contentParent.GetChild(contentParent.childCount-1).gameObject.SetActive(true);

        if (saveNames[(int)selectedGameMode] == null)
            return;

        GameObject button;
        for (byte i = 0; i < saveNames[(int)selectedGameMode].Length; i++)
        {
            if(saveNames[(int)selectedGameMode][i] == null)
                continue;

            button = contentParent.GetChild(i).gameObject;
            button.GetComponent<SaveSlotButton>().SlotName.text = saveNames[(int)selectedGameMode][i];
            button.SetActive(true);
        }
    }

    public void LoadSelectedSaveFile()
    {
        if(saveNames[(int)selectedGameMode] == null)
            LevelManager.Load(selectedGameMode);
        else
            LevelManager.Load(selectedGameMode, saveNames[(int)selectedGameMode][selectedSaveSlot]);
    }

    public void OpenSaveFileCreateMenu()
    {
        for (byte i = 0; i < contentParent.childCount; i++)
        {
            if (contentParent.GetChild(i).gameObject.activeSelf == false)
            {
                slotNamingMenu.SetActive(true);
                selectedSaveSlot = i;
                return;
            }
        }
        //not enough space popup
    }

    private void CreateNewSaveFile()
    {
        contentParent.GetChild(selectedSaveSlot).gameObject.SetActive(true);
        contentParent.GetChild(selectedSaveSlot).GetComponent<SaveSlotButton>().SlotName.text = saveFileNameInput.text;
        saveNames[(int)selectedGameMode][selectedSaveSlot] = SaveParser.CreateNewJsonSave(saveFileNameInput.text, selectedGameMode).name;
    }

    public void ApplyInputText()
    {
        for(byte i = 0; i < saveNames[(int)selectedGameMode].Length; i++)
        {
            if(saveNames[(int)selectedGameMode][i] == null)
                break;

            if(saveFileNameInput.text == saveNames[(int)selectedGameMode][i])
                return;
        }

        if (saveFileNameInput.text.Length > 0 && !saveFileNameInput.text.Any(Path.GetInvalidFileNameChars().Contains))
        {
            slotNamingMenu.SetActive(false);
            CreateNewSaveFile();
            saveFileNameInput.text = string.Empty;
        }
    }

    public void DeleteSelectedSaveFile()
    {
        contentParent.GetChild(selectedSaveSlot).gameObject.SetActive(false);
        saveNames[(int)selectedGameMode][selectedSaveSlot] = null;
        SaveParser.DeleteSaveFile(selectedSaveSlot, selectedGameMode);
        UpdateButtons();
    }

    public static void SelectSaveSlot(int id)
    {
        if (id < 0)
            Debug.LogError("Selected save slot ID is less than 0. Provided ID value: " + id);

        if (id > instance.maxSaveSlotCount)
            Debug.LogError("Id value is bigger than max slots count. Provided ID value: " + id + ", max slots count value: " + instance.maxSaveSlotCount);

        instance.selectedSaveSlot = (byte)id;
        foreach (Transform button in instance.contentParent)
            button.GetComponent<Button>().interactable = true;
        instance.contentParent.GetChild(id).GetComponent<Button>().interactable = false;
        instance.deleteButton.interactable = true;
        instance.loadButton.interactable = true;
    }
}
