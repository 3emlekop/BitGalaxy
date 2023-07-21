using UnityEngine;

public class DeviceItem : InventoryItem
{
    [SerializeField] private DeviceData deviceData;

    private void Start()
    {
        SetDeviceData(deviceData);
    }

    public void SetDeviceData(DeviceData data)
    {
        SetItemData(data);
        deviceData = data;
    }

    new public DeviceData GetData() { return deviceData; }
}
