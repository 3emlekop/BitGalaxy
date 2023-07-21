using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    private Transform flash;
    private Color fading = Color.white;
    private SpriteRenderer sprite;
    public bool isStatic = false;

    private void Awake()
    {
        flash = transform;
        sprite = flash.GetComponent<SpriteRenderer>();
    }

    public void Flash()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        StartCoroutine(FlashCoroutine(Color.white));
    }

    public void Flash(Color color)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        StartCoroutine(FlashCoroutine(color));
    }

    public void SetColor(Color color, bool isStatic)
    {
        this.isStatic = isStatic;
        sprite.color = color;
    }

    private void OnEnable()
    {
        if(!gameObject.activeSelf)
            gameObject.SetActive(true);

        if(!isStatic)
            StartCoroutine(FlashCoroutine(Color.white));
    }

    private IEnumerator FlashCoroutine(Color color)
    {
        fading = color;
        for (byte i = 0; i < 20; i++)
        {
            fading.a -= Time.fixedDeltaTime * 4;
            sprite.color = fading;

            if (fading.a <= 0)
                gameObject.SetActive(false);
            yield return null;
        }
    }
}
