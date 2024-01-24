[System.Serializable]
public class InventoryData
{
    public int Size { get; private set; }
    public ItemData[] StoredItems { get; private set; }

    public InventoryData()
    {
        Size = 30;
        StoredItems = new ItemData[Size];
    }
}