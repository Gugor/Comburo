
using UnityEngine;


namespace Comburo
{

    public class SaveSystem : MonoBehaviour
    {

        private static SaveSystem _instance;
        public bool isFirstOpen;
        int numberOfTimesGameHasBeingOpened = 0;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                
            }
            if (PlayerPrefs.HasKey("IsFirstOpen"))
            {
                PlayerPrefs.DeleteKey("IsFirstOpen");
                PlayerPrefs.SetInt("IsFirstOpen", numberOfTimesGameHasBeingOpened);
            }
        }
        public static SaveSystem Instance
        {
            get
            {
                return _instance;
            }
        }

        public void Save(int Score)
        {
           PlayerPrefs.SetInt("IsFirstOpen",numberOfTimesGameHasBeingOpened);
           if (PlayerPrefs.GetInt("Score") <= Score)
           {
                    PlayerPrefs.SetInt("Score", Score);
                    PlayerPrefs.Save();
           }
        }

        public int LoadScore()
        {
            if (PlayerPrefs.HasKey("Score"))
            {
                //Debug.Log("Has key");
                //Debug.Log(PlayerPrefs.GetInt("Score",0));
                return PlayerPrefs.GetInt("Score",0);
            }
            else
            {
                return -1;
            }
        }
    }

}
