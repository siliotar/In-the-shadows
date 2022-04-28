using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider VolumeSlider;

    public void SetVolume(float value)
    {
        Player.MusicVolume = value;
        SaveSystem.SavePlayer();
    }

    public void ResetProgress()
    {
        SaveSlot.ResetStats();
        SaveSystem.SavePlayer();
        VolumeSlider.value = Player.MusicVolume;
    }
}
