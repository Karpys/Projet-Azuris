using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGouvernail : MonoBehaviour
{
    Vector3 touchStart;
    private Touch touch;
    private float Amplitude;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                DragStart();
            }
            if (touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }
            if (touch.phase == TouchPhase.Canceled)
            {
                DragRelease();
            }

        }
    }

    void DragStart()
    {
        touchStart = Camera.main.ScreenToWorldPoint(touch.position);
        
    }

    void DragRelease()
    {
        touchStart = Vector3.zero;
    }

    void Dragging()
    {
        Amplitude = Camera.main.ScreenToWorldPoint(touch.position).x - touchStart.x;
        Debug.Log(Amplitude);
    }
}
