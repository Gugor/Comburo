using System.Collections.Generic;
using Comburo.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Comburo
{
    public abstract class AbstractSpawnerManager<T> : MonoBehaviour
    {
        #region Public Variables
        public Brain brain;
        public SpawningModeManager spawningModeManager;
        public SpawnedPoolManager spawnedPool;
        public List<T> spawnersPool;
        [Space(10)]
        [Header("Data")]
        [Tooltip("Change Values in the Data object.")]
        public SpawnerManagerData defaultData;
        public SpawningModesPoolSO modesPoolData;
        public Transform elementalsParent;


        
        [HideInInspector] public AbstractSpawningMachine spawnerMachine;
        [HideInInspector] public float spawningDelayModifier; //Absolute speed to be modified by the spawning mode. Grab it's default value from data object
        [HideInInspector] public float elementalsSpeed;
        #endregion

        #region Public Events
        
        public UnityEvent onSpawning;

        #endregion

        #region Protected Variables
        protected int spawnedObjects;
        protected bool isActive = true;
        protected float timeToSpawn;
        protected float remainingTimeToSpawn;
        #endregion

        #region Porperties
        public float RemainingTimeToSpawn => remainingTimeToSpawn;
        public bool IsActive { get => isActive; set => isActive = value; }
        public int SpawnedObjects { get => spawnedObjects; }
        
        #endregion


        #region Abstract Methods
        public abstract void Initialize();
        public abstract void setOnSpawners();
        public abstract void setOffSpawners();
        public abstract void Execute();
        public abstract List<T> GetSpawners();
        #endregion

        #region Main Mehtods


        public virtual void refreshSpawningDelay()
        {
            Debug.Log("Refreshing Spawning time delay");
            timeToSpawn = spawningModeManager.CurrentModeCentriped.timeBetweenSpawnings * spawningDelayModifier;
            remainingTimeToSpawn = timeToSpawn;
        }

        public void ObjectsSpawnedPlusOne()
        {
            spawnedObjects++;
        }
        #endregion
    }
}
