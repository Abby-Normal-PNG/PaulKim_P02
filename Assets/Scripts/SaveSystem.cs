﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveRecords(PlayerRecords records)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.rec";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(records);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadRecords()
    {
        string path = Application.persistentDataPath + "/player.rec";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }
}
