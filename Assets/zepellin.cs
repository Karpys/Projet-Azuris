using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zepellin : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.forward*100*Time.deltaTime);
        transform.Rotate(new Vector3(0,TestGouvernail.instance.Amplitude/200 * Time.deltaTime,0));
    }
}
