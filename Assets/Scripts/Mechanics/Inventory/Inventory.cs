using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnItemsChanged;

    [SerializeField] private Item itemPrefab;
    [SerializeField] private ItemMenu itemMenu;
    [SerializeField] private Transform storageTransform;
    [SerializeField] private bool isPlayerInventory = false;

    public static Inventory playerInventoryInstance;

    public List<Item> items = new List<Item>();
    public int ItemsCount => items.Count;

    private int itemIdCounter = 0;

    private void Awake()
    {
        if (isPlayerInventory == false)
            return;

        if (playerInventoryInstance == null)
            playerInventoryInstance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void AddItem(ItemData data)
    {
        if(data == null)
            return;
        
        if(data.Name == string.Empty)
            return;

        var newItemObject = Instantiate(itemPrefab, storageTransform);
        newItemObject.SetData(data, itemIdCounter);
        newItemObject.OnClickEvent += OpenItemMenu;

        items.Add(newItemObject);
        OnItemsChanged.Invoke();
        itemIdCounter++;
    }

    private void OpenItemMenu(int itemId)
    {
        foreach (var item in items)
        {
            if (item.InventoryId == itemId)
            {
                itemMenu.ApplyInfo(item.Data, item.InventoryId);
                break;
            }
        }
        itemMenu.gameObject.SetActive(true);
    }

    public void RemoveItem(int id)
    {
        for(int i = 0; i < ItemsCount; i++)
        {
            if(items[i].InventoryId == id)
            {
                items.RemoveAt(i);
                Destroy(storageTransform.GetChild(i).gameObject);
                break;
            }
        }

        OnItemsChanged.Invoke();
    }

    public void ApplyData(InventoryData data)
    {
        foreach (var item in data.StoredItems)
            AddItem(item);
    }

    private void OnDisable()
    {
        foreach (var item in items)
            item.OnClickEvent -= OpenItemMenu;
    }
}