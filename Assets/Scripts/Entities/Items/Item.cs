using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] protected Image mainSpriteRenderer;
    [SerializeField] protected Image outlineSpriteRenderer;
    [SerializeField] protected ItemData itemData;

    public bool IsActive { get; set; }

    protected Transform itemTransform;

    protected void Awake()
    {
        itemTransform = transform;
    }

    public void SetData(ItemData data)
    {
        if(data == null)
        {
            mainSpriteRenderer.sprite = null;
            outlineSpriteRenderer.sprite = null;
            outlineSpriteRenderer.color = Color.white;
        }
        else
        {
            mainSpriteRenderer.sprite = data.sprite;
            outlineSpriteRenderer.sprite = data.sprite;
            outlineSpriteRenderer.color = data.RarityColor;
        }
        itemData = data;
    }
}