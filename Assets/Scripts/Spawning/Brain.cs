using UnityEngine;
using Comburo.ScriptableObjects;
using System.Collections.Generic;

namespace Comburo
{
    public class Brain : MonoBehaviour
    {
        /**********************************************************************
         * + We need to check beforehand if the game can run
         * + check if the 3 SpawningModesPoolSO[] are not empty or null
         * + check every mode manager inside and see if is set
         * 
         *********************************************************************/
        /********************************************************************
         * ====== Understanding the spawning mode selection sequence =======
         * We have 3 difficulty pools : [ Restfull, Active, Intense ] 
         * Those are arrays (data pools) from which we pull spawning mode managers data [SpawningModesPool, ...N]
         * Those mode managers contains 2 specific Spawner Manager wich controls 2 different spawning systems: CentripedSpawnerManager, WaveSpawnerManager
         * We need to iterate over the difficulty mode array once at a time and retrive a random SpawningModePool
         * Once we have this pool we need to iterate over it again  -one index at time- in order to retrive the SpawningModeManager wich controls the Spawner Managers on current mode
        ***********************************************************************/

        public SpawningModesPoolSO defaulSpawningModesPoolSO;
        [Range(1.0f, 10.0f)]
        public float difficulty;
        public bool isRandomDifficulty = false;

        //public SpawnerManager[] spawnerManagers;

        private int protectionIndex = 3;

        [Header("Modes Pools")]

        public SpawningModesPoolSO[] restingModesPool; //Is a 1d array of SpawningModesPoolSO from low difficulty level
        public SpawningModesPoolSO[] activeModesPool;
        public SpawningModesPoolSO[] intenseModesPool;
        [Space(10)]
        public SpawningModesPoolSO[] currentSpawningModesPool; //A set of differents spawning modes from a same difficulty
        public SpawningModesPoolSO[] previousSpawningModesPool;
        [Space(10)]
        public SpawningModesPoolSO currentSpawningModeGroup;
        public SpawningModesPoolSO previousSpawningModeGroup;

        public int timesDifficultyPoolHasChanged = 0;
        public int timesDifficultyPoolHasTriedToChange = 0;

        private int difficultyIndex = 0;
        private Dictionary<string, SpawningModesPoolSO[]> loadedDifficultyData;


        public void Start()
        {
            loadedDifficultyData = new Dictionary<string, SpawningModesPoolSO[]>();
            loadDifficultyData();
        }
        /// <summary>
        /// <para>Retrieves a SpawningModePoolSO wich spawning modes to be used in a spawn manager</para>
        /// </summary>
        /// <returns></returns>
        public SpawningModesPoolSO getNextDifficultyModesPool()
        {
            //Get the current difficulty pool
            SpawningModesPoolSO[] temp = selectDifficultyModePool(setNextDifficultyIndex());

            if (temp.Length <= 0)
            {
                if (protectionIndex <= 0)
                {
                    protectionIndex = 3;
                    //It should open a UI screen showing there are no data to retrive.
                    //Game can't start cause this.
                    Debug.Log("Prevening infinite loop caused by insufficient mode settings. Protection Index: " + protectionIndex);
                    GameManager.Instance.Pause();
                    return null;
                }
                else
                {
                    Debug.Log("Getting new difficulty mode pool:" + currentSpawningModesPool.ToString() + "Index: " + difficultyIndex);
                    protectionIndex--;
                    getNextDifficultyModesPool(); //While its null will try to pick another DifficultyMode.
                    return currentSpawningModeGroup;

                }
            }
            else 
            { 
                Debug.Log("Getting new difficulty mode pool:" + currentSpawningModesPool.ToString() + "Index: " + difficultyIndex);
                previousSpawningModesPool = currentSpawningModesPool;

                currentSpawningModesPool = temp;

                //Get the spawning modes group to set in spawning manager
                currentSpawningModeGroup = getRandomSpawningModesPool(currentSpawningModesPool);
                //If its null try again

                return currentSpawningModeGroup;
            }
        }

        /// <summary>
        /// <para>Select a spawning mode pool</para>
        /// </summary>
        /// <param name="difficultyPool">Array of SpawningModesPoolSO wich store spawning modes data</param>
        /// <returns></returns>
        private SpawningModesPoolSO getRandomSpawningModesPool(SpawningModesPoolSO[] difficultyPool)
        {
            if (difficultyPool.Length <= 0) { return null; }

            int index = Random.Range(0, difficultyPool.Length - 1);
            Debug.Log("Spawning Mode pool length => " + difficultyPool.Length + ".// Spawning Mode Pool index => " + index);

            SpawningModesPoolSO temp = difficultyPool[index];
            
            if (temp.spawningModesManagersPool.Length <= 0)
            {
                getRandomSpawningModesPool(difficultyPool);
                return null;
            }
            previousSpawningModeGroup = currentSpawningModeGroup;
            currentSpawningModeGroup = temp;
            
            return currentSpawningModeGroup;
        }
        
        /// <summary>
        /// <para>Set the next difficulty index in the game</para>
        /// </summary>
        /// <returns></returns>
        private int setNextDifficultyIndex()
        {
            Debug.Log("Setting new difficulty Index: Previous => " + difficultyIndex);
            if (difficultyIndex > 2)
            {
                Debug.Log("Reseting difficulty index");
               return difficultyIndex = 0;
                
            }
            else
            {
                Debug.Log("Setting new difficulty Index: Next => " + (difficultyIndex + 1));
                return difficultyIndex++;
            }

        }


        /// <summary>
        /// Loads the difficulty modes in a dictionary and check if game can start. If no difficulty data pool has data charage default data pool.
        /// </summary>
        /// <returns>true if game has being loaded successfully, false otherwise.</returns>
        private bool loadDifficultyData() 
        {
            if (restingModesPool != null && restingModesPool.Length > 0 && restingModesPool[0] != null)
            {
                Debug.Log("Loading Restfull difficulty pool data");
                loadedDifficultyData.Add("Restfull",restingModesPool);
            }
            if (activeModesPool != null && activeModesPool.Length > 0 && activeModesPool[0] != null) 
            {
                Debug.Log("Loading Active difficulty pool data");
                loadedDifficultyData.Add("Active", activeModesPool);
            }
            if (intenseModesPool != null && intenseModesPool.Length > 0 && intenseModesPool[0] != null)
            {
                Debug.Log("Loading Intense difficulty pool data");
                loadedDifficultyData.Add("Intense", intenseModesPool);
            }

            if (loadedDifficultyData.Count <= 0)
            {
                Debug.Log("There is no data to be loaded, therefore game can't start");
                GameManager.Instance.UnableToLoadData();
                return false;
            }
            else 
            {
                Debug.Log("Data has being loaded successfully. Starting game...");
                return true;
            }

        }
        /// <summary>
        /// <para>Choose an array of SpawningModesPoolSO from the 3 main difficultiy array pools: Resting, Active and Intense</para>
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private SpawningModesPoolSO[] selectDifficultyModePool(int index)
        {
            string[] difficultyPoolsNames = { "Restfull", "Active", "Intense" };

            //SpawningModesPoolSO[] spawningModesPoolSOs = loadedDifficultyData[difficultyPoolsNames[index]];
            Debug.Log("Difficulty index selection: " + index);
            if (index == 0)
            {
                currentSpawningModesPool = restingModesPool;
                return restingModesPool;
            }
            else if (index == 1)
            {
                currentSpawningModesPool = activeModesPool;
                return activeModesPool;
            }
            else if (index == 2)
            {
                currentSpawningModesPool = intenseModesPool;
                return intenseModesPool;
            }
            else
            {
                return null;
            }

        }

        public bool CheckInGameDataStatus() 
        {
            //Bitwise operator with logic
            int difficultyPoolsCheck = 0;
            int isActiveRestFull = 1;
            int isActiveActive = 2;
            int isActiveIntense = 4;

            if (restingModesPool.Length > 0) difficultyPoolsCheck |= isActiveRestFull;
            if (activeModesPool.Length > 0) difficultyPoolsCheck |= isActiveActive;
            if (intenseModesPool.Length > 0) difficultyPoolsCheck |= isActiveIntense;
            return false;
        }

        

        /// <summary>
        /// <para>Sets a new global difficulty for the current game</para>
        /// </summary>
        public void setNewGlobalDifficulty()
        {
            difficulty += 0.01f;
        }

 

    }

}
