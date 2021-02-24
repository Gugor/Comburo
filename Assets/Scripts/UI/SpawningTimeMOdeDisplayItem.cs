
using UnityEngine;
using TMPro;

namespace Comburo
{
    public class SpawningTimeMOdeDisplayItem : MonoBehaviour
    {
        public SpawningModeManager spawningModeManager;
        public TextMeshProUGUI textDisplay;

        // Update is called once per frame
        void Update()
        {
            if (textDisplay != null)
            {
                float num = Mathf.Round(spawningModeManager.RemainingTimeToChangeMode);
                textDisplay.text = num.ToString();
            }
        }
    }
}
