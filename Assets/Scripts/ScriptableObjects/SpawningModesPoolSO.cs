using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo.ScriptableObjects
{
    [CreateAssetMenu(fileName ="Comburo",menuName = "Comburo/SpawningSystem/SpawninghModePool")]
    public class SpawningModesPoolSO : ScriptableObject
    {
        public string name;
        public DifficultyTpes difficulty;
        public SpawningModeManagerSO[] spawningModesManagersPool;
        public bool canBeLoaded;
        public override string ToString()
        {
            return string.Format("{0} is difficulty level {1} and has {2} spawningModeManagers");
        }

        private void OnValidate()
        {
            int length = spawningModesManagersPool.Length;
            if (length > 0) 
            { 
                SpawningModeManagerSO managerSO = spawningModesManagersPool[length - 1];
                if (!managerSO.canBeLoaded) 
                {
                    Debug.LogError("DataNotFound: " + spawningModesManagersPool[length-1].name  +  " cannot be loaded, because has none spawning mode in it, or it use is disable. Please add a spawning mode data object and try again.");
                    //Array.Resize(ref spawningModesManagersPool, spawningModesManagersPool.Length - 1); ;
                    checkSpawningModeManagerData();
                } 
            }

            if (spawningModesManagersPool.Length > 0 && spawningModesManagersPool[0] != null)
            {
                canBeLoaded = true;
            }
            else 
            {
                canBeLoaded = false;
            }
        }

        private SpawningModeManagerSO[] checkSpawningModeManagerData() 
        {
            SpawningModeManagerSO[] spmmso = new SpawningModeManagerSO[spawningModesManagersPool.Length - 1];
            int index = 0;
            foreach (var item in spmmso)
            {
                if (item.canBeLoaded)
                {
                    index++;
                    spmmso[index] = item; 
                }
            }

            return spmmso;
        }

    }
}
