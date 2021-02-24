        using UnityEngine;
using UnityEngine.Events;

public class DoubleTouch : MonoBehaviour
{
    public float touchCrono = 0.4f;
    public UnityEvent onDoubleTouch;

    private float currentCronoTouch;

    // Start is called before the first frame update
    void Start()
    {
        currentCronoTouch = touchCrono;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCronoTouch >= 0)
        {
            currentCronoTouch -= Time.deltaTime;
        }
        else
        {
            currentCronoTouch = touchCrono;
        }

        if (Input.touchCount > 1 && currentCronoTouch >= 0)
        {
            onDoubleTouch.Invoke();
        }
    }
}
