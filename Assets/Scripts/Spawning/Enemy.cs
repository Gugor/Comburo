using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{
    public abstract class Enemy : MonoBehaviour, ISpawneable
    {
        #region Public Variables
        public Transform target;
        public Transform parent;
        public Vector3 position;
        public Spawner originSpawner;
        public WaveSpawnerManager waveSpawningManager;
        public SpawnedPoolManager spawnedPool;
        public float desapearingDistanceFromSapwn;
        public float baseSpeed;
        #endregion

        #region Private Variables
        private List<ISpawneable> spawnedPoolBelogsTo;
        private float xDistanceFromSpawnPoint;
        private float distanceFromPlayer;
        #endregion

        #region Properties
        public Transform iTransform { get => transform; } //ISpawneable
        public GameObject iGameObject { get => gameObject; } //ISpawneable
        public int SpawningWeight { get; set; } //ISpawneable
        #endregion

        #region Initializers

        //ISpawneable
        public virtual void Initialize(string name, Transform parent, float speed, Vector3 spawningPosition)
        {
            this.name = name;
            this.parent = parent;
            this.waveSpawningManager = parent.GetComponent<WaveSpawnerManager>();
            this.baseSpeed = speed;

            transform.SetParent(parent);
            transform.position = parent.position;

            waveSpawningManager.enemiesSpawned.Add(this);

        }

        //ISpawneable
        public virtual void Initialize(string name, Transform target, Transform parent, Vector3 spawningPosition, SpawnedPoolManager spawnedPoolManager)
        {
            this.name = name;
            this.target = target;
            this.parent = parent;
            this.position = spawningPosition;
            this.spawnedPool = spawnedPoolManager;

            transform.position = parent.position;
            spawnedPool.Add(this);
        }
        #endregion

        #region BuiltIn Methods
            
        #endregion

        #region Main Methods
        /// <summary>
        /// <para>Retrives the distance from the point where the object spawned.</para>
        /// </summary>
        /// <returns></returns>
        public virtual float getDistanceFromSpawn()
        {
           return Vector3.Distance(transform.position, originSpawner.transform.position);
        }

        /// <summary>
        /// <para>Makes the object disapear when it arrives to a certain distance from the spawning point.</para>
        /// </summary>
        public virtual void Desapear()
        {
            float distance = getDistanceFromSpawn();
            float size = transform.lossyScale.x;
            float disepearingDistance = distance + size;

            if (disepearingDistance >= desapearingDistanceFromSapwn)
            {
                if(spawnedPool.Contains(this))
                {
                    spawnedPool.Remove(this);
                }
                Destroy(gameObject);
            }
        }
        #endregion

        #region Abstract Methods
        public abstract void Kill( Collider other);
        public abstract void Move();

        #endregion
    }
}