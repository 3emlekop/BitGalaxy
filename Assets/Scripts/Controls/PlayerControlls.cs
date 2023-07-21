using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 startTouchPos = new Vector2(Screen.width / 2, Screen.height / 4);
    private Vector2 touchPos;
    private Vector2 pos = new Vector2();
    public float MoveTime { get; private set; }

    public void SetMoveTime(float time)
    {
        if (time <= 0 && time > 1)
            return;

        MoveTime = time;
    }
    private void Start()
    {
        mainCamera = Camera.main;
        SetMoveTime(0.1f);
    }

    private void OnEnable()
    {
        touchPos = startTouchPos;
        ScorePointer.isRunning = true;

        if(ScorePointer.instance != null)
            ScorePointer.instance.ResetScore();
    }

    private void OnDisable()
    {
        if (GameMenu.instance == null)
            return;

        GameMenu.instance.Pause(false);
        ScorePointer.isRunning = false;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            pos = Input.GetTouch(0).position;

            if (pos.y < Screen.height - Screen.height / 15)
                touchPos = pos;
        }

        MoveToPoint(transform, mainCamera.ScreenToWorldPoint(touchPos));
    }

    private void MoveToPoint(Transform obj, Vector2 point)
    {
        if (Time.timeScale == 0)
            return;

        obj.position = Vector2.Lerp(obj.position, point, MoveTime);
    }
}
