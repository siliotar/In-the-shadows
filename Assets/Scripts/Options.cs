using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider VolumeSlider;
    public AudioMixer audioMixer;

    private string _volumeParameter = "volume";

    public void SetVolume(float value)
    {
        audioMixer.SetFloat(_volumeParameter, Mathf.Log10(value) * 30.0f);
    }

    public void ResetProgress()
    {
        SaveSlot.ResetStats();
        SaveSystem.SavePlayer();
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParameter, VolumeSlider.value);
    }

    private void Start()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat(_volumeParameter, 0.5f);
    }
}
