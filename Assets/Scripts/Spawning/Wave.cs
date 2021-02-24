using System.Collections;
using System.Collections.Generic;

namespace Comburo
{
    [System.Serializable]
    public class Wave 
    {
        public float timeToNextWave;
        public List<WaveDataItem> waveDataItem;
    }
}
