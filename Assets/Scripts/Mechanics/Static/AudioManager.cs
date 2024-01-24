using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] float startTimer = 0f;
    [SerializeField] bool isLoopStart = false;
    [SerializeField] float fadeSpeed = 1f;

    private byte index = 255;
    private float startVolume;
    private float maxVolume = 1f;
    public bool lockMusic = false;

    private void Start()
    {
        startVolume = audioSource.volume;
        if (startTimer == 0f)
            PickMusic();
        else if (!isLoopStart)
            Timer.Create(transform, PickMusic, startTimer, false);
        else
        {
            PickMusic();
            Timer.Create(transform, PickMusic, startTimer, true);
        }
    }

    public void NextMusic()
    {
        audioSource.Stop();

        if (index + 1 < audioClips.Length)
            index++;
        else
            index = 0;

        var nextAudio = audioClips[index];
        audioSource.clip = nextAudio;
        StartCoroutine(AudioFadeIn(audioSource));
    }

    public void PickMusic()
    {
        if (lockMusic)
            return;

        if(audioClips.Length == 1)
        {
            audioSource.clip = audioClips[0];
            StartCoroutine(AudioFadeIn(audioSource));
            return;
        }

        if (index == 255)
            index = (byte)Random.Range(0, audioClips.Length - 2);
        else
        {
            if (audioSource.isPlaying)
                return;

            if (index + 1 < audioClips.Length)
                index++;
            else
                index = 0;
        }

        audioSource.clip = audioClips[index];
        StartCoroutine(AudioFadeIn(audioSource));
    }

    public void PickLastMusic()
    {
        StartCoroutine(CurrentAudioFadeOut(false));
        Timer.Create(transform, Fade, 2, false);
    }

    private void Fade()
    {
        maxVolume = 1f;
        audioSource.clip = audioClips[audioClips.Length - 1];
        StartCoroutine(AudioFadeIn(audioSource));
    }

    private IEnumerator AudioFadeIn(AudioSource audio)
    {
        float step = fadeSpeed / 10;
        audio.volume = 0;
        audio.Play();
        while (audio.volume < maxVolume)
        {
            audio.volume += step;
            yield return null;
        }
    }

    public IEnumerator UnpauseCurrentAudio()
    {
        float step = 0.02f;
        audioSource.volume = 0;
        audioSource.UnPause();
        while (audioSource.volume < 1)
        {
            audioSource.volume += step;
            yield return null;
        }
    }

    public IEnumerator CurrentAudioFadeOut(bool pause)
    {
        if (audioSource.isPlaying)
        {
            float step = 0.05f;
            while (audioSource.volume > 0)
            {
                audioSource.volume -= step;
                yield return null;
            }
            if (pause)
                audioSource.Pause();
            else
                audioSource.Stop();
        }
    }

    public IEnumerator DownVolume(float percent)
    {
        float targetValue = Mathf.Clamp(audioSource.volume * percent, 0.05f, 1f);
        maxVolume = targetValue;
        while(audioSource.volume > targetValue)
        {
            audioSource.volume -= 0.03f;
            yield return null;
        }
    }

    public IEnumerator ResetVolume()
    {
        while (audioSource.volume < startVolume)
        {
            audioSource.volume += 0.01f;
            yield return null;
        }
        audioSource.volume = startVolume;
    }

    public void ResetVolume(bool instant)
    {
        if(instant)
            audioSource.volume = startVolume;
        else
            StartCoroutine(ResetVolume());
    }
}
