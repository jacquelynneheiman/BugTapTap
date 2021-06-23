using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem 
{
    static string path = Application.persistentDataPath + "/player.bin";

    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        FileStream stream = new FileStream(path, FileMode.Open);

        if (File.Exists(path) && stream.Length > 0)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }

    }

    public static void ClearSaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        GameManager.instance.player.lastLevelUnlocked = 1;

        for(int i = 0; i < 20; i++)
        {
            GameManager.instance.player.starsEarnedPerLevel[i] = 0;
        }

        PlayerData data = new PlayerData(GameManager.instance.player);

        formatter.Serialize(stream, data);

        stream.Close();
    }
}
