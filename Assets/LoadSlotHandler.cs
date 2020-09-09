using System.Collections.Generic;
using UnityEngine;

public class LoadSlotHandler : ListLoader
{
    [SerializeField]
    private DataManager.SaveSlot SaveSlotGrabbed;

    private bool unusedPadsDisabled;

    // Start is called before the first frame update
    void Start()
    {
        unusedPadsDisabled = false;
        SaveSlotGrabbed = DataManager.SaveSlot.None;
        ReloadLoadPads();
    }

    // Update is called once per frame
    void Update()
    {
        if ((!unusedPadsDisabled) && (!SaveSlotGrabbed.Equals(DataManager.SaveSlot.None)))
        {

            foreach (GameObject gameObject in ObjectList)
            {
                if (!gameObject.GetComponent<LoadSlot>().getSaveSlot().Equals(SaveSlotGrabbed))
                    gameObject.SetActive(false); 
            }

            unusedPadsDisabled = true;
        }
    }

    public void ReloadLoadPads()
    {
        LoadSlot[] RABArray = this.GetComponentsInChildren<LoadSlot>();

        ReloadList(ref ObjectList, RABArray);

        List<LoadSlot> loadSlotList = new List<LoadSlot>();

        foreach (GameObject gameObject in ObjectList)
        {
            loadSlotList.Add(gameObject.GetComponent<LoadSlot>());
        }

        AckLoadPads(loadSlotList);
    }

    private void AckLoadPads(List<LoadSlot> loadPadList)
    {
        foreach (LoadSlot loadSlot in loadPadList)
        {
            loadSlot.setLoadSlotHander(this.gameObject.GetComponent<LoadSlotHandler>());
        }
    }

    public void setGrabbedSlot(DataManager.SaveSlot SaveSlot)
    {

        if (SaveSlotGrabbed.Equals(DataManager.SaveSlot.None))
            SaveSlotGrabbed = SaveSlot;

    }
}
