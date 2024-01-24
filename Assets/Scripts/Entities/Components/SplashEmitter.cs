using UnityEngine;

public class SplashEmitter : MonoBehaviour
{
    public SplashArea splashArea;
    private Transform splash;
    private Transform parent;

    private float radius;
    private byte damage;
    private string ignoreTag;
    private Vector3 size;

    public void SetSplashArea(SplashArea splashArea, Transform parent)
    {
        this.splashArea = splashArea;
        splash = splashArea.GetComponent<Transform>();
        size = new Vector3(radius, radius);
        splash.localScale = size;

        this.parent = parent;
        splashArea.Damage = damage;
        splashArea.IgnoreTag = ignoreTag;
    }

    public void SetStats(float radius, byte damage, string ignoreTag)
    {
        this.radius = radius;
        this.damage = damage;
        this.ignoreTag = ignoreTag;
    }

    public void CreateSplash()
    {
        if (splash == null)
            return;

        splash.position = parent.position;
        splashArea.gameObject.SetActive(true);
    }
}
