using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidAreaController : MonoBehaviour
{
    private List<GameObject> checkPointList;

    private List<GameObject> restrictedAreaList;

    private GameObject player;

    [SerializeField]
    private GameObject initialPosition;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ToInitPosition(initialPosition);
    }

    /*Finds the lateszt spawnpoint due to players position*/
    private GameObject LatestCheckPoint(List<GameObject> CheckPointList, GameObject Player)
    {
        GameObject lastCheckPoint = CheckPointList[0];
        System.DateTime LatestAvailableTimestamp = lastCheckPoint.GetComponent<CheckPoint>().AvailableTimestamp;

        foreach (GameObject cp in CheckPointList) {
            if (cp.GetComponent<CheckPoint>().AvailableTimestamp > LatestAvailableTimestamp) {
                LatestAvailableTimestamp = cp.GetComponent<CheckPoint>().AvailableTimestamp;
                lastCheckPoint = cp;
            }
        }
        return lastCheckPoint;
    }


    /* RelocateToSpawnpoint - Relocates player to the latest spawnpoint */
    public void ToCheckPoint()
    {
        GameObject UsedCheckPoint = LatestCheckPoint(checkPointList, player);

        Debug.Log("Teleport to" + UsedCheckPoint.transform.position);

        StartCoroutine(DoTeleport(player, UsedCheckPoint));
    }

    /* RelocateToInitPosition - Relocates player to the Initial position defined by the script setting */
    public void ToInitPosition(GameObject InitialPosition)
    {
        Debug.Log("Teleport to Initial Position:" + InitialPosition.transform.position);
        StartCoroutine(DoTeleport(player, InitialPosition));
    }

    /*Should be Called from the outside of this class in case of necessity to refresh RestrictedArea dataset*/
    public void ReloadRestrictedAreas(RestrictedAreas RAreas)
    {
        if (RAreas != null)
        {
            restrictedAreaList = RAreas.GetObjectList();
            foreach (GameObject RestrictedArea in restrictedAreaList)
            {
                RestrictedArea.GetComponent<RestrictedAreaBox>().AckRestrictedArea(this.gameObject);
            }

            RAreas.GetRestrictedAreaTilemap().GetComponent<RestrictedAreaTilemap>().AckRestrictedArea(this.gameObject);

        }
        else
        {
            Debug.Log("Restricted Areas script did not found in iput value. Name:" + this.name);
        }
    }

    public void ReloadCheckPoints(CheckPoints CPs)
    {
        if (CPs != null)
            checkPointList = CPs.GetObjectList();
        else
            Debug.Log("Checkpoints script did not found in iput value. Name:" + this.name);
    }

    /*Teleports Player GO with assigned OVRPlayerController to desired Position*/
    IEnumerator DoTeleport(GameObject player, GameObject checkPoint)
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        player.GetComponent<Transform>().position = checkPoint.transform.position;
        
        yield return new WaitForEndOfFrame();
      
    }


}