using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    public float moveTime { get; private set; }
    private Vector2 startTouchPos = new Vector2(Screen.width / 2, Screen.height / 4);
    private Vector2 touchPos = Vector2.zero;
    private Vector2 pos = Vector2.zero;
    private Camera mainCamera;
    private Transform playerTransform;

    public void SetMoveTime(float time)
    {
        if (time <= 0 || time >= 1)
            throw new System.Exception("Argument \'time\' should be bigger than 0 or less than 1.");

        moveTime = time;
    }
    private void Start()
    {
        mainCamera = Camera.main;
        SetMoveTime(0.1f);
        playerTransform = transform;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if(Input.GetMouseButton(0))
        {
            pos = Input.mousePosition;
            touchPos = pos;
        }
#endif

        if (Input.touchCount > 0)
        {
            pos = Input.GetTouch(0).position;
            if (pos.y < Screen.height - Screen.height / 15)
                touchPos = pos;
        }

        playerTransform.position = Vector2.Lerp(playerTransform.position, touchPos, moveTime);
    }
}
