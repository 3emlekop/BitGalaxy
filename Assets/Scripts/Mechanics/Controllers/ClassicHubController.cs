using UnityEngine;

public class ClassicHubController : GameModeController
{
    [SerializeField] private Ship playerShip;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private Transform shopItemsParent;
    [SerializeField] private Economy playerEconomy;

    new private void Awake()
    {
        base.Awake();
        
    }
}
