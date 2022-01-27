using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Zepellin;
    private float InitialHeight;

    void Start()
    {
        InitialHeight = transform.position.y;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,Zepellin.transform.position,1*Time.deltaTime);
        transform.position = new Vector3(transform.position.x, InitialHeight, transform.position.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, Zepellin.transform.rotation, 10f * Time.deltaTime);
    }
}
