using UnityEngine;
using System;

public class Options : MonoBehaviour
{
    public void SetVolume(float value)
    {
        PlayerData.MusicVolume = value;
    }

    public void ResetProgress()
    {
        Array.Fill(PlayerData.Levels, LevelInfo.Inactive);
        PlayerData.Levels[0] = LevelInfo.Active;
    }
}
