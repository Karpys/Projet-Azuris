using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VilleSpawner : MonoBehaviour
{

    [SerializeField] private List<GameObject> ville;
    [SerializeField] private Transform cloud;

    [SerializeField] private float maxX;
    [SerializeField] private float maxZ;
    [SerializeField] private int amount;

    
    void Start()
    {

        for (int i = 0; i < amount; i++)
        {
            GameObject villeI = Instantiate(ville[Random.Range(0, ville.Count)]);
            villeI.transform.position = new Vector3(Random.Range(-maxX, maxX),
                Random.Range(cloud.position.y - 50, cloud.position.y + 200), Random.Range(-maxZ, maxZ));
            villeI.transform.Rotate(Vector3.up, Random.Range(0, 360));
        }
        
    }

    
}
