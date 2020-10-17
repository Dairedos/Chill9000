using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRotating : MonoBehaviour
{

    [SerializeField]
    private float radialSpeedCoeficient;
    
    Vector3 rotation;
    
    private GameObject projectile;
   
    void FixedUpdate()
    {

        rotation = new Vector3(0,0,(radialSpeedCoeficient / Time.deltaTime));
        this.gameObject.transform.Rotate(rotation, Space.Self);
    }
}
