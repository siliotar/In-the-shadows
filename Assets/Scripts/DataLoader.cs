using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataLoader : MonoBehaviour
{
    public GameObject[] Slots;
    public Slider VolumeSlider;

    void Start()
    {
        for (int i = 0; i < Slots.Length; ++i)
        {
            PlayerData data = SaveSystem.LoadPlayer(i);
            TMP_Text text = Slots[i].GetComponentInChildren<TMP_Text>();
            if (data == null)
                text.text = "Empty slot";
            else
            {
                int solved = 0;
                foreach (LevelInfo level in data.Levels)
                    if (level == LevelInfo.Solved)
                        ++solved;
                if (data.mode == GameMode.None)
                    text.text = "Not Selected\n\n" + solved + "/" + data.Levels.Length;
                else
                    text.text = data.mode.ToString() + "\n\n" + solved + "/" + data.Levels.Length;
            }
        }
        string json = System.IO.File.ReadAllText("Assets/Levels.JSON");
        LevelsData levels = JsonUtility.FromJson<LevelsData>(json);
        foreach (LevelData data in levels.levels)
        {
            data.additiontalCorrectRotations.Add(Vector3.zero);
            foreach (Object obj in data.objects)
                if (obj.mass == 0.0f)
                    obj.mass = 1.0f;
        }
        Levels.data = new List<LevelData>();
        Levels.data.AddRange(levels.levels);
    }
}
