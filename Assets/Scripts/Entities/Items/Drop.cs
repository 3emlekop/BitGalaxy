using UnityEngine;

public class Drop : Item
{
    [SerializeField] private AudioClip pickupSound;

    private Transform target;
    private Vector2 moveVector;
    private CameraShake cameraShake;

    new private void Awake()
    {
        base.Awake();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        itemTransform = transform;
        moveVector = itemTransform.position;
    }

    private void Update()
    {
        if (target != null)
            MoveToTarget(target);
        else
            MoveDown();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    new public GameObject SetData(ItemData data)
    {
        if (data == null)
            Debug.LogError("Item data of drop item is null.");
        else
        {
            mainSpriteRenderer.sprite = data.sprite;
            outlineSpriteRenderer.color = data.RarityColor;
        }
        itemData = data;
        return gameObject;
    }

    private void MoveDown()
    {
        if (itemTransform.position.y < -6)
        {
            Destroy(gameObject);
            return;
        }
        moveVector.y = itemTransform.position.y - Time.deltaTime;
        itemTransform.position = moveVector;
    }

    private void MoveToTarget(Transform target)
    {
        if (Vector2.Distance(itemTransform.position, target.position) > 0.01f)
            itemTransform.position = Vector3.Lerp(itemTransform.position, target.position, 0.2f);
        else
        {
            //Inventory.AddItem(itemData);
            cameraShake.PlayExplosionSound(pickupSound, 0.5f);
            Destroy(itemTransform.gameObject);
        }
    }
}
