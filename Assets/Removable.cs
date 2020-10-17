using System.Collections.Generic;
using UnityEngine;

public class Removable : MonoBehaviour
{
    [SerializeField]
    private string DetectedColliderName;

    [SerializeField]
    private bool removableByTimer;
    [SerializeField]
    private float removableByTimerMax;
    

    [SerializeField]
    private List<GameObject> removables = new List<GameObject>();


   

    private float removableByTimerMin = 0f;
    private float removableByTimerCurrent = 0f;

    private bool removingObstacles;

    void Start() {
        if ((removableByTimerMax < 1f) && removableByTimer)
        {
            removableByTimerMax = 1f;
        }
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
                else {
                    removableByTimerCurrent = removableByTimerMin;
                    removingObstacles = false;

                    SetActiveToRemovables(true);
                }
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Button Collided with : " + col.name);
        if (col.tag.Equals(DetectedColliderName))
        {

            removableByTimerCurrent = 0f;
            removingObstacles = true;
            SetActiveToRemovables(false);
            
        }
    }

    private void SetActiveToRemovables(bool active) {
        foreach (GameObject removable in removables)
        {
            Debug.Log("SetActiveObstacle: " + removable.name);
            removable.SetActive(active);
        }
    }
}
