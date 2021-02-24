using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{
    public class PanelManager : MonoBehaviour
    {
        public RectTransform panel;
        public KeyCode keyCode;
        public bool pauseGame;


        private void Update()
        {
            if (Input.GetKeyDown(keyCode))
            {
                TogglePanel();
            }
        }

        public void showPanel()
        {
            Debug.Log("Show Panel: " + panel.name);
            panel.gameObject.SetActive(true);
            if (pauseGame)
            {
                GameManager.Instance.Pause();
            }
        }

        public void TogglePanel()
        {
            if (pauseGame)
            {
                if (GameManager.Instance.IsPaused)
                {
                    GameManager.Instance.unPause();
                }
                else
                {
                    GameManager.Instance.Pause();
                }
                panel.gameObject.SetActive(!panel.gameObject.activeSelf);
            }
            else
            {
                panel.gameObject.SetActive(!panel.gameObject.activeSelf);
            }
        }
    }
}
