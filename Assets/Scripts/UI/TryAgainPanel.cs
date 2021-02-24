using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryAgainPanel : MonoBehaviour
{
    public Animator paintCircle;
    // Start is called before the first frame update
    void Start()
    {
        paintCircle.Play("CircleSpin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
