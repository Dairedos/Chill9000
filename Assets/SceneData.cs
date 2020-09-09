using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> bigCollectableList = new List<GameObject>();

    public int smallColCollected { get; set; }
    public int bigColCollected { get; set; }

    private Dictionary<PersistentSlotData.BigCollectable, bool> bigCollectableStatus = new Dictionary<PersistentSlotData.BigCollectable, bool>();

    public void setBigCollectableStatus(Dictionary<PersistentSlotData.BigCollectable, bool> bigCollectableStatus)
    {
        this.bigCollectableStatus = bigCollectableStatus;
    }

    public Dictionary<PersistentSlotData.BigCollectable, bool> getBigCollectableStatus()
    {
        return bigCollectableStatus;
    }

    public List<GameObject> getBigCollectableList()
    {
        return bigCollectableList;
    }
}

