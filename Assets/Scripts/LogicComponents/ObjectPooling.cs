using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public byte size;

        public Pool(string tag, GameObject prefab, byte size)
        {
            this.tag = tag;
            this.prefab = prefab;
            this.size = size;
        }
    }

    #region Singleton
    public static ObjectPooling Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion

    [SerializeField] private List<Pool> poolList = new List<Pool>();
    [SerializeField] private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    private GameObject spawnObject;
    private Vector2 spawnPosition = new Vector2(0, 10);
    private Transform spawnObjTransform;

    public void AddPool(string tag, GameObject prefab, byte size)
    {
        if (Exists(tag))
            return;

        Pool pool = new Pool(tag, prefab, size);
        poolList.Add(pool);

        Queue<GameObject> poolObject = new Queue<GameObject>();

        for (byte i = 0; i < pool.size; i++)
        {
            GameObject obj = Instantiate(pool.prefab);
            obj.SetActive(false);
            poolObject.Enqueue(obj);
        }

        poolDictionary.Add(pool.tag, poolObject);

    }

    public void AddPool(string tag, GameObject prefab, byte size, Transform parent)
    {
        if (Exists(tag))
            return;

        Pool pool = new Pool(tag, prefab, size);
        poolList.Add(pool);

        Queue<GameObject> poolObject = new Queue<GameObject>();

        for (byte i = 0; i < pool.size; i++)
        {
            GameObject obj = Instantiate(pool.prefab, spawnPosition, Quaternion.identity, parent);
            obj.SetActive(false);
            poolObject.Enqueue(obj);
        }

        poolDictionary.Add(pool.tag, poolObject);

    }

    public Transform SpawnFromPool(string tag, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist");
            return null;
        }

        spawnObject = poolDictionary[tag].Dequeue();

        if (!spawnObject.activeSelf)
        {
            spawnObject.transform.position = position;
            Activate();
        }

        poolDictionary[tag].Enqueue(spawnObject);

        return spawnObject.transform;
    }

    public Transform SpawnFromPool(string tag, Vector3 position, float rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist");
            return null;
        }

        spawnObject = poolDictionary[tag].Dequeue();
        spawnObjTransform = spawnObject.transform;

        if (!spawnObject.activeSelf)
        {
            Activate();
            spawnObjTransform.Rotate(0, 0, rotation);
            spawnObjTransform.position = position;
        }

        poolDictionary[tag].Enqueue(spawnObject);

        return spawnObjTransform;
    }

    public bool Exists(string poolTag)
    {
        return poolDictionary.ContainsKey(poolTag);
    }

    public void PrintPools()
    {
        foreach (Pool pool in poolList)
            Debug.Log($"Pool | prefab: {pool.prefab.name}; size: {pool.size}; tag: {pool.tag}");
    }

    private void Activate() { spawnObject.SetActive(true); }
}