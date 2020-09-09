using UnityEngine;
using TMPro;
public class MainMenu : MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI SmallCollectables;
    [SerializeField]
    TextMeshProUGUI AllSmallCollectables;
    [SerializeField]
    TextMeshProUGUI AllBigCollectables;
    [SerializeField]
    TextMeshProUGUI GameFinished;

    void Awake() {
        SmallCollectables.text = ("Small Collectables Available: "+ PersistentGeneralData.smallCollectables.ToString());
        AllSmallCollectables.text = ("All Small Collectables Collected: " + PersistentGeneralData.collectedAllSmallCollectables.ToString());
        AllBigCollectables.text = ("All Big Collectables Collected: " + PersistentGeneralData.collectedAllBigCollectables.ToString());
        GameFinished.text = ("Game Finished: " + PersistentGeneralData.gameFinished.ToString());
    }

    public void QuitGame()
    {
        Debug.Log("Exiting Game!");
        Application.Quit();
        
    }

    


}
