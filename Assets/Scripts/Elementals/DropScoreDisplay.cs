using System.Collections;
using UnityEngine;
using TMPro;

namespace Comburo
{
    public class DropScoreDisplay : MonoBehaviour
    {
        public ScoreDisplayManager scoreDisplayManager;
        public TextMeshProUGUI displayItem;
        public Animator anim;


        // Start is called before the first frame update
        public void Start ()
        {

            if (scoreDisplayManager != null && displayItem != null)
            {
                if (scoreDisplayManager.isUnScore)
                {
                    int negativeScore = -scoreDisplayManager.score;
                    displayItem.text = negativeScore.ToString();

                }
                else
                {
                    displayItem.text = scoreDisplayManager.score.ToString();
                }
            }

            if (anim != null)
            {
                anim.Play(0);

            }
        }


        
    }
}
