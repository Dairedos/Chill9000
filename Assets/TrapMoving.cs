using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMoving : MonoBehaviour
{

    private Animator trapAnimator;

    [SerializeField]
    private bool fastNonActive;

    [SerializeField]
    public bool stayActive { set; get; }

    [SerializeField]
    public bool stayNonActive { set; get; }

    [SerializeField]
    private float activeTime;

    private float activeTimer;

    [SerializeField]
    private float nonActiveTime;

    private float nonActiveTimer;

    [SerializeField]
    [Range(0.0f, 2.0f)]
    private float delayTime;

    private float delayTimer;
    private bool delayDone;
    
    private bool goingActive;
    private bool goingNonActive;

    [SerializeField]
    private float colliderOffsetY;

    [SerializeField]
    private float colliderOffsetX;
    
    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float activeSpeedMultiplier;

    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float nonActiveSpeedMultiplier;

    private PolygonCollider2D collider;

    private Vector2 newOffset;
    
    // Start is called before the first frame update
    void Start()
    {


        trapAnimator = this.GetComponent<Animator>();
        goingNonActive = true;

        collider = this.GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (stayActive && stayNonActive)
            stayNonActive = false;

        if (delayDone)
        {
            trapAnimator.SetBool("GoingActive", goingActive);
            trapAnimator.SetBool("GoingNonActive", goingNonActive);
            if (goingActive)
            {

                if (activeTimer < activeTime)
                {
                    activeTimer += Time.deltaTime;


                    float offset_y = colliderOffsetY - (colliderOffsetY * (activeTimer / activeTime) * activeSpeedMultiplier);
                    float offset_x = colliderOffsetX - (colliderOffsetX * (activeTimer / activeTime) * activeSpeedMultiplier);

                    if ((offset_y < 0) || (offset_x < 0))
                        newOffset = new Vector2(offset_x, offset_y);
                    else
                        newOffset = new Vector2(0, 0);

                    collider.offset = newOffset;
                }
                else if (!stayActive)
                {
                    activeTimer = 0;

                    goingActive = false;
                    goingNonActive = true;
                }
            }

            else if (goingNonActive)
            {
                if (nonActiveTimer < nonActiveTime)
                {
                    nonActiveTimer += Time.deltaTime;

                    float offset_y;
                    float offset_x;

                    if (fastNonActive)
                    {
                        offset_y = colliderOffsetY;
                        offset_x = colliderOffsetX;
                    }
                    else
                    {
                        offset_y = colliderOffsetY * (nonActiveTimer / nonActiveTime) * nonActiveSpeedMultiplier;
                        offset_x = colliderOffsetX * (nonActiveTimer / nonActiveTime) * nonActiveSpeedMultiplier;
                    }

                    if ((offset_y > colliderOffsetY) || (offset_x > colliderOffsetX))
                        newOffset = new Vector2(offset_x, offset_y);
                    else
                        newOffset = new Vector2(colliderOffsetX, colliderOffsetY);

                    collider.offset = newOffset;
                }

                else if (!stayNonActive)
                {
                    nonActiveTimer = 0;

                    goingActive = true;
                    goingNonActive = false;
                }
            }
        }
        else {
            if (delayTimer < delayTime)
                delayTimer += Time.deltaTime;
            else
                delayDone = true;
        }
    }
}
