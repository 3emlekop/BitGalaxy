using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Toggle fpsCounterToggle;
    [SerializeField] private Toggle screenShakeToggle;

    private readonly string fpsCounterSaveKey = "FpsCounter";
    private readonly string screenShakeSaveKey = "ScreenShake";

    private void Start()
    {
        if (PlayerPrefs.HasKey(fpsCounterSaveKey) == false || PlayerPrefs.HasKey(screenShakeSaveKey) == false)
        {
            fpsCounterToggle.isOn = false;
            screenShakeToggle.isOn = true;
            SaveSettings();
            return;
        }

        fpsCounterToggle.isOn = PlayerPrefs.GetInt(fpsCounterSaveKey) == 1;
        screenShakeToggle.isOn = PlayerPrefs.GetInt(screenShakeSaveKey) == 1;
    }

    public void SwitchFpsCounter()
    {
        fpsCounterToggle.isOn = !fpsCounterToggle;
    }

    public void SwitchShakeScreen()
    {
        screenShakeToggle.isOn = !screenShakeToggle;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt(fpsCounterSaveKey, fpsCounterToggle.isOn == true ? 1 : 0);
        PlayerPrefs.SetInt(screenShakeSaveKey, screenShakeToggle.isOn == true ? 1 : 0);
    }

    private void OnDisable()
    {
        SaveSettings();
    }
}