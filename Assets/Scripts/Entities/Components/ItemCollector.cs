using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemCollector : MonoBehaviour
{
    private CircleCollider2D collectArea;
    private Transform collectorTransform;
    private Drop item;

    private void Start()
    {
        collectorTransform = transform;
        collectArea = GetComponent<CircleCollider2D>();
        collectArea.isTrigger = true;
        SetRadius(0.2f);
    }

    public void SetRadius(float radius)
    {
        collectArea.radius = radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
            if (collision.TryGetComponent<Drop>(out item))
                item.SetTarget(collectorTransform);
    }
}
