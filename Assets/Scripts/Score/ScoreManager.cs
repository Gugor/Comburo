using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Comburo
{
    public class ScoreManager : MonoBehaviour
    {

        private static ScoreManager _instance;

        public static ScoreManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScoreManager();
                }

                return _instance;
            }
        }

        [HideInInspector] private int _score = 0;
        public int Score => _score;
        public UnityEvent onScored;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        public void AddScore(int score)
        {
            //onScored.Invoke();
            _score += score;
        }

        public GameObject DisplayScoreUI(GameObject scorePrefab,Transform parent, out ScoreDisplayManager scoreDisplayManager)
        {
            GameObject instance = Instantiate(scorePrefab, transform.position, Quaternion.identity);
            instance.transform.localPosition = parent.position;

            scoreDisplayManager = instance.GetComponent<ScoreDisplayManager>();
            scoreDisplayManager.sorable = parent.GetComponent<IScorable>();
            scoreDisplayManager.gameObject.SetActive(true);

            return instance;
        }
    }
}
