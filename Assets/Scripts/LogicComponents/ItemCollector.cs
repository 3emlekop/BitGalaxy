using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private CircleCollider2D area;
    private Transform collector;

    private void Start()
    {
        collector = transform;
        area = GetComponent<CircleCollider2D>();
        area.isTrigger = true;
        SetRadius(0.1f);
    }

    public void SetRadius(float radius)
    {
        area.radius = radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            var item = collision.GetComponent<DropItem>();
            item.SetTarget(collector);
        }
    }
}
