using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{
    public class Tutorial : MonoBehaviour
    {
        public GameManager gameManager;
        public GameObject tutorialPanel;

        // Start is called before the first frame update
        void Start()
        {
            if (gameManager.isFirstOpen)
            {
                Debug.Log("First time game opens");
                tutorialPanel.SetActive(true);
                gameManager.Pause();
            }
            else
            {
                tutorialPanel.SetActive(false);
            }

        }

        public void Close()
        {
            //Debug.Log("Closing tutorial");
            gameManager.isFirstOpen = false;
            tutorialPanel.SetActive(false);
            gameManager.unPause();
        }

        
    }
}
