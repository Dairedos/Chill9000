using UnityEngine;

public class RestrictedAreas : ListLoader
{
    private ValidAreaController ValidAreaCheck;

    // Start is called before the first frame update
    void Start()
    {
        ValidAreaCheck = this.GetComponentInParent<ValidAreaController>();
        ReloadRAB();
    }

    public void ReloadRAB()
    {
        RestrictedAreaBox[] RABArray = this.GetComponentsInChildren<RestrictedAreaBox>();

        ReloadList(ref ObjectList, RABArray);
        ValidAreaCheck.ReloadRestrictedAreas(this.GetComponent<RestrictedAreas>());
    }
}
