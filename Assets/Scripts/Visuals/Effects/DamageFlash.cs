using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public bool isStatic = false;

    private Transform flash;
    private Color fading = Color.white;
    private SpriteRenderer sprite;
    private float delta;
    private GameObject owner;

    private void Awake()
    {
        delta = Time.fixedDeltaTime * 4;
        flash = transform;
        sprite = flash.GetComponent<SpriteRenderer>();
        owner = gameObject;
    }

    public void Flash(Color color)
    {
        fading = color;
    }

    public void SetColor(Color color, bool isStatic)
    {
        this.isStatic = isStatic;
        sprite.color = color;
        fading = color;
    }

    private void Update()
    {
        if (isStatic)
            return;

        if(fading.a < 0.01f)
        {
            owner.SetActive(false);
            return;
        }

        fading.a -= delta;
        sprite.color = fading;
    }
}
