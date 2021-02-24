using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Comburo
{
    public class PausedPanel : MonoBehaviour
    {
        public GameObject panel;
        public GameManager gameManager;
        public Animator anim;
        

        // Start is called before the first frame update
        void Start()
        {
        }

        public void Open()
        {
            StartCoroutine(PlayOpen());
        }

        public IEnumerator PlayOpen()
        {
            panel.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            anim.Play("PanelFadeIn");

        }

        public void Close()
        {
            StartCoroutine(PlayClose());
        }

        public IEnumerator PlayClose()
        {
            anim.Play("PanelFadeOut");
            Debug.Log("Before wating paused fade out => s" + anim.runtimeAnimatorController.animationClips[1].averageDuration);
            
            gameManager.unPause();
            yield return new WaitForSeconds(anim.runtimeAnimatorController.animationClips[1].averageDuration);
            Debug.Log("After playing paused fade out");
            panel.SetActive(false);
            StopCoroutine(PlayClose());
        }
    }
}
