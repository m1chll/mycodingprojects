using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public GameObject target;
    public Vector3 offset;
    void Update()
    {
       // Camera pos = Target Pos
       transform.position = target.transform.position + offset; 
    }
}
