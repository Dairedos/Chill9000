using UnityEngine;

public class RestrictedAreas : ListLoader
{
    private ValidAreaController ValidAreaCheck;

    private GameObject RestrictedAreaTilemapObject;

    // Start is called before the first frame update
    void Start()
    {
        ValidAreaCheck = this.GetComponentInParent<ValidAreaController>();
        ReloadRAB();
    }

    //Reload Restricted Areas (boxes and tilemap)
    public void ReloadRAB()
    {
        RestrictedAreaBox[] RABArray = this.GetComponentsInChildren<RestrictedAreaBox>();
        
        ReloadList(ref ObjectList, RABArray);
        
        ValidAreaCheck.ReloadRestrictedAreas(this.GetComponent<RestrictedAreas>());
    }

    public GameObject GetRestrictedAreaTilemap() {

        return this.GetComponentInChildren<RestrictedAreaTilemap>().gameObject;
      
    } 
}
