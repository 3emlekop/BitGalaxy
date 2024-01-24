using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMenu : MonoBehaviour
{
    [SerializeField] private Transform mapCanvas;
    [SerializeField] private float minScale = 0.4f;
    [SerializeField] private float maxScale = 1.6f;

    private float zoomFactor = 0.3f;

    private Vector3 currentScale = Vector3.one;

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartCampaignFight()
    {
        SceneManager.LoadScene(LevelManager.GetGameModeSceneId(SaveParser.GameMode.Campaign) + 1);
    }

    public void StartClassicFight()
    {
        SceneManager.LoadScene(LevelManager.GetGameModeSceneId(SaveParser.GameMode.Classic) + 1);
    }

    public void ZoomOutMap()
    {
        currentScale = mapCanvas.localScale;
        currentScale.x -= zoomFactor;
        currentScale.y -= zoomFactor;

        if (currentScale.x < minScale)
        {
            currentScale.x = minScale;
            currentScale.y = minScale;
        }

        mapCanvas.localScale = currentScale;
    }

    public void ZoomInMap()
    {
        currentScale = mapCanvas.localScale;
        currentScale.x += zoomFactor;
        currentScale.y += zoomFactor;

        if (currentScale.x > maxScale)
        {
            currentScale.x = maxScale;
            currentScale.y = maxScale;
        }

        mapCanvas.localScale = currentScale;
    }

    public void CenterMap()
    {

    }
}
