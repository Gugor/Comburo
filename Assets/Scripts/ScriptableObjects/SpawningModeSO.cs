using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Comburo", menuName = "Comburo/SpawningSystem/CentripedSpawningModeData")]
    public class SpawningModeSO : AbstractSpawningModeSO
    {

        [Header("Spawning Weights")]
        public List<Weighted> objectsToSpawnInCentripedSpawningSystem = new List<Weighted>();
        [HideInInspector] public List<ISpawneable> centripedSystemspawneables;

        public delegate void SpawnInSecuence();

        [HideInInspector] public SpawnInSecuence spawnInSecuence;

        [Header("Spawner Settings")]
        public bool useCustomSpawners;
        public List<CustomSpawner> cutomSpawners = new List<CustomSpawner>();

#if UNITY_EDITOR
        private void OnValidate()
        {
            SetOnEditorWeightedNames();
            SetSpawneablesInCSS();
        }

        private void SetOnEditorWeightedNames()
        {
            if (objectsToSpawnInCentripedSpawningSystem.Count >= 0) { return; }

            foreach (Weighted weighted in objectsToSpawnInCentripedSpawningSystem)
            {
                if (weighted == null) { return; }
                weighted.name = weighted.spawneablePrefab.ToString();
            }
        }

        /// <summary>
        /// <para>Sets the list of objects to be spawned in centriped spawning system (CSS for short)</para>
        /// </summary>
        public void SetSpawneablesInCSS()
        {
            if (objectsToSpawnInCentripedSpawningSystem.Count >= 0) { return; }

            Debug.Log("Setting Colectables...");
            Debug.Log("Colectables:" + objectsToSpawnInCentripedSpawningSystem.ToString());
            Debug.Log("Colectables:" + objectsToSpawnInCentripedSpawningSystem.Count);


            foreach (Weighted weighted in objectsToSpawnInCentripedSpawningSystem)
            {
                if (weighted == null) { return; }

                if (weighted.spawneablePrefab != null)
                {
                    centripedSystemspawneables.Add(weighted.spawneablePrefab.GetComponent<ISpawneable>());
                }
            }
        }
    #endif
    }
}