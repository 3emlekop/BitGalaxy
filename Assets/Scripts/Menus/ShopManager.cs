using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Inventory shopInventory;

    private void Awake()
    {
        foreach(var item in ResourceManager.instance.GetAllItems())
            shopInventory.AddItem(item);

        //TODO: Implement unlocked items loading
    }
}
