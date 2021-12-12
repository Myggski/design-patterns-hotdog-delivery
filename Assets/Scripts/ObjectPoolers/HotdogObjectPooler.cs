using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolers {
    public class HotdogObjectPooler : MonoBehaviour {
        [SerializeField]
        private HotdogPoolData hotdogPoolData;
        
        private Queue<IPooledHotdog> _hotdogs;

        private static HotdogObjectPooler _instance;
        public static HotdogObjectPooler Instance => _instance;
            
        /// <summary>
        /// Setting up singleton pattern and creates an instance of the poolDictionary
        /// </summary>
        private void Setup() {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            } else {
                _instance = this;
            }
        }
        
        /// <summary>
        /// Setting up the game objects inside the pool when the game starts
        /// </summary>
        private void PreparePool() {
            _hotdogs = new Queue<IPooledHotdog>();
            
            for (int i = 0; i < hotdogPoolData.size; i++) {
                IPooledHotdog poolHotdog = CreateObject();
                poolHotdog.SoftDelete();
                _hotdogs.Enqueue(poolHotdog);
            }
        }

        /// <summary>
        /// Gets a hotdog from the pool, if the pool is empty it creates a new hot dog
        /// </summary>
        /// <returns></returns>
        public IPooledHotdog Get() => _hotdogs.Count > 0 
            ? _hotdogs.Dequeue() 
            : CreateObject();

        /// <summary>
        /// Removes the hot dog and then adds it to the queue again
        /// </summary>
        /// <param name="pooledHotdog"></param>
        public void Release(IPooledHotdog pooledHotdog) {
            pooledHotdog.SoftDelete();
            _hotdogs.Enqueue(pooledHotdog);
        }

        /// <summary>
        /// Instantiates a new hot dog
        /// </summary>
        /// <returns></returns>
        private IPooledHotdog CreateObject() => Instantiate(hotdogPoolData.prefab).GetComponent<IPooledHotdog>();

        private void Awake() {
            Setup();
        }

        private void Start() {
            PreparePool();
        }

        [Serializable]
        private struct HotdogPoolData {
            public GameObject prefab;
            [Tooltip("How many game objects to prepare")]
            public int size;
        }
    }
}