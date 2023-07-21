using UnityEngine;

public class BulletBarrier : MonoBehaviour
{
    private void Start()
    {
        transform.SetParent(null);
        transform.position = new Vector3(Random.Range(-2f, 2f), 0);
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
            collision.gameObject.SetActive(false);
    }
}
