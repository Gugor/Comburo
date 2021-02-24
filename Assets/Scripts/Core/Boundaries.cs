using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public Transform inBoundsObject;
    public float yOffset;
    public Vector2 MarginsScreenPercentage;

    private float xMargin;
    private float yMargin;
    private Vector3 screenBounds;
    private float ObjectWidth;
    private float ObjectHeight;
    Vector3 viewPos;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        xMargin = screenBounds.x * 0.05f;
        yMargin = screenBounds.y * 0.05f;

    }

    // Update is called once per frame
    void Update()
    {

        ObjectWidth = (transform.lossyScale.x / 2)- ((transform.lossyScale.x / 2) - (inBoundsObject.lossyScale.x / 2));
        ObjectHeight = (transform.lossyScale.y / 2) - ((transform.lossyScale.y / 2) - (inBoundsObject.lossyScale.z / 2));
        viewPos = transform.position;

        viewPos.x = Mathf.Clamp(
            viewPos.x,
            (screenBounds.x - (ObjectWidth * 10) - xMargin) * -1,
            screenBounds.x - (ObjectWidth * 10) - xMargin);

        viewPos.z = Mathf.Clamp(
            viewPos.z,
            (screenBounds.y - (ObjectHeight * 10) - yOffset - yMargin) * -1,
            (screenBounds.y - (ObjectHeight * 10) - yOffset - yMargin));

        transform.position = new Vector3(viewPos.x, viewPos.y, viewPos.z);
    }
}
