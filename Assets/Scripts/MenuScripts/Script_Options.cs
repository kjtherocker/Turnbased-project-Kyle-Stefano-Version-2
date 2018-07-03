using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Script_Options : MonoBehaviour
{

    public Dropdown resolutionDropdown;
    public AudioMixer audioMixer;

    Resolution[] ListOfResolution;

    void Start()
    {
        ListOfResolution = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> List_Options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < ListOfResolution.Length; i++)
        {
            string GetOptions = ListOfResolution[i].width + "X" + ListOfResolution[i].height;
            List_Options.Add(GetOptions);

            if (ListOfResolution[i].width == Screen.currentResolution.width &&
               ListOfResolution[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(List_Options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }

    // Use this for initialization
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Component_Mixer_Volume", volume);
    }

    public void SetQuality(int qualityindex)
    {
        QualitySettings.SetQualityLevel(qualityindex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }


    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = ListOfResolution[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }
	// Update is called once per frame
	void Update ()
    {
		
	}

}
