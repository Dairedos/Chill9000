using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDispenser : MonoBehaviour
{
    [SerializeField]
    private GameObject startPoint;

    private GameObject projectile;

    private float newPositionShift;

    [SerializeField]
    private float shootingDistance;

    [SerializeField]
    private float ProjectileSpeedMultiplier;
    
    // Start is called before the first frame update
    void Start()
    {

        projectile = this.GetComponentInChildren<Animator>().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            if (projectile.transform.localPosition.x < shootingDistance)
            {
                newPositionShift += Time.deltaTime * ProjectileSpeedMultiplier;
            }
            else
            {
                newPositionShift = startPoint.transform.localPosition.x;
            }
            projectile.transform.localPosition = new Vector3(
                                                                newPositionShift,
                                                                projectile.transform.localPosition.y,
                                                                projectile.transform.localPosition.z);
       
    }
}
        
