using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : ListLoader
{
    private ValidAreaController ValidAreaCheck;

    // Start is called before the first frame update
    void Start()
    {
        ValidAreaCheck = this.GetComponentInParent<ValidAreaController>();
        ReloadCP();
    }

    void Update()
    {

    }


    public void ReloadCP()
    {
        CheckPoint[] CPArray = this.GetComponentsInChildren<CheckPoint>();

        ReloadList(ref ObjectList, CPArray);
        ValidAreaCheck.ReloadCheckPoints(this.GetComponent<CheckPoints>());
    }
}