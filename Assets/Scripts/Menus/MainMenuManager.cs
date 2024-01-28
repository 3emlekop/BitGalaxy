using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private CameraMover cameraMover;

    private readonly string[] socials =
    {
        "https://www.youtube.com/@Reejen", //YouTube
        "https://www.patreon.com/user?u=98907575", //Patreon
        "https://discord.gg/JkRNEqxbAW" //Discord
    };

    private Vector2 saveMenuPos = new Vector2(0, 10);

    public void Load()
    {
        saveManager.UpdateSaveSlots();
        cameraMover.MoveToPoint(saveMenuPos);
    }

    public void NewGame()
    {
        saveManager.CreateNewSaveFile("New game", false);
        saveManager.LoadSaveFile("New game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SaveParser.ClearSaveFiles();
    }

    public void FollowSocials(int socialId)
    {
        Application.OpenURL(socials[socialId]);
        if (PlayerPrefs.HasKey($"social{socialId}") == false)
            PlayerPrefs.SetString($"social{socialId}", "visited");
    }
}
