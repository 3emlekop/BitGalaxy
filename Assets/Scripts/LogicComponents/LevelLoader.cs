using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Slider slider;
    [SerializeField] private Animator animator;
    Vector3 pos = new Vector3();

    public void LoadLevel(byte index)
    {
        pos.y = mainCamera.transform.position.y;
        loadingScreen.transform.position = pos;
        animator.SetTrigger("Start");
        StartCoroutine(LoadAsync(index));
    }

    private IEnumerator LoadAsync(int index)
    {
        yield return new WaitForSeconds(0.5f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
