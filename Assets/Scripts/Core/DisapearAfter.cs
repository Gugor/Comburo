using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearAfter : MonoBehaviour
{
    public float timeToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DestroyAfter", timeToDestroy);
    }

    IEnumerator DestroyAfter(float s)
    {
        yield return new WaitForSeconds(s);
        Destroy(gameObject);
    }
}
