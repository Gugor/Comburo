using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Comburo", menuName = "Comburo/SpawningSystem/SpawningModeManager")]
    public class SpawningModeManagerSO : ScriptableObject
    {
        [Header("Main Settings")]
        public float timeToChangeMode;
        
        [Space(10)]

        [Header("Centriped Spawning Mode")]
        public bool UseCentripedSpawningMode;
        public SpawningModeSO SpawningModeSO;
        
        [Header("Wave Spawning Mode")]
        public bool UseWaveSpawningMode;
        public SpawningModeWaveSO spawningModeWaveSO;
        public bool canBeLoaded;
        public void OnValidate()
        {
            bool loaded = false ;
            if (UseCentripedSpawningMode)
            {
                if (SpawningModeSO != null)
                {
                    loaded = true;
                }
                else 
                {
                    loaded = false;
                }
            }

            if (UseWaveSpawningMode)
            {
                if (spawningModeWaveSO != null)
                {
                    loaded = true;
                }
                else
                {
                    loaded = false;
                }
            }

            canBeLoaded = loaded;

        }
    }
}
