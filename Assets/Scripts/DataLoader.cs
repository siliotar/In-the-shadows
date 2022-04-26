using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DataLoader : MonoBehaviour
{
    public Slider VolumeSlider;

    void Start()
    {
        PlayerData.Levels = new LevelInfo[5];
        PlayerData.Levels[0] = LevelInfo.Active;
        PlayerData.Levels[3] = LevelInfo.Solved;
        PlayerData.mode = GameMode.None;
        VolumeSlider.value = PlayerData.MusicVolume;
        string json = System.IO.File.ReadAllText("Assets/Levels.JSON");
        LevelsData levels = JsonUtility.FromJson<LevelsData>(json);
        Levels.data = new List<LevelData>();
        Levels.data.AddRange(levels.levels);
    }
}
