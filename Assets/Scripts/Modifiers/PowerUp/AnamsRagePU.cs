using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{
    public class AnamsRagePU : AbstractPowerUpMachine
    {
        public CameraShaking cameraShaking;
        public GameObject blastPrefab;
        public float explotionGrowRate;
        public float explotionDispersionSpeed;
        [SerializeField] private Transform anam;
        [SerializeField] private float maxBlastSize;

        public override void Extinguish()
        {
            
        }

        public override void Set()
        {
            
        }

        public override IEnumerator Use()
        {
            powerUpManager.powerUpDisplayImage.unsetImage();
            StartCoroutine(cameraShaking.Shake(0.6f,0.4f));
            Explode();
            yield return null;
        }

        void Explode()
        {
            GameObject instance =  Instantiate(blastPrefab);

            instance.transform.position = anam.position;
            Vector3 originalScale = instance.transform.localScale;

            if (originalScale.x >= maxBlastSize)
            {
                Destroy(gameObject);
            }

            instance.transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z) * explotionGrowRate * explotionDispersionSpeed;


        }
        IEnumerator Sequence()
        {
            yield return null;
        }


    }
}
