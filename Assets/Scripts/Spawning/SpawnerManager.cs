using System;
using System.Collections;
using System.Collections.Generic;
using Comburo.ScriptableObjects;
using UnityEngine;

namespace Comburo
{

    public class SpawnerManager : AbstractSpawnerManager<Spawner>
    {
        #region Public Variables
        public SpawningModeSO currentMode;
        [Range(0, 20)]
        [Space(10.0f)]
        [Tooltip("Chose the prefabs you want to spawn.")]
        public List<ISpawneable> objectsToSpawn = new List<ISpawneable>();
        [Space(10)]
        [SerializeField] private SpawningModesPoolSO currentdifficultyModeData;

        [Space(10.0f)]
        [HideInInspector] public List<Spawner> customSpawners = new List<Spawner>();
        public List<Spawner> activeSpawners = new List<Spawner>();

        public int sumWeight; //The sum of the weight of all the spawning objects

        public List<Elemental> nemesis;

        [HideInInspector] public int minTimeToChangeMode = 10;
        [HideInInspector] public int maxTimeToChangeMode = 30;
        #endregion



        private void Awake()
        {
            
            InitSpawnManagerData(defaultData);

        }

        void Start()
        {
            //Initialize default data
            Debug.Log("Starting " + this.name);
            spawnedObjects = 0;
            remainingTimeToSpawn = 0;
            
            Debug.Log("Start defaul mode: " + spawningModeManager.CurrentModeCentriped.name);

        }

        // Update is called once per frame
        void Update()
        {
        }

        //Every Time a mode Change you should Initialize the data.
        public void InitSpawnManagerData(SpawnerManagerData data)
        {
            elementalsSpeed = data.elementalsSpeed * brain.difficulty;
            minTimeToChangeMode = data.minTimeToChangeMode;
            maxTimeToChangeMode = data.maxTimeToChangeMode;
            spawningDelayModifier = data.spawningDelay;
        }

        /// <summary>
        /// <para>Initizlize the data for the next spawning mode in spawnin manager</para>
        /// </summary>
        public override void Initialize()
        {
            Debug.Log("Intializing new Spawning mode data in " + this.name);
            IsActive = false;
            setOffSpawners();
            Debug.Log(spawningModeManager.ToString());
            Debug.Log(spawningModeManager.CurrentModeCentriped.ToString());
            Debug.Log(defaultData.ToString());
            elementalsSpeed = defaultData.elementalsSpeed * spawningModeManager.CurrentModeCentriped.elementalSpeedModifier;
            
            Debug.Log("New mode: " + spawningModeManager.CurrentModeCentriped.name);
            Debug.Log("Spawneables list. Num of elements " + spawningModeManager.CurrentModeCentriped.centripedSystemspawneables.Count);
            objectsToSpawn = spawningModeManager.CurrentModeCentriped.centripedSystemspawneables;

            setWeights(spawningModeManager.CurrentModeCentriped);

            //PickRandomTimeToChangeMode();
            refreshSpawningDelay();
            SelectSpawnersPool();
            buildCustomSpawnerList();
            setOnSpawners();

            IsActive = true;
        }

        private void setWeights(SpawningModeSO mode)
        {
            Debug.Log("Setting weights");
            List<Weighted> weighteds = mode.objectsToSpawnInCentripedSpawningSystem;

            for(int i = 0; i < weighteds.Count; i++)
            {
                weighteds[i].spawneablePrefab.GetComponent<ISpawneable>().SpawningWeight = weighteds[i].weight; 
            }
        
            sumWeight = getSpawningWeight();
        }


        public override void setOnSpawners()
        {
            Debug.Log("Setting on spawners");
            if (spawningModeManager.CurrentModeCentriped.offsetEveryOf >= 1)
            {
                for (int i = spawningModeManager.CurrentModeCentriped.startingSpawner; i < activeSpawners.Count; i += spawningModeManager.CurrentModeCentriped.offsetEveryOf)
                {
                    activeSpawners[i].canSpawn = true;
                    onSpawning.AddListener(activeSpawners[i].SpawnRandom);
                }
            }
        }

        public override void setOffSpawners()
        {
            Debug.Log("Setting off spawners in " + this.name);
            foreach (Spawner sp in activeSpawners)
            {
                sp.canSpawn = false;
                onSpawning.RemoveListener(sp.SpawnRandom);
            }
        }

        public void buildCustomSpawnerList()
        {
            foreach (CustomSpawner spawner in spawningModeManager.CurrentModeCentriped.cutomSpawners)
            {
                if (spawner.index >= spawningModeManager.CurrentModeCentriped.cutomSpawners.Count)
                {
                    continue;
                }
                else
                {
                    Spawner sp = spawnersPool[spawner.index];
                    customSpawners.Add(sp);
                }
            }

        }

        /// <summary>
        /// Decides which spawner pool to use: the default or custom one (wich is lead by Current Spawning Mode)
        ///</summary>
        public void SelectSpawnersPool()
        {
            if (spawningModeManager.CurrentModeCentriped.useCustomSpawners)
            {
                
                if (activeSpawners.Count <= 0)
                {
                    activeSpawners = spawnersPool;
                }
                else
                {
                    activeSpawners = customSpawners;
                }
            }
            else
            {
                activeSpawners = spawnersPool;
            }
        }



        //<sumary>
        //Add the wheight of the objects to spawn and returns it.
        //</sumary>
        int getSpawningWeight()
        {
            Debug.Log("Getting spawning weight.");
            int sum = 0;
            foreach (Weighted weighted in currentMode.objectsToSpawnInCentripedSpawningSystem)
            {
                sum += weighted.weight;
            }
            return sum;
            //return spawningModeManager.CurrentMode.fireElementalWeight + spawningModeManager.CurrentMode.waterElementalWeight + spawningModeManager.CurrentMode.nemesisElementalWeight;
        }

        public override void Execute()
        {
            if (spawnerMachine != null && IsActive)
            {
                setOnSpawners();
                spawnerMachine.Use();
            }
        }

        public override List<Spawner> GetSpawners()
        {
            return spawnersPool;
        }

        public override string ToString()
        {
            return String.Format("{0} owns {1} spawners.", this.name, spawnersPool.Count);
        }
    }
}
