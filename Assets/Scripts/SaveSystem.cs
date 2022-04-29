using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    public static void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/slot" + Player.SaveSlot + ".data";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData();
        data.Levels = Player.Levels;
        data.mode = Player.mode;
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(int slot)
    {
        string path = Application.persistentDataPath + "/slot" + slot + ".data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
            return null;
    }
}
