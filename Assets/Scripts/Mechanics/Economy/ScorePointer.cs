using TMPro;
using UnityEngine;

public class ScorePointer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemiesKilledText;
    [SerializeField] private TextMeshProUGUI roundScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    private const int startPoints = 0;

    public static int HighScore { get; private set; }
    private static int EnemiesKilled { get; set; }
    public static int RoundPoints { get; private set; }
    public static bool isRunning = true;

    private Transform pointer;

    public static ScorePointer instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(instance);

        RoundPoints = startPoints;
        GetHighScore();
        pointer = transform;
        Timer.Create(pointer, AddScore, 1f, true);
        UpdateStats();
    }

    public static int GetHighScore()
    {
        if (PlayerPrefs.HasKey("highScore"))
            HighScore = PlayerPrefs.GetInt("highScore");
        else
            HighScore = 0;

        return HighScore;
    }

    private void AddScore() { if (isRunning) RoundPoints++; }

    public void AddScore(int score) 
    { 
        if (isRunning) RoundPoints += score;
        EnemiesKilled++;
    }

    public void UpdateStats()
    {
        if (RoundPoints > HighScore)
        {
            HighScore = RoundPoints;
            PlayerPrefs.SetInt("highScore", HighScore);
        }

        enemiesKilledText.text = $"ENEMIES KILLED: {EnemiesKilled}";
        roundScoreText.text = $"ROUND SCORE: {RoundPoints}";
        highScoreText.text = $"HIGH SCORE: {HighScore}";
    }

    public void ResetScore() 
    {
        UpdateStats();
        RoundPoints = 0;
        EnemiesKilled = 0;
    }
}
