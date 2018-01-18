using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource audio;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
 
    /// <summary>
    /// Sets initial resolution settings if available (for-PC)
    /// </summary>
    void Start()
    {
       if (!audio.isPlaying)
        { 
            audio.Play();
        }

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)

            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    /// <summary>
    /// Sets Resolution (if available)
    /// </summary>
    /// <param name="resolutionIndex"></param>
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    /// <summary>
    /// Sets Volume
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        audio.volume = volume/80 + (float)0.99;
    }
    /// <summary>
    /// Sets Graphic Quality (higher qualities produce better gamefield shadows)
    /// </summary>
    /// <param name="qualityIndex"></param>
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex-1);
    }
    /// <summary>
    /// Sets fullScreen (works perfectly for .exe build but has click delay for webGL because actions such as fullScreen don't kick in until a user click event occurs)
    /// </summary>
    /// <param name="isFullscreen"></param>
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}
