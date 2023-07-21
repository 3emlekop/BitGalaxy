using UnityEngine;
using UnityEngine.UI;

public sealed class TurretItem : InventoryItem
{
    [SerializeField] private Image moduleIcon;
    [SerializeField] private TurretData turretData;

    private void Start()
    {
        SetTurretData(turretData);
    }

    public void SetTurretData(TurretData data)
    {
        SetItemData(data);

        if (data == null)
            turretData = null;
        else
        {
            turretData = ScriptableObject.Instantiate(data);
            turretData.name = data.name;

            if (data.module != null)
                SetModule(data.module);
        }

    }

    public void SetModule(ModuleData module)
    {
        turretData.module = module;
        moduleIcon.sprite = module.sprite;
        moduleIcon.color = Color.white;
        turretData.outlineColor = module.outlineColor;
    }

    public ModuleData GetModule() { return turretData.module; }

    new public TurretData GetData() { return turretData; }
}
