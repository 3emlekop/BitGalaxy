using UnityEngine;
using UnityEngine.UI;
public class InventoryItem : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] protected Image itemIcon;
    [SerializeField] protected Image itemOutline;

    [SerializeField] public bool isBought;

    private ItemData data;
    public byte itemIndex;

    public void SetItemData(ItemData data)
    {
        if (data == null)
        {
            this.data = null;
            itemIcon.sprite = null;
            itemOutline.sprite = null;
            itemIcon.color = new Color(0, 0, 0, 0);
        }
        else
        {
            ItemData newItem = ScriptableObject.Instantiate(data);
            this.data = newItem;
            itemIcon.sprite = newItem.sprite;
            itemOutline.sprite = newItem.sprite;
            itemIcon.color = Color.white;
        }
    }

    public ItemData GetData() { return data; }
}
