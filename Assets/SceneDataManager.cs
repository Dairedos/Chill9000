using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SceneDataManager : MonoBehaviour
{

    private SceneData sceneData;

    private List<Collectable> bigCollectableList = new List<Collectable>();

    // Start is called before the first frame update
    void Start()
    {

        sceneData = this.GetComponent<SceneData>();
        initCollectedTotal();

        Dictionary<PersistentSlotData.BigCollectable, bool> bigCollectablestatus = sceneData.getBigCollectableStatus();

        /*Deprecated - setting already picked flag in case of already picked*/
        /*
        foreach (GameObject gameObject in sceneData.getBigCollectableList()) {

            PersistentData.BigCollectable nameToBigCol = (PersistentData.BigCollectable)Enum.Parse(typeof(PersistentData.BigCollectable), gameObject.name);

            if (bigCollectablestatus.ContainsKey(nameToBigCol))
            {
                bool status = bigCollectablestatus[nameToBigCol];
                gameObject.GetComponent<Collectable>().setAlreadyPicked(true);
            }

            bigCollectableList.Add(gameObject.GetComponent<Collectable>());
        }
        */

    }


    private void initCollectedTotal()
    {
        sceneData.setBigCollectableStatus(PersistentSlotData.bigCollectableStatus[PersistentSlotData.currentScene]);

    }

    private void setBigColTaken(SceneData sceneData)
    {
        List<GameObject> bigCollectable = sceneData.getBigCollectableList();

    }


}
