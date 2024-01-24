using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;

    private Transform cameraTransform;
    private Vector3 startPos;
    private bool isEnabled;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("screenShake"))
            isEnabled = PlayerPrefs.GetInt("screenShake") == 1 ? true : false;
        else
            isEnabled = true;

        cameraTransform = transform;
        startPos = cameraTransform.position;
    }

    public void PlayExplosionSound(AudioClip clip, float volume)
    {
        if (audioSource == null)
            return;

        if (audioSource.enabled == false)
            return;

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void Shake(float duration, float magnitude)
    {
        if(isEnabled)
            StartCoroutine(StartShake(duration, magnitude));
    }

    private IEnumerator StartShake(float duration, float magnitude)
    {
        float elapsed = 0f;
        while (elapsed < magnitude)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            cameraTransform.position = new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }
        cameraTransform.position = startPos;
    }
}
