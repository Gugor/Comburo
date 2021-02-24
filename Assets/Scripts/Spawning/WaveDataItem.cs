using UnityEngine;

namespace Comburo
{
    [System.Serializable]
    public class WaveDataItem : CustomSpawner
    {
        public SpawnerWave spawner;
        public Enemy spawneable;
        [Range(0f, 100f)]
        public float itemSpeed = 1.0f;

    }
}
