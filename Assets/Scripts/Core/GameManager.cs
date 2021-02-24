using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Comburo
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {            
                return _instance;
            }
        }

        [Header("Dependencies")]
        public SaveSystem saveSystem;
        public ScoreManager scoreManager;

        [Space(10)]
        public bool isFirstOpen = true;

        [Space(10)]
        [Header("Events")]
        public UnityEvent onStartGame;
        public UnityEvent onPausedGame;
        public UnityEvent onUnpauseGame;
        public UnityEvent onRestartGame;
        public UnityEvent onGameOver;
        public UnityEvent onDataLoadError;

        [Header("Settings")]
        public bool canRecordTime = false;
        private float gamePlayElapsedTime;

        private bool _isPaused;
        public bool IsPaused => _isPaused;
        public float GamePlayElapsedTime => gamePlayElapsedTime;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                
            }
            
            int firstOpen = PlayerPrefs.GetInt("isFirstOpen");
            
            if (PlayerPrefs.HasKey("isFirstOpen"))
            {
                isFirstOpen = false;
            }

            saveSystem = GetComponent<SaveSystem>();
            scoreManager = GetComponent<ScoreManager>();
        }

        public void Start()
        {
            Time.timeScale = 1;

            if(onStartGame != null)
            onStartGame.Invoke();

        }

        private void Update()
        {

            gamePlayElapsedTime += Time.deltaTime;
        }

        public void StartCounting(bool t)
        {
            canRecordTime = t;
        }

        public void LoadScene(string level)
        {
            if(level != null)
            SceneManager.LoadScene(level);
        }

        public void Pause()
        {
            _isPaused = true;
            Time.timeScale = 0;
            //if(onPausedGame != null)
            onPausedGame.Invoke();
        }

        public void unPause()
        {
            _isPaused = false;
            if (onUnpauseGame != null)
                onUnpauseGame.Invoke();
            Time.timeScale = 1;
        }

        public void Restart()
        {
            if(onRestartGame != null)
            onRestartGame.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
            if (SceneManager.GetActiveScene().name == "MainLevel")
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }

        public void GameOver()
        {
            if (onGameOver == null)
            {
                onGameOver = new UnityEvent();
            }
            
                Debug.Log("Game Over");
                onGameOver.Invoke();
                Time.timeScale = 0;
                saveSystem.Save(scoreManager.Score);

        }

        public void UnableToLoadData() 
        {
            Debug.Log("Spawning data is missing. Please set spawning data in order to load game.");
            onDataLoadError.Invoke();
        }
        public void SaveScore(int score)
        {
            PlayerPrefs.SetInt("score", score);
            PlayerPrefs.Save();
        }
    }
}