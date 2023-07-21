using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Transform cameraTransform;

    private Vector3 targetPos;

    private void Awake()
    {
        cameraTransform = mainCamera.transform;
    }

    public void Levels()
    {
        targetPos.x = 0;
        targetPos.y = 10;
    }

    public void OpenInventory()
    {
        targetPos.x = 10;
        targetPos.y = 0;
    }

    public void Options()
    {
        targetPos.x = -10;
        targetPos.y = 0;
    }

    public void Shop()
    {
        targetPos.x = 10;
        targetPos.y = 10;
    }

    public void Back()
    {
        targetPos = Vector2.zero;
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void MoveCamera()
    {
        cameraTransform.position = Vector2.Lerp(cameraTransform.position, targetPos, 0.3f);
    }
}
