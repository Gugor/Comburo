using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{
    public class SpawnedPoolManager : MonoBehaviour
    {
        [SerializeField] private List<ISpawneable> spawnedPool = new List<ISpawneable>();

        public List<ISpawneable> Spawneables { get; } 
        public int Count { get => spawnedPool.Count; }

        public void Add(ISpawneable spawneable)
        {
            if (spawnedPool != null)
            {
                Debug.Log(spawneable.iTransform.name + " added to pool " + this.name);
                spawnedPool.Add(spawneable);
            }
        }
         
        public void Remove(ISpawneable spawneable)
        {
            if (spawnedPool.Contains(spawneable))
            {
                Debug.Log(spawneable.iTransform.name + " removed from pool " + this.name);
                spawnedPool.Remove(spawneable);
            }
        }

        public bool Contains(ISpawneable spawneable)
        {
            return spawnedPool.Contains(spawneable);
        }
        
    }
}
