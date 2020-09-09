using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSliderSettings : MonoBehaviour {

    public AudioMixer audioMixer;
	// Use this for initialization
	void Start () {
        float volume;
        audioMixer.GetFloat("volume", out volume);
        GameObject.FindGameObjectWithTag("VolumeSlider")
                .GetComponent<Slider>().value = volume;
    }
	
}
