using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public Slider VolumeSlider;
    public AudioMixer audioMixer;
    public Toggle FullscreenToggle;
    public TMP_Dropdown ResolutionDropdown;

    private string _volumeParameter = "volume";
    private string _fullscreenParameter = "isFullscreen";

    private Resolution[] _resolutions;

    public void SetVolume(float value)
    {
        audioMixer.SetFloat(_volumeParameter, Mathf.Log10(value) * 30.0f);
    }

    public void ResetProgress()
    {
        SaveSlot.ResetStats();
        SaveSystem.SavePlayer();
    }

    public void SetFullscreen(bool value)
    {
        Screen.fullScreen = value;
    }

    public void SetResolution(int index)
    {
        Screen.SetResolution(_resolutions[index].width, _resolutions[index].height, Screen.fullScreen);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParameter, VolumeSlider.value);
        PlayerPrefs.SetInt(_fullscreenParameter, Convert.ToInt32(FullscreenToggle.isOn));
    }

    private void Start()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat(_volumeParameter, 0.5f);
        FullscreenToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt(_fullscreenParameter, 1));
        _resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; ++i)
        {
            options.Add(_resolutions[i].width.ToString() + " X " + _resolutions[i].height.ToString());

            if (_resolutions[i].width == Screen.width && _resolutions[i].height == Screen.height)
                currentResolutionIndex = i;
        }

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = currentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();
    }
}
