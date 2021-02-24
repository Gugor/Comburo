
using UnityEngine;
using TMPro;
namespace Comburo
{
    public class SaveScoreDispalyItem : MonoBehaviour
    {
        public SaveSystem saveSystem;
        public TextMeshProUGUI scoreDisplay;
        // Start is called before the first frame update
        void Start()
        {
            if (saveSystem != null)
            {
                if (saveSystem.LoadScore() != -1)
                {
                    scoreDisplay.text = saveSystem.LoadScore().ToString();
                }
            }
        }

    }
}
