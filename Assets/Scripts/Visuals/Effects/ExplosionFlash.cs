using System.Collections;
using UnityEngine;

public class ExplosionFlash : MonoBehaviour
{
    private Transform flashTransform;
    private SpriteRenderer spriteRenderer;
    private Color color;
    public float scale = 3f;

    private void Awake()
    {
        flashTransform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (flashTransform == null)
            flashTransform = transform;
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        float speed = 0.6f / scale;
        flashTransform.localScale = Vector3.one * scale;
        color = Color.white;
        spriteRenderer.color = color;
        for (byte i = 0; i < 5 * scale; i++)
        {
            flashTransform.localScale = Vector3.Lerp(flashTransform.localScale, Vector3.zero, speed);
            color.a = Mathf.Lerp(color.a, 0, speed);
            spriteRenderer.color = color;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
