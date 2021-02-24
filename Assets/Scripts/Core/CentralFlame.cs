using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Comburo
{
    public class CentralFlame : MonoBehaviour
    {
        public float playerGrowRate = 0.5f;
        public SpawnerManager spawnerManager;
        public ScoreManager scoreManager;
        public GameObject unscoreGO;
        
        private ScoreDisplayManager scoreDisplay;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Drop"))
            {
                
                if (other.GetComponent<Elemental>())
                {
                    Elemental elemental = other.GetComponent<Elemental>();
                    scoreManager.AddScore(-elemental.score/10);

                    StartCoroutine(DisplayScore(elemental));

                    elemental.Desapear();

                }
            }

            IEnumerator DisplayScore(Elemental elemental)
            {
                GameObject scoreDisplayGO = Instantiate(unscoreGO, transform);

                scoreDisplayGO.transform.position = transform.position;

                scoreDisplay = scoreDisplayGO.GetComponent<ScoreDisplayManager>();
                scoreDisplay.sorable = elemental.GetComponent<IScorable>();
                
                Animator anim = scoreDisplay.GetComponentInChildren<Animator>();
                anim.Play(0);

                yield return new WaitForSeconds(anim.runtimeAnimatorController.animationClips[0].averageDuration);

                if (scoreDisplayGO == null) { yield return null; } 
                Destroy(scoreDisplayGO);
            }

        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Player")
            {
                Grow grow = other.GetComponentInChildren<Grow>();
                StartCoroutine(grow.Increase(playerGrowRate));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                other.GetComponentInChildren<Grow>().setAutoGrow(false);
                other.GetComponentInChildren<Grow>().setAutoGrow(true);
            }
        }
    }
}
