using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace BrawnSwan
{
    public class settingsMenu : MonoBehaviour
    {
        Resolution[] resolutions;
        public TMP_Dropdown resDropdown;
        public AudioMixer audioMixer;

        private void Start()
        {
            resolutions = Screen.resolutions;

            resDropdown.ClearOptions();

            List<string> resList = new List<string>();

            int currentResIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
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
            //Debug.Log(masterVolume);
            audioMixer.SetFloat("MasterVolume", masterVolume);
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void SetFS(bool isFS)
        {
            Screen.fullScreen = isFS;
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
}