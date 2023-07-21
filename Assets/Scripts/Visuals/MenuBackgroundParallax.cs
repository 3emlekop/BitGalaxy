using UnityEngine;

public class MenuBackgroundParallax : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds;
    [SerializeField] private Transform mainCamera;

    private Vector3 offset;

    private void Start()
    {
        offset = backgrounds[0].position;
    }

    private void Update()
    {
        Parallax();
    }

    private void Parallax()
    {
        backgrounds[0].position = offset + (mainCamera.position / (1.05f));
        backgrounds[1].position = offset + (mainCamera.position / (1.15f));
    }
}
