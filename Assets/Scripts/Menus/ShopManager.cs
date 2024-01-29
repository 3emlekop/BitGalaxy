using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Item itemPrefab;

    private ItemData[] shopItems;


    private void Awake()
    {
        shopItems = ResourceManager.instance.GetAllItems();

        foreach(var item in shopItems)
        {
            AddItem(item);
        }
    }

    private void AddItem(ItemData data)
    {
        var item = Instantiate(itemPrefab, transform);
        item.SetData(data);
    }
}
