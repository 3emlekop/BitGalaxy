using UnityEngine;

public class LayerConveyor : MonoBehaviour
{
    private Transform layerTransform;
    private Vector3 startPos;
    private float offset;

    private void Start()
    {
        layerTransform = transform;
        offset = 0.8f * layerTransform.parent.parent.localScale.y;
        startPos = new Vector3(0, 5 * offset);
    }

    private void FixedUpdate()
    {
        if (layerTransform.position.y <= -3 * offset)
        {
            layerTransform.position = startPos;
        }
    }
}
