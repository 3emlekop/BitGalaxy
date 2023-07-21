using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds = new Transform[3];
    [SerializeField] private float[] layersSpeed = new float[3];

    private void Start()
    {
        foreach (Transform t in backgrounds) { t.position = Vector2.zero + Vector2.down; }
    }

    private void FixedUpdate()
    {
        for (byte i = 0; i < backgrounds.Length; i++)
            if (backgrounds[i] != null) LayerParallax(backgrounds[i], i);
    }

    private void LayerParallax(Transform background, byte line)
    {
        background.position += Vector3.down * (line + 1) * layersSpeed[line] * Time.fixedDeltaTime;
        return;
    }
}
