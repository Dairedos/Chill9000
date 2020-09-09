using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour {

    public static SettingsMenu instance;

    public TMP_Dropdown resolutionDropdown;
   // public AudioMixer audioMixer;
    public Slider volumeSlider;
    private float actualVolume = 0;


    Resolution[] resolutions;
   
    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
         
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height) {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }
    public void SetVolume(float volume) {
        actualVolume = volume;
       // audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void setFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    public void setResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void muteMasterGroupMusic(bool isMuted)
    {
        if (isMuted){

           // audioMixer.SetFloat("volume", -80f);
            volumeSlider.interactable = false;
        }
        else {
           // audioMixer.SetFloat("volume", actualVolume);
            volumeSlider.interactable = true;
        }
    }


}
