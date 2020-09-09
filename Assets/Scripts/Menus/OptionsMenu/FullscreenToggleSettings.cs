using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggleSettings : MonoBehaviour {

    
    // Use this for initialization
    void Start()
    {

        if (Screen.fullScreen)
        {
            GameObject.FindGameObjectWithTag("FullscreenToggle")
                .GetComponent<Toggle>().isOn = true;
        }
        else
        {
            GameObject.FindGameObjectWithTag("FullscreenToggle")
              .GetComponent<Toggle>().isOn = false;
        }

    }
}
