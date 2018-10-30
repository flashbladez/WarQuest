using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCamera : MonoBehaviour
{

    [SerializeField] GameObject cameraToUse = null;

    void Awake()
    {
        var cam = Instantiate(cameraToUse, transform.parent);
        cam.transform.SetParent(gameObject.transform, false);
        transform.DetachChildren();
    }


    void Update()
    { 

    }

}
