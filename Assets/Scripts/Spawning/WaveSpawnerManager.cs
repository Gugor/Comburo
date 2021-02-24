using System.Collections;
using System.Collections.Generic;
using Comburo.ScriptableObjects;
using UnityEngine;

namespace Comburo
{
    public class WaveSpawnerManager : AbstractSpawnerManager<SpawnerWave>
    {
        #region Public Variables
        public SpawningModeWaveSO currentMode;
        public List<Enemy> enemiesSpawned;
        [HideInInspector] public SpawningModeWaveSO currentSpawningMode;
        #endregion
        #region Private Variables
        private List<Wave> waves = new List<Wave>();
        private int waveIndex;
        #endregion

        #region Properties
        public int WaveIndex => waveIndex; 
        #endregion

        #region Inherited Methods
        public override void Initialize()
        {
            waves = spawningModeManager.CurrentModeWave.waves;
            
        }

        public override void setOffSpawners()
        {
            //throw new System.NotImplementedException();
        }

        public override void setOnSpawners()
        {
            //throw new System.NotImplementedException();
        }
        #endregion

        public override void Execute()
        {
            SpawnWave(waveIndex);
        }

        public void SpawnWave(int index)
        {
            Wave wave = waves[index];
            for (int i = 0; i < wave.waveDataItem.Count; i++)
            {
                WaveDataItem dataItem = wave.waveDataItem[i];
                dataItem.spawneable.Initialize("nemesis_" + i,dataItem.spawner.transform,dataItem.itemSpeed,dataItem.spawneable.iTransform.position);

            }
        }

        public override List<SpawnerWave> GetSpawners()
        {
            return spawnersPool;
        }
    }
}
