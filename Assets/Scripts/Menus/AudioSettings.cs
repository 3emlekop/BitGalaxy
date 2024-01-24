using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private readonly string musicSliderSaveKey = "MusicVolume"; 
    private readonly string sfxSliderSaveKey = "SfxVolume"; 

    private float MusicVolume
    {
        set
        {
            musicSlider.value = value;
            SetMusicVolume();
        }
    }

    private float SfxVolume
    {
        set
        {
            sfxSlider.value = value;
            SetSfxVolume();
        }
    }

    private void Start()
    {
        if(PlayerPrefs.HasKey(musicSliderSaveKey) == false || PlayerPrefs.HasKey(sfxSliderSaveKey) == false)
        {
            musicSlider.value = 0.5f;
            sfxSlider.value = 0.5f;
            SaveSettings();
            return;
        }

        MusicVolume = PlayerPrefs.GetFloat(musicSliderSaveKey);
        SfxVolume = PlayerPrefs.GetFloat(sfxSliderSaveKey);
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("Music", Mathf.Log10(musicSlider.value) * 20);
    }

    public void SetSfxVolume()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxSlider.value) * 20);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(musicSliderSaveKey, musicSlider.value);
        PlayerPrefs.SetFloat(sfxSliderSaveKey, sfxSlider.value);
    }

    private void OnDisable()
    {
        SaveSettings();
    }
}
