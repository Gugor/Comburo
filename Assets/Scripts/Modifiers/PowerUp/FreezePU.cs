using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{
    public class FreezePU : AbstractPowerUpMachine
    {
        public Animator screenEffectAnim;
        public GameObject effectPrefab;

        

        private float fadeInAnimTime;
        private float trappedAnimTime;
        private float fadeOutAnimTime;

        Elemental[] elementals;
        float currentUseTime;

        // Start is called before the first frame update
        void Start()
        {
            isBeingUsed = false;
            currentUseTime = useTime;
        }

        public override void Set()
        {
          
        }

        public override IEnumerator Use()
        {
            Debug.Log("Using " + this.name +  " Power Up");

 
        //[[[Internal configuration]]]
            isBeingUsed = true;
            

            //Activate screen spider web effect
            screenEffectAnim.gameObject.SetActive(true);

            //Unset the powerUp icon from the button
            powerUpManager.powerUpDisplayImage.unsetImage();
            Debug.Log("After screen animation");

            spawnerManager.setOffSpawners();

            //List carriyng the instantiated effects objects in the world. 
            List<GameObject> effects = new List<GameObject>();

            //Find all elementals in screen
            elementals = GameObject.FindObjectsOfType<Elemental>();
            Debug.Log("Freezing " + elementals.Length);


            //Find every elemental and
            foreach (Elemental elemental in elementals)
            {

                //Stop it
                if (elemental == null) { yield return null; }
                elemental.IsMoving = false;

                //Instantiate effect, set elemental as its parent and make it follow.
                GameObject instance = Instantiate(effectPrefab, elemental.transform);
                Debug.Log("Trap :" + instance);
                instance.transform.SetParent(elemental.transform);
                instance.transform.position = elemental.transform.position;
                effects.Add(instance);

                //Play fadein animation
                instance.GetComponent<Animator>().Play("SpiderTrap_FadeIn");

                //Get animations times. They will be used in this enumerator to now how long to wait until next action
                Animator instanceAnim = instance.GetComponent<Animator>();
                fadeInAnimTime = instanceAnim.runtimeAnimatorController.animationClips[0].averageDuration;
                trappedAnimTime = instanceAnim.runtimeAnimatorController.animationClips[1].averageDuration;
                fadeOutAnimTime = instanceAnim.runtimeAnimatorController.animationClips[2].averageDuration;


                
            }

            //Wait until screen animation is over
            yield return new WaitForSeconds(screenEffectAnim.runtimeAnimatorController.animationClips[0].averageDuration);

            //Turn off screen animation object
            screenEffectAnim.gameObject.SetActive(false);
            yield return new WaitForSeconds(fadeInAnimTime);

            Debug.Log("Changing state to trapped. Nº of traps:" + effects.Count);
            foreach (GameObject effect in effects)
            {
                if (effect != null && effect.GetComponent<Animator>() != null)
                {
                    Animator anim = effect.GetComponent<Animator>();

                    Debug.Log("Effect Animator: " + anim);

                    float randSpeed = Random.Range(1.0f, 1.3f); 
                    anim.speed = randSpeed;
                    anim.Play("SpiderTrap_Trapped");
                }
                yield return null;
            }

            yield return new WaitForSeconds(useTime);

            Elemental[] elementalsLasting = FindObjectsOfType<Elemental>();
            Debug.Log("Elementals after spider trap power up: " + elementalsLasting.Length);
            
            foreach (Elemental elemental in elementalsLasting)
            {
                if (elemental != null)
                {
                    if (elemental.GetComponentInChildren<SpiderTrapEffect>().GetComponent<Animator>())
                    {
                        Animator animEffect = elemental.GetComponentInChildren<SpiderTrapEffect>().GetComponent<Animator>();
                        animEffect.Play("SpiderTrap_FadeOut");
                        yield return new WaitForSeconds((fadeOutAnimTime + fadeOutAnimTime) / 3);
                        elemental.IsMoving = true;
                    }
                    else
                    {
                        yield return null;
                    }
                }
            }

            yield return new WaitForSeconds(.4f);

            fadeInAnimTime = 0;
            fadeOutAnimTime = 0;
            trappedAnimTime = 0;

            Elemental[] effectsDestroy = FindObjectsOfType<Elemental>();
            
            foreach (Elemental eff in effectsDestroy)
            {
                if (eff != null)
                {
                    Destroy(eff.gameObject.GetComponentInChildren<SpiderTrapEffect>());
                }
   
            }
            elementals = null;

            yield return new WaitForSeconds(1.0f);



            Extinguish();
        }


        public override void Extinguish()
        {
            if (powerUpManager == null) { return; }
            powerUpManager.currentPower = null;
            powerUpManager.startCoolDown = true;
            powerUpManager.isWorldClearToSpawn = true;
            isBeingUsed = false;
            spawnerManager.setOnSpawners();

        }
    }

}
