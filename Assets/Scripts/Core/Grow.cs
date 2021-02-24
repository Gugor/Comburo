using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{
    public class Grow : MonoBehaviour
    {
        public Player player;
        public float growingElapsedTime = 0.7f;
        private bool canGrow = true;
        private SphereCollider col;
        private Vector3 minSize;
        private float minColRadius;
        private float maxSize = 1.3f;
        [SerializeField]private float lerpModifier = 10f;
        [SerializeField] private float lerpRadiusModifier = 1f;
        private float elapsedTime;

        public float ElapsedTime { get => elapsedTime; }
        public bool CanGrow { get => canGrow; set => canGrow = value; }

        private void Awake()
        {
            col = GetComponent<SphereCollider>();
            minSize = transform.lossyScale;
        }


        private void Start()
        {
            elapsedTime = growingElapsedTime;
            StartCoroutine(ConstantGrowing(1,elapsedTime));
        }


        public void setAutoGrow(bool auto)
        {
            CanGrow = auto;
        }

        public IEnumerator ConstantGrowing(int amount,float _elapsedTime)
        {

            while (CanGrow)
            {
                yield return new WaitForSeconds(_elapsedTime);
                StartCoroutine(Increase(amount));
                yield return null;
            }
            

        }

        public IEnumerator Increase(float amount)
        {
            if (amount <= 0) { yield return null; }
            Debug.Log("Player Increase...");
            float lScale = 0;


            if (transform.localScale.x < maxSize)
            {
                Vector3 targetScale = new Vector3(transform.lossyScale.x + (amount * player.growRate), transform.lossyScale.y, transform.lossyScale.z + (amount * player.growRate));
                while (lScale < 100f)
                {
                    
                    lScale += lerpModifier;
                    gameObject.transform.localScale = Vector3.Lerp(transform.localScale, targetScale, lScale * Time.deltaTime);


                    float lerpValue = Mathf.Lerp(col.radius, transform.lossyScale.x * 3, lScale * Time.deltaTime);
                    col.radius = lerpValue;

                    yield return null;
                }
                
            }
            else
            {
                Vector3 targetScale = new Vector3(maxSize, minSize.y, maxSize);

                while (lScale < 100f)
                {
                    lScale += lerpModifier;
                    transform.localScale = Vector3.Lerp(transform.localScale, targetScale, lScale * Time.deltaTime);

                    float lerpValue = Mathf.Lerp(col.radius, maxSize * 3, lScale * Time.deltaTime);
                    col.radius = lerpValue;

                    yield return null;
                }

            }
        }


        public IEnumerator Decrease(int amount)
        {
            if (amount <= 0) { yield return null; }

            //Checking value to be compared with the min value to be decreased;
            float checkScale = transform.lossyScale.x - (amount * player.growRate);

            float lScale = 0;

            //If player doesn't decrease less than de min scale value then...
            if (checkScale > minSize.x)
            {
                Vector3 targetScale = new Vector3(transform.lossyScale.x - (amount * player.growRate), transform.lossyScale.y, transform.lossyScale.z - (amount * player.growRate));



                while (lScale < 100f)
                {
                    lScale += lerpModifier;
                    //lRadius += lerpRadiusModifier;
                    gameObject.transform.localScale = Vector3.Lerp(transform.localScale, targetScale, lScale * Time.deltaTime);

                    if (col.radius > minColRadius)
                    {
                        float lerpValue = Mathf.Lerp(col.radius, transform.lossyScale.x * 3, lScale * Time.deltaTime); 
                        col.radius = lerpValue;
                    }
                    else
                    {
                        col.radius = 0.9f;
                    }
                    yield return null;
                }

                
                yield return null;
            }
            else
            {
                while (lScale < 100f)
                {
                    lScale += lerpModifier;
                    transform.localScale = Vector3.Lerp(transform.localScale, minSize, lScale);

                    float lerpValue = Mathf.Lerp(col.radius, .9f, lScale * Time.deltaTime);
                    col.radius = lerpValue;

                    yield return null;
                }
                    col.radius = .9f;
            }
        }

    }
}
