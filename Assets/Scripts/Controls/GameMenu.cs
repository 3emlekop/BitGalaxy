using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private LevelLoader loader;
    public static GameMenu instance;

    private void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }
    public void Exit()
    {
        loader.LoadLevel(0);
        gameObject.SetActive(false);
    }

    public void Continue()
    {
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        gameManager.RespawnPlayer();
        gameObject.SetActive(false);
    }

    public void Pause(bool isTimeStop)
    {
        gameObject.SetActive(true);
        Time.timeScale = isTimeStop ? 0 : 0.2f;
        continueButton.SetActive(isTimeStop);

        if(ScorePointer.instance != null)
            ScorePointer.instance.UpdateStats();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
