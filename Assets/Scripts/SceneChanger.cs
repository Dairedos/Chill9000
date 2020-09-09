using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    private bool collidingWithPlayer;

    [SerializeField]
    private string DetectedColliderName;

    [SerializeField]
    private SceneLoader.Scene PreviousScene;

    [SerializeField]
    private SceneLoader.Scene NextScene;

    [SerializeField]
    private bool makeAction;

    [SerializeField]
    private bool isLastLevelScene;

    private DataManager dataManager;

    void Awake()
    {
        dataManager = GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManager>();
        PersistentSlotData.gameFinished = false;
    }
    
        // Start is called before the first frame update
        void Start()
    {
        if (NextScene.Equals(SceneLoader.Scene.MainMenuScene)) {
            isLastLevelScene = true;

        }

            makeAction = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (makeAction)
        {
            makeAction = false;
            collidingWithPlayer = true;

            PersistentSlotData.saveTimestamp = System.DateTime.UtcNow;

            if (isLastLevelScene)
            {
                PersistentSlotData.lastCheckpointScene = PreviousScene;
                PersistentSlotData.gameFinished = true;
                PersistentGeneralData.gameFinished = true;
            }
            else
                PersistentSlotData.lastCheckpointScene = NextScene;
            

            dataManager.SaveSlotData(PersistentSlotData.slotPosition);
            dataManager.SaveGeneralData();

            SceneLoader.Loader.SwapScenes(PreviousScene, NextScene, 0.1f);
            
}
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name.Equals(DetectedColliderName) &&
            (!collidingWithPlayer))
        {
                makeAction = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.name.Equals(DetectedColliderName))
        {
            collidingWithPlayer = false;
            makeAction = false;
        }
    }
}
