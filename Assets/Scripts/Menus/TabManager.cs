using System;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    [SerializeField] private Inventory sortingInventory;
    [SerializeField] private Button[] tabButtons;

    private bool[] isCategoryVisibleArray = new bool[Enum.GetNames(typeof(ItemType)).Length];

    private void Start()
    {
        sortingInventory.OnItemsChanged.AddListener(RefreshFilter);

        for(byte i = 0; i < isCategoryVisibleArray.Length; i++)
            isCategoryVisibleArray[i] = true;
    }

    public void SwitchCategoryVisibility(int itemTypeId)    
    {
        for(byte i = 0; i < isCategoryVisibleArray.Length; i++)
        {
            isCategoryVisibleArray[i] = false;
            tabButtons[i].interactable = true;
        }

        isCategoryVisibleArray[itemTypeId] = true;
        tabButtons[itemTypeId].interactable = false;
        tabButtons[3].interactable = true;
        RefreshFilter();
    }

    public void ResetVisibility()
    {
        for(byte i = 0; i < isCategoryVisibleArray.Length; i++)
        {
            tabButtons[i].interactable = true;
            isCategoryVisibleArray[i] = true;
        }
        tabButtons[3].interactable = false;
        RefreshFilter();
    }

    private void RefreshFilter()
    {
        foreach(var item in sortingInventory.items)
            item.gameObject.SetActive(isCategoryVisibleArray[(int)item.Data.ItemType]);
    }
}