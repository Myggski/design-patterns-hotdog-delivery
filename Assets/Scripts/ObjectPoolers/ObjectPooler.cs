using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    [SerializeField]
    private List<PoolData> pools;

    private static ObjectPooler _instance;
    private Dictionary<PoolObjectTag, Queue<IPooledObject>> _poolDictionary;

    public static ObjectPooler Instance => _instance;

    /// <summary>
    /// Setting up singleton pattern
    /// </summary>
    private void Setup() {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
            _poolDictionary = new Dictionary<PoolObjectTag, Queue<IPooledObject>>();
        }
    }

    private void PreparePool() {
        foreach (PoolData pool in pools) {
            Queue<IPooledObject> objectPool = new Queue<IPooledObject>();

            for (int i = 0; i < pool.size; i++) {
                IPooledObject poolObject = Instantiate(pool.prefab).GetComponent<IPooledObject>();
                poolObject.SoftRemove();
                objectPool.Enqueue(poolObject);
            }
            
            _poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public IPooledObject SpawnFromPool(PoolObjectTag poolObjectTag, Vector3 position, Quaternion rotation) {
        if (!_poolDictionary.ContainsKey(poolObjectTag)) {
            Debug.LogWarning($"Pool with tag {poolObjectTag} doesn't exsist.");
            return null;
        }

        IPooledObject objectToSpawn = _poolDictionary[poolObjectTag].Dequeue();
        objectToSpawn?.Initialize(position, rotation);
        
        _poolDictionary[poolObjectTag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
    
    private void Awake() {
        Setup();
    }
 
    private void Start() {
        PreparePool();
    }
    
    [Serializable]
    private struct PoolData {
        public PoolObjectTag tag;
        public GameObject prefab;
        public int size;
    }
}
