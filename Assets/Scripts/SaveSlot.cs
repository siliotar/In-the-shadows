using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    public int SlotID;
    public Slider VolumeSlider;

    public static void ResetStats()
    {
        Player.Levels = new LevelInfo[5];
        Player.Levels[0] = LevelInfo.Active;
        Player.mode = GameMode.None;
        Player.MusicVolume = 0.5f;
    }

    public void LoadData()
    {
        PlayerData data = SaveSystem.LoadPlayer(SlotID);
        if (data == null)
            ResetStats();
        else
        {
            Player.Levels = data.Levels;
            Player.mode = data.mode;
            Player.MusicVolume = data.MusicVolume;
        }
        Player.SaveSlot = SlotID;
        VolumeSlider.value = Player.MusicVolume;
    }
}
