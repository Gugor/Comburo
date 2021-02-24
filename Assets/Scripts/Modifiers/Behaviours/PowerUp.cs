using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Comburo
{
    [RequireComponent(typeof(Rigidbody), typeof(PathCreation.Examples.PathFollower))]
    public class PowerUp : MonoBehaviour, IScorable
    {
        [Header("Dependencies")]
        public PowerUpManager puManager; //Editor referenced
        public ScoreManager scoreManager;
        public Animator anim; //Editor referenced
        public GameObject scoreDisplayPrefab;
        [Header("Settings")]
        public PowerUps powerUPType; //Editor referenced
        public Sprite iconSprite; //Editor referenced
        public float lifeTime;
        [SerializeField] private int score;
        public UnityEvent onDiyng;

        private AbstractPowerUpMachine powerUpMachine;
        private float currentLifeTime;
        
        private Rigidbody rb;

        public int Score { get => score;}

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            currentLifeTime = lifeTime;
        }

        void Update()
        {
            CountDownLifeTime();
        }

        protected void CountDownLifeTime()
        {
            currentLifeTime -= Time.deltaTime;
            if (currentLifeTime <= 0)
            {
                Desapear();
            }
        }

        public void Desapear()
        {
            Debug.Log("I desapear... ohhh");
            puManager.isPowerUpInWorld = false;
            Destroy(gameObject);
        }

        public IEnumerator getPicked()
        {
            Debug.Log("Getting picked...");

            //Say to the powerUp manager wich powerUp machine to use among them.
            puManager.setCurrentPowerUPMachine(powerUPType); 
            Debug.Log("Current Power Up: " + puManager.currentPower);


            //Confirm animator an play death
            if (anim != null)
            {
                Debug.Log(anim.name);
                Debug.Log("Diying... Spider...");
                anim.Play("Spider_Death");
                onDiyng.Invoke();
            }


            //Activate power btn
            puManager.powerUpBtn.SetActive(true);
            puManager.powerUpDisplayImage.gameObject.SetActive(true);
            puManager.powerUpDisplayImage.imageDisplay.enabled = true;
            puManager.powerUpDisplayImage.imageDisplay.sprite = iconSprite;

            yield return new WaitForSeconds(anim.runtimeAnimatorController.animationClips[1].averageDuration);
            
            Desapear();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Debug.Log("Player has entered...");
                StartCoroutine(getPicked());

                Player player = other.GetComponentInParent<Player>();
                Debug.Log("Power up score player: " + other.name + " || " + player);

                GetComponent<SphereCollider>().enabled = false;

                player.scoreManager.AddScore(score);

                ScoreDisplayManager display;
                player.scoreManager.DisplayScoreUI(scoreDisplayPrefab, transform, out display);
            }
        }

        public int getScore()
        {
            return score;
        }
    }
}

