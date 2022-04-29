using UnityEngine;

public class SaveSlot : MonoBehaviour
{
    public int SlotID;

    public static void ResetStats()
    {
        Player.Levels = new LevelInfo[5];
        Player.Levels[0] = LevelInfo.Active;
        Player.mode = GameMode.None;
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
        }
        Player.SaveSlot = SlotID;
    }
}
