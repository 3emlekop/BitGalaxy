using TMPro;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsTextMesh;
    [SerializeField] private TextMeshProUGUI scoreTextMesh;

    private int lastFrameIndex;
    private float[] frameDeltaTimeArray = new float[50];

    public static Transform instance;

    private void Start()
    {
        if (instance == null)
            instance = transform;
    }

    private void Update()
    {
        frameDeltaTimeArray[lastFrameIndex] = Time.deltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;

        fpsTextMesh.text = "FPS: " + Mathf.RoundToInt(CalculateFps()).ToString();
        scoreTextMesh.text = "SCORE: " + ScorePointer.RoundPoints;
    }

    private float CalculateFps()
    {
        float total = 0f;
        foreach (float deltaTime in frameDeltaTimeArray)
            total += deltaTime * Time.timeScale;
        return frameDeltaTimeArray.Length / total;
    }
}
