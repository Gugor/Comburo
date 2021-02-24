using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Comburo
{
    public class FireElemental : Elemental
    {
        [Header("Fire Settings")]
        public int growthAmount;
        public GameObject scoreNumberUI;
        public UnityEvent onPlayerTrigger;
        private Grow playerGrow;
        private bool isAlive = true;


        //Elemental =>
        public override void Initialize(string name, Transform target, Transform parent, Vector3 position, SpawnedPoolManager spawnedPool)
        {
            base.Initialize(name, target, parent, position, spawnedPool);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && isAlive)
            {
                
                playerGrow = other.GetComponent<Grow>();
                Player player = playerGrow.player;
                
                if (player != null)
                {

                    player.scoreManager.AddScore(score);
                    StartCoroutine(playerGrow.Increase(growthAmount));

                    ScoreDisplayManager display;
                    player.scoreManager.DisplayScoreUI(scoreNumberUI,transform, out display);

                    Explode();
                    Desapear();
                }
            }
        }

        public override void Desapear()
        {
            spawnerManager.spawnedPool.Remove(this);
            Destroy(gameObject);
        }
        private void Explode()
        {
            GameObject instance = Instantiate(explotion);
            instance.transform.position = transform.position;
        }

    }

}
