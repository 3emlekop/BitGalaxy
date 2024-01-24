using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds = new Transform[2];
    [SerializeField] private float[] coefficients = new float[2] { 1.05f, 1.15f };
    [SerializeField] private float speed;

    private Transform cameraTransform;
    private Vector2 point = Vector2.zero;
    private Vector3 backgroundOffset;

    private void Awake()
    {
        cameraTransform = transform;
        backgroundOffset = backgrounds[0].position;
    }

    public void SetX(float x)
    {
        point.x = x;
    }

    public void SetY(float y)
    {
        point.y = y;
    }

    public void MoveToPoint()
    {
        StopAllCoroutines();
        StartCoroutine(Move(point));
    }

    public void MoveToPoint(Vector2 point)
    {
        StopAllCoroutines();
        StartCoroutine(Move(point));
    }

    private IEnumerator Move(Vector2 point)
    {
        while (Vector2.Distance(cameraTransform.position, point) > 0.01f)
        {
            cameraTransform.position = Vector2.Lerp(cameraTransform.position, point, Time.deltaTime * speed);

            backgrounds[0].position = backgroundOffset + cameraTransform.position / 1.05f;
            backgrounds[1].position = backgroundOffset + cameraTransform.position / 1.15f;
            yield return null;
        }
        cameraTransform.position = point;
    }
}
