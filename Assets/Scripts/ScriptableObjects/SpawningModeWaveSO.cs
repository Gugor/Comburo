using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Comburo", menuName = "Comburo/SpawningSystem/WaveSpawningModeData")]
    public class SpawningModeWaveSO : AbstractSpawningModeSO
    {
        [Space(10)]
        [Header("Waves for Directional Spawning System")]
        public bool useWaves;
        public List<Wave> waves = new List<Wave>();

        
    }
}
