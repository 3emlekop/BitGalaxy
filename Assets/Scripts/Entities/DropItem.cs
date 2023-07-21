using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private SpriteRenderer glow;

    private Transform itemTransform;
    private Vector3 moveVector;
    public ItemData itemData;

    public bool targeted { get; private set; }
    private Transform target;

    private void Start()
    {
        itemTransform = transform;
        moveVector = itemTransform.position;
        targeted = false;
    }

    private void Update()
    {
        if (targeted)
            MoveToTarget(target);
        else
            MoveDown();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        targeted = true;
    }

    public void SetData(ItemData data)
    {
        itemData = data;
        icon.sprite = data.sprite;
        glow.color = data.outlineColor;
    }

    public ItemData GetData()
    {
        return itemData;
    }

    private void MoveDown()
    {
        moveVector.y = itemTransform.position.y - Time.deltaTime;
        itemTransform.position = moveVector;
    }

    private void MoveToTarget(Transform target)
    {
        if (Mathf.Abs(target.position.x - itemTransform.position.x) > 0.1f &&
            Mathf.Abs(target.position.y - itemTransform.position.y) > 0.1f)
            itemTransform.position = Vector3.Lerp(itemTransform.position, target.position, 0.05f);
        else
        {
            SaveSystem.AddItem(itemData);
            Destroy(itemTransform.gameObject);
        }
    }
}
