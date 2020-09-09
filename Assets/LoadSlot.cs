using System.Collections;
using UnityEngine;
using TMPro;

public class LoadSlot : MonoBehaviour
{
    private LoadSlotHandler handler;

    private DataManager dataManager;

    [SerializeField]
    private SceneLoader.Scene previousScene;
    
    [SerializeField]
    private DataManager.SaveSlot SaveSlot;
    
    private AudioSource audioSource;

    [SerializeField]
    TextMeshProUGUI slotInfoData;

    [SerializeField]
    GameObject DeleteDataButton;


    void Awake() {
        dataManager = GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManager>();

        slotInfoData.text = dataManager.LoadSlotInfoData(this.SaveSlot);

        if (!slotInfoData.text.Equals("Empty")) {
            DeleteDataButton.SetActive(true);
        }
        else
            DeleteDataButton.SetActive(false);
    }

public void PickSlot() {
        handler.setGrabbedSlot(this.SaveSlot);
        StartCoroutine(Load(0.2f));
    }

    IEnumerator Load(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        dataManager.LoadSlotData(SaveSlot);
        SceneLoader.Loader.SwapScenes(previousScene, PersistentSlotData.lastCheckpointScene, 0.2f);
        Destroy(this.gameObject);
    }

    public void setLoadSlotHander(LoadSlotHandler handler)
    {
        this.handler = handler;
    }

    public DataManager.SaveSlot getSaveSlot()
    {
        return SaveSlot;
    }

    public void DeleteSlot() {
        dataManager.DeleteSavedData(this.SaveSlot);
        slotInfoData.text = dataManager.LoadSlotInfoData(this.SaveSlot);
    }
}
