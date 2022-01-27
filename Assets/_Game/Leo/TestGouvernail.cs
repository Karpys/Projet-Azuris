using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGouvernail : MonoBehaviour
{
    Vector3 touchStart;
    private Touch touch;
    public float Amplitude;
    public float RotationSpeed;
    public static TestGouvernail instance;

    void Start()
    {
        if (instance != this && instance != null)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }
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

            
            transform.Rotate(Vector3.forward * Time.deltaTime * -Amplitude / 1200* RotationSpeed);
        }
    }

    void DragStart()
    {
        touchStart = touch.position;
        Debug.Log(touchStart);

    }

    void DragRelease()
    {
        touchStart = Vector3.zero;
        Amplitude = 0;
    }

    void Dragging()
    {
        Amplitude =touch.position.x - touchStart.x;
        Debug.Log(Amplitude);
    }
}
