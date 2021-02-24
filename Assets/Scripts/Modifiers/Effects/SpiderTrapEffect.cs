using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderTrapEffect : MonoBehaviour
{
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        FlipRandom();
    }

    void FlipRandom()
    {
        int ranX = Random.Range(0,1);
        int ranY = Random.Range(0, 1);

        if (ranX == 1)
        {
            sr.flipX = true;
        }

        if (ranY == 1)
        {
            sr.flipY = true;
        }
    }

}
