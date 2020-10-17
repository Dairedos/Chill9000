using System.Collections.Generic;
using UnityEngine;

public class MovingTrapRemote : MonoBehaviour
{
    [SerializeField]
    private string DetectedColliderName;

    [SerializeField]
    private bool removableByTimer;
    [SerializeField]
    private float removableByTimerMax;

    private const string ANIMATION_ACTION_VAR = "DoAction";
    private const string NOTIFICATION_CLOCK_VAR = "ActivateClock";

    [SerializeField]
    private List<GameObject> removables = new List<GameObject>();

    private float removableByTimerMin = 0f;
    private float removableByTimerCurrent = 0f;

    private bool removingObstacles;

    private Animator animator;

    [SerializeField]
    private GameObject sideNotification;

    private Animator sideNotificationAnimator;

    void Start()
    {
        foreach (GameObject removable in removables)
        {
            removable.GetComponent<TrapMoving>().stayActive = true;
            removable.GetComponent<TrapMoving>().stayNonActive = false;
        }

            
        if ((removableByTimerMax < 1f) && removableByTimer)
        {
            removableByTimerMax = 1f;
        }

        animator = this.GetComponent<Animator>();
        sideNotificationAnimator = sideNotification.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (removingObstacles && removableByTimer)
        {
            if (removableByTimerCurrent < removableByTimerMax)
            {
                removableByTimerCurrent += Time.deltaTime;
            }
            else
            {
                removableByTimerCurrent = removableByTimerMin;
                removingObstacles = false;

                SetActiveToRemovables(true);
                animator.SetBool(ANIMATION_ACTION_VAR, false);

                sideNotificationAnimator.SetBool(NOTIFICATION_CLOCK_VAR, false);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Button Collided with : " + col.name);
        if (col.tag.Equals(DetectedColliderName))
        {
            removingObstacles = false;
            SetActiveToRemovables(false);

            animator.SetBool(ANIMATION_ACTION_VAR, true);
            sideNotificationAnimator.SetBool(NOTIFICATION_CLOCK_VAR, false);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Button Collided with : " + col.name);
        if (col.tag.Equals(DetectedColliderName))
        {
            removableByTimerCurrent = 0f;
            removingObstacles = true;
            SetActiveToRemovables(false);

            sideNotificationAnimator.SetBool(NOTIFICATION_CLOCK_VAR, true);
        }
    }


    private void SetActiveToRemovables(bool active)
    {
        foreach (GameObject removable in removables)
        {
            Debug.Log("SetActiveObstacle: " + removable.name);

            removable.GetComponent<TrapMoving>().stayActive = active;
            removable.GetComponent<TrapMoving>().stayNonActive = !active;
        }
    }
}
