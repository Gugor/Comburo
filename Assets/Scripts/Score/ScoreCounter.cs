using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Comburo
{
    public class ScoreCounter : MonoBehaviour
    {

        public TextMeshProUGUI scoreDisplay;
        [SerializeField] private ScoreManager scoreManager;

        private Animator anim;

        float waitTime = 0.5f;
        float currentWaitTime;
        int maxScore;
        int currentCount;
        bool canCount = false;
        bool hasFinishedCounting = false;
        // Start is called before the first frame update
        void Start()
        {
            maxScore = scoreManager.Score;
            currentWaitTime = waitTime;
            anim = GetComponent<Animator>();
            //Counter();
        }

        private void OnEnable()
        {
            hasFinishedCounting = false;

        }

        private void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).pressure > 0.5f)
            {
                // Debug.Log("Screen touched: " + Input.touchCount);
                canCount = false;
                hasFinishedCounting = true;
                scoreDisplay.text = maxScore.ToString();
                return;
            }
            else if (currentWaitTime <= waitTime && !hasFinishedCounting)
            {
                canCount = true;
                currentCount++;
            }
            else
            {
                currentWaitTime = waitTime;
            }

            if (canCount)
            { 
                Counting();
            }
        }


        public void Counter()
        {
          StartCoroutine(ICounting());
        }

        public void Counting()
        {
            //Debug.Log("Starting score count");

            if (currentCount <= maxScore)
            {

                //Debug.Log(currentCount <= maxScore);
                currentCount++;
                scoreDisplay.text = currentCount.ToString();
                anim.Play("HitScoreCounter");
                canCount = false;
            }

        }

        public IEnumerator ICounting()
        {
            Debug.Log("Starting score count");
            yield return new WaitForSeconds(1);
            Debug.Log("One second waited");
            while (currentCount <= maxScore)
            {

                Debug.Log(currentCount <= maxScore);
                currentCount++;
                scoreDisplay.text = currentCount.ToString();
                anim.Play("HitScoreCounter");
                //canCount = false;
                yield return new WaitForSeconds(anim.runtimeAnimatorController.animationClips[0].averageDuration);
            }

        }
    }
}

