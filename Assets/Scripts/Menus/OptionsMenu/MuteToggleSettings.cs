using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MuteToggleSettings : MonoBehaviour {

    public AudioMixer audioMixer;
	// Use this for initialization
	void Start () {
        float volume;
        audioMixer.GetFloat("volume", out volume);
        if (volume.Equals(-80f)) {
            GameObject.FindGameObjectWithTag("MuteToggle")
                .GetComponent<Toggle>().isOn = true;
        }
        else {
            GameObject.FindGameObjectWithTag("MuteToggle")
              .GetComponent<Toggle>().isOn = false;
        }

    }
	
	
}
