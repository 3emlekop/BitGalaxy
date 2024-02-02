using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public delegate void OnClick(int itemId);
    public event OnClick OnClickEvent;

    [SerializeField] protected Image mainSpriteRenderer;
    [SerializeField] protected Image outlineSpriteRenderer;
    [SerializeField] protected Image glowingSpriteRenderer;
    [SerializeField] protected ItemData itemData;

    public bool IsActive { get; set; }
    public int InventoryId { get; private set; }

    protected Transform itemTransform;

    protected void Awake()
    {
        itemTransform = transform;
    }

    public ItemData Data => itemData;

    public void SetData(ItemData data, int inventoryId)
    {
        if (data == null)
        {
            mainSpriteRenderer.sprite = null;
            outlineSpriteRenderer.sprite = null;
            glowingSpriteRenderer.color = Color.white;
        }
        else
        {
            mainSpriteRenderer.sprite = data.Sprite;
            outlineSpriteRenderer.sprite = data.Sprite;
            glowingSpriteRenderer.color = data.RarityColor;
        }
        itemData = data;
        InventoryId = inventoryId;
    }

    public void SetData(ItemData data)
    {
        if (data == null)
        {
            mainSpriteRenderer.sprite = null;
            outlineSpriteRenderer.sprite = null;
            glowingSpriteRenderer.color = Color.white;
        }
        else
        {
            mainSpriteRenderer.sprite = data.Sprite;
            outlineSpriteRenderer.sprite = data.Sprite;
            glowingSpriteRenderer.color = data.RarityColor;
        }
        itemData = data;
    }

    public void Click()
    {
        OnClickEvent.Invoke(InventoryId);
    }
}