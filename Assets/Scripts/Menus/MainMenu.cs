using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    readonly static string[] socials =
    {
        "https://www.youtube.com/channel/UC2CgQJWCB1065NjZHAiMl4Q", //YouTube
        "https://www.patreon.com/user?u=98907575", //Patreon
        "https://discord.gg/JkRNEqxbAW" //Discord
    };

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SaveGame(int saveId)
    {
        
    }

    public void LoadGame(int saveId)
    {

    }

    public void LoadLevel(int levelId)
    {
        SceneManager.LoadScene(levelId);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SaveManager.ClearSaveFiles();
    }

    public void FollowSocials(int socialId)
    {
        Application.OpenURL(socials[socialId]);
        if (PlayerPrefs.HasKey($"social{socialId}") == false)
            PlayerPrefs.SetString($"social{socialId}", "visited");
    }
}
