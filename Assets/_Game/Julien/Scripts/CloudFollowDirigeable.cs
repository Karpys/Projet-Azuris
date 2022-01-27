using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudFollowDirigeable : MonoBehaviour
{

    [SerializeField] private Transform dirigeable;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(dirigeable.position.x, transform.position.y, dirigeable.position.z);
    }
}
