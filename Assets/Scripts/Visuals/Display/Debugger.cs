using TMPro;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsTextMesh;
    [SerializeField] private TextMeshProUGUI scoreTextMesh;

    private int lastFrameIndex;
    private float[] frameDeltaTimeArray = new float[50];
    public bool showFps;

    public static Debugger instance;

    private void Start()
    {
        if (instance == null)
            instance = this;

        fpsTextMesh.text = string.Empty;
        //showFps = Options.instance.showFps;
    }

    private void Update()
    {
        frameDeltaTimeArray[lastFrameIndex] = Time.deltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;

        scoreTextMesh.text = "SCORE: " + ScorePointer.RoundPoints;
        if(showFps)
            fpsTextMesh.text = "FPS: " + Mathf.RoundToInt(CalculateFps()).ToString();
    }

    private float CalculateFps()
    {
        float total = 0f;
        foreach (float deltaTime in frameDeltaTimeArray)
            total += deltaTime * Time.timeScale;
        return frameDeltaTimeArray.Length / total;
    }
}
