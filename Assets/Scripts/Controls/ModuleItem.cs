using UnityEngine;

public class ModuleItem : InventoryItem
{
    [SerializeField] private ModuleData moduleData;

    private void Start()
    {
        SetModuleData(moduleData);
    }

    public void SetModuleData(ModuleData data)
    {
        SetItemData(data);
        moduleData = data;
    }

    new public ModuleData GetData() { return moduleData; }
}
