using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Brackeys Code special thanks https://www.youtube.com/watch?v=9A9yj8KnM8c&ab_channel=Brackeys.
namespace Comburo
{
    public class CameraShaking : MonoBehaviour
    {
        public IEnumerator Shake(float duration, float magnitude)
        {
            Vector3 originalPos = transform.localPosition;

            float elapsed = 0.0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float z = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = new Vector3(x, originalPos.y, z);

                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.localPosition = originalPos;
        }
    }
}
