using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class settingsMenu : MonoBehaviour
{
    Resolution[] resolutions;
    public TMP_Dropdown resDropdown;
    public AudioMixer audioMixer;
    private int[] _fpsArray = new int [6];

    private void Start()
    {
        resolutions = Screen.resolutions;

        resDropdown.ClearOptions();

        List<string> resList = new List<string>();

        int currentResIndex = 0;

        _fpsArray[0] = -1;
        _fpsArray[1] = 30;
        _fpsArray[2] = 60;
        _fpsArray[3] = 90;
        _fpsArray[4] = 120;
        _fpsArray[5] = 144;

        for(int i = 0; i < resolutions.Length; i++) 
        { 
            string option = resolutions[i].width + " x " + +resolutions[i].height;
            resList.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        resDropdown.AddOptions(resList);
        resDropdown.value = currentResIndex;
        resDropdown.RefreshShownValue();
    }

    public void SetVolume(float masterVolume)
    {
        Debug.Log("MasterVolume set to " + masterVolume);
        audioMixer.SetFloat("MasterVolume", masterVolume);
    }

    public void SetQuality(int qualityIndex)
    {
        Debug.Log("qualityIndex set to " +qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFS(bool isFS)
    {
        Debug.Log("isFS set to " + isFS);
        Screen.fullScreen = isFS;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Debug.Log("Resolution set to " + resolution.height + "p");
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    public void SetVSync(int vSync)
    {
        Debug.Log("vSync set to " + vSync);
        QualitySettings.vSyncCount = vSync;
    }

    public void SetFPS(int FPS_Entered)
    {
        int FPS = _fpsArray[FPS_Entered];
        Debug.Log("FPS set to " + FPS);
        Application.targetFrameRate = FPS;
    }
}
