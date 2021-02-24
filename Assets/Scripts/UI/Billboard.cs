using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [HideInInspector] public Transform targetCamera;

    private Quaternion originalRotation;

    private void Awake()
    {
        targetCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    // Update is called once per frame
    void Update()
    {
        if(targetCamera != null)
        transform.LookAt(transform.position - targetCamera.position);
    }
}
