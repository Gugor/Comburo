using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Comburo
{
    public class SpawningModeDisplayItem : MonoBehaviour
    {
        public SpawnerManager spawnerManager;
        public TextMeshProUGUI textDisplay;

        // Update is called once per frame
        void Update()
        {
            if (textDisplay != null)
            {
                textDisplay.text = spawnerManager.spawningModeManager.name;
            }
        }
    }

}
