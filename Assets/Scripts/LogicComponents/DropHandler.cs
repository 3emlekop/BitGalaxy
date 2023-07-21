using UnityEngine;

public class DropHandler : MonoBehaviour
{
    [SerializeField] private DropItem itemPref;
    [SerializeField] private ItemData[] drop;
    [SerializeField] private float[] chances;

    private Transform owner;
    private HealthSystem healthSystem;
    private DropItem[] dropItems;

    private void Awake()
    {
        owner = transform;
        drop = owner.GetComponent<Enemy>().availableTurrets;
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.onDeath += SpawnDrop;
    }

    private void OnEnable()
    {
        SetDrop();
    }

    private void SetDrop()
    {
        if (drop.Length == 0)
            return;

        dropItems = new DropItem[drop.Length];

        for (byte i = 0; i < dropItems.Length; i++)
        {
            if (chances[i] == 0)
                continue;

            if (Random.Range(0, 100) < chances[i])
            {
                itemPref.SetData(drop[i]);
                dropItems[i] = itemPref;
                Debug.Log(dropItems[i].GetData().name);
            }
        }
    }

    public bool HasDrop()
    {
        if (dropItems == null)
            return false;

        return dropItems.Length > 0;
    }

    private void SpawnDrop()
    {
        foreach (var item in dropItems)
            if (item != null)
                Instantiate(item, owner.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f)), Quaternion.identity);

        dropItems = null;
    }
}
