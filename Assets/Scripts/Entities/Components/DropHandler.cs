using UnityEngine;

public class DropHandler : MonoBehaviour
{
    [SerializeField] private Drop dropItemPref;
    [SerializeField] private ItemData[] drop;
    [SerializeField] private float[] chances;
    [SerializeField] private bool ignoreKilledCondition = false;

    private HealthSystem healthSystem;
    private bool[] hasDrops;
    private Transform owner;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDeathEvent += SpawnDrop;
        hasDrops = new bool[drop.Length];

        owner = transform;
        RevealDrops();
    }

    private void OnEnable()
    {
        RevealDrops();
    }

    private void OnDisable()
    {
        healthSystem.OnDeathEvent -= SpawnDrop;
    }

    private void RevealDrops()
    {
        for (byte i = 0; i < hasDrops.Length; i++)
        {
            int chance = Mathf.RoundToInt(chances[i]);

            if (chance == 100)
            {
                hasDrops[i] = true;
                return;
            }

            if (chance == 0)
            {
                hasDrops[i] = false;
                return;
            }

            hasDrops[i] = Random.Range(0, 100) < chance ? true : false; 
        }
    }

    public ItemData GetDropItem()
    {
        if (!HasDrop())
            return null;
        else
            for (byte i = 0; i < drop.Length; i++)
                if (hasDrops[i])
                    return drop[i];

        return null;
    }

    public bool HasDrop()
    {
        foreach (var hasDrop in hasDrops)
            if (hasDrop)
                return true;

        return false;
    }

    private void SpawnDrop(bool withLoot)
    {
        if (!withLoot && !ignoreKilledCondition)
            return;

        for (byte i = 0; i < chances.Length; i++)
        {
            if (!hasDrops[i])
                continue;

            Vector3 randPos = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.2f, 0.2f), 0);
            Instantiate(dropItemPref.SetData(drop[i]), owner.position + randPos, Quaternion.identity);
        }
    }
}
