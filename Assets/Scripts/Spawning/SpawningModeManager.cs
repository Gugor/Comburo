using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Comburo.ScriptableObjects;
using System;

namespace Comburo
{
    public class SpawningModeManager : MonoBehaviour
    {
        #region Public Variables
        public Brain brain;
        public GameManager gameManager;
        public SpawnerManager spawnerManager;
        public WaveSpawnerManager waveSpawnerManager;
        public SpawningModesPoolSO defaultSpawninModePool;
        public UnityEvent onChangeMode;
        #endregion
        #region Private Variables
        private Tools.CallBackTimer<Action> changeModeTimer;
        private float timeToChangeMode;
        private float remainingTimeToChangeMode;
        private SpawningModesPoolSO currentspawningModesPoolSO; // Seems to be unused
        private SpawningModeManagerSO[] currentSpawningModesManager; //A list of the current spawning mode managers to be use in the spawner managers
        private SpawningModeManagerSO currentSpawningModeManager; //The current spawning mode to be used in the spawning managers
        private SpawningModeSO currentModeCentriped; //The current spawning mode extract from the current spawning mode manager to be used in spawner manager centriped
        private SpawningModeWaveSO currentModeWave; //Same but in wave spawner manager
        private int poolIndex; //The index from the current spawning mode manager in the collection of the current spawning mode managers
        private bool isActive = true;
        #endregion

        #region Properties
        public float RemainingTimeToChangeMode { get => remainingTimeToChangeMode; }
        public SpawningModeSO CurrentModeCentriped { get => currentModeCentriped; }
        public SpawningModeWaveSO CurrentModeWave { get => currentModeWave; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public SpawningModeManagerSO[] CurrentSpawningModePool { get => currentSpawningModesManager; }
        #endregion

        #region Builtin Methods
        private void Awake()
        {
            defaultSpawninModePool = brain.defaulSpawningModesPoolSO;
            currentSpawningModesManager = defaultSpawninModePool.spawningModesManagersPool;
        }

        void Start()
        {
            changeModeTimer = new Tools.CallBackTimer<Action>(timeToChangeMode,Time.deltaTime,true,ChangeMode);
            currentSpawningModeManager = currentSpawningModesManager[0];
            Refresh();

            //Make spawnerManager sucribe to onChangeMode event.
            if (spawnerManager.gameObject.activeSelf) 
            { 
                onChangeMode.AddListener(spawnerManager.Initialize);
            }
            if (waveSpawnerManager.gameObject.activeSelf)
            {
                onChangeMode.AddListener(waveSpawnerManager.Initialize);
            }
            //gameManager.onPausedGame.AddListener(ToogleActive);
            //gameManager.onUnpauseGame.AddListener(ToogleActive);
            //gameManager.onGameOver.AddListener(ToogleActive);
            //gameManager.onRestartGame.AddListener(ToogleActive);

        }

        // Update is called once per frame
        void Update()
        {
            if (IsActive)
            {
                CoolDownChangeMode();
            }
        }
        #endregion

        #region Main Methods
        /// <summary>
        /// <para>Refresh the data to be used for the current spawnining mode</para>
        /// </summary>
        public void Refresh()
        {
            if (currentSpawningModeManager == null) 
            {
                currentSpawningModeManager = defaultSpawninModePool.spawningModesManagersPool[0];
            }

            Debug.Log("Refreshing mode manager: " + currentSpawningModeManager.ToString());
            timeToChangeMode = currentSpawningModeManager.timeToChangeMode;
            changeModeTimer.Time = timeToChangeMode;
            remainingTimeToChangeMode = changeModeTimer.RemainingTime;
            
            //Cheking if we need to use CentripedSpawningMode
            if (currentSpawningModeManager.UseCentripedSpawningMode) 
            {
                if (spawnerManager.gameObject.activeSelf)
                {
                    currentModeCentriped = currentSpawningModeManager.SpawningModeSO;
                    spawnerManager.currentMode = currentModeCentriped;
                    spawnerManager.Initialize();
                    currentModeCentriped.SetSpawneablesInCSS();
                }
            }

            //Cheking if we need to use WaveSpawningMode
            if (currentSpawningModeManager.UseWaveSpawningMode) 
            {
                if (waveSpawnerManager.gameObject.activeSelf)
                {
                    currentModeWave = currentSpawningModeManager.spawningModeWaveSO;
                    waveSpawnerManager.IsActive = currentModeWave.useWaves;
                    waveSpawnerManager.currentSpawningMode = currentModeWave;
                    waveSpawnerManager.Initialize();
                }
            }


            SelectSpawningType(); //Think this should be in SpawnerManager
            
        }


        /// <summary>
        /// <para>Changes the current spawning mode. Callback in the changeModetimer</para>
        /// </summary>
        private void ChangeMode()
        {
            SpawningModeManagerSO mode = selectNextSpawningModeManager(currentSpawningModesManager);

            Debug.Log("Changing mode manager: " + currentSpawningModeManager);
            onChangeMode.Invoke();
            

            Refresh();
        }

        // Probably this should go in the spawning manager not here
        /// <summary>
        /// <para>Selects the spawning type from this mode into de spwaner manager spawner machine. Type: Paralel || Secuencial</para>
        /// </summary>
        private void SelectSpawningType()
        {
            Debug.Log("Select new Spawning Type Machine: " + currentSpawningModeManager.SpawningModeSO.spawningMachineType);

            if (currentSpawningModeManager.SpawningModeSO.spawningMachineType == SpawningMachineType.Paralel)
            {
                spawnerManager.spawnerMachine = new ParalelSpawningMachine(spawnerManager, currentSpawningModeManager.SpawningModeSO.timeBetweenSpawnings * spawnerManager.spawningDelayModifier);
                spawnerManager.spawnerMachine.Init();
            }
            if (currentSpawningModeManager.SpawningModeSO.spawningMachineType == SpawningMachineType.Secuencial)
            {
                spawnerManager.spawnerMachine = new SecuencialSpawningMachine(spawnerManager.spawnersPool, currentSpawningModeManager.SpawningModeSO.objectsToSpawnInCentripedSpawningSystem ,currentModeCentriped.timeBetweenSpawnings * spawnerManager.spawningDelayModifier);
                spawnerManager.spawnerMachine.Init();
            }
        }

        /// <summary>
        /// <para>Checks when is time to change to the next spawning mode</para>
        /// </summary>
        public void CoolDownChangeMode()
        {
            if (changeModeTimer == null) { return; }
            if (changeModeTimer.isStopped) { return; }

            remainingTimeToChangeMode = changeModeTimer.RemainingTime;

            Debug.Log("Remaining time to change mode: " + RemainingTimeToChangeMode);
            changeModeTimer.UpdateTime(Time.deltaTime);
            //Debug.Log("Remaining Time to change mode: " + remainingTimeToChangeMode);
        }

        /// <summary>
        /// Select next data pool from where retrive spaning mode data for the current mode.
        /// </summary>
        /// <param name="pool">The pool of Spawning Mode Managers where to retrive current spawning mode</param>
        /// <returns>Return a spawning mode manager data object with the data for the current spawning mode</returns> 
        public SpawningModeManagerSO selectNextSpawningModeManager(SpawningModeManagerSO[] pool)
        {
            //May be we would need to be sure that we can only call this function
            //if pool is not null and length is greater than 0
            if (poolIndex >= pool.Length)
            {
                //This means we reach the max length of the array and we need to retrieve another difficulty pool
                //Set index to 0 in order to start counting again
                poolIndex = 0;
                //Go get another pool data
                currentSpawningModesManager = getNextDifficultyModesPool();
                Debug.Log("Selectin next spawning mode: ");
                return selectNextSpawningModeManager(currentSpawningModesManager);
            }

            //Check if Next Spawning Mode is not null
            if (pool[poolIndex] == null)
            {
                return selectNextSpawningModeManager(pool);
            }
            else
            {
                currentSpawningModeManager = pool[poolIndex];
                poolIndex++;
                return currentSpawningModeManager;
            }
                    //Set next Index
        }

        //Think this should be return in SpawnerManager
        private bool CheckIfModeHasCentralSpawneables()
        {
            return CurrentModeCentriped.objectsToSpawnInCentripedSpawningSystem.Count > 0;
        }

        private SpawningModeManagerSO[] getNextDifficultyModesPool()
        {
            Debug.Log("Getting new difficulty mode pool, inside " + this.name);
            return brain.getNextDifficultyModesPool().spawningModesManagersPool;
        }


        public override string ToString()
        {
            return String.Format("Mode Manager =>{0} used at time: {1}", currentSpawningModeManager.name, Time.deltaTime);
        }
        #endregion
    }
}

