using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private readonly string[] socials =
    {
        "https://www.youtube.com/@Reejen", //YouTube
        "https://www.patreon.com/user?u=98907575", //Patreon
        "https://discord.gg/JkRNEqxbAW" //Discord
    };

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(int levelId)
    {
        SceneManager.LoadScene(levelId);
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
