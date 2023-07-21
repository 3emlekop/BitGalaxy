using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    public float radius = 5f;
    private ItemCollector collector;
    private Transform ship;

    void Start()
    {
        ship = transform.parent;
        transform.position = ship.position;
        collector = ship.GetComponent<ItemCollector>();
        collector.SetRadius(radius);
    }
}
