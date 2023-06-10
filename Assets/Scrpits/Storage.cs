using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Device;

//public class Storage: MonoBehaviour
//{
//    private BinaryFormatter formatter;
//    public GameObject player;
//    public void Load()
//    {
//        if (File.Exists(UnityEngine.Device.Application.persistentDataPath + "/saves/MercRebirth.save"))
//        {
//            formatter = new BinaryFormatter();
//            FileStream file = File.Open(UnityEngine.Device.Application.persistentDataPath + "/saves/MercRebirth.save", FileMode.Open);
//            GameData data = (GameData)formatter.Deserialize(file);
//            file.Close();
//            player.GetComponent<Stats>().CopyStats(data.stats);
//            Debug.Log("GameData loaded!");
//        }
//        else
//        {
//            Debug.LogError("Сохранения отсутствуют!");
//        }
//    }
//    public void Save()
//    {
//        formatter = new BinaryFormatter();
//        FileStream file = File.Create(UnityEngine.Device.Application.persistentDataPath + "/saves/MercRebirth.save");
//        GameData data = new GameData();
//        data.stats = player.GetComponent<Stats>();
//        formatter.Serialize(file, data);
//        file.Close();
//        Debug.Log("Игровой процесс сохранён!");
//    }
//}
//[Serializable]
//public class GameData
//{
//    public Stats stats;
//}

public class Storage: MonoBehaviour
{
    private BinaryFormatter formatter;
    private GameObject player;

    public void Load() 
    {
        if (File.Exists(UnityEngine.Device.Application.persistentDataPath + "/saves/MercRebirth.save"))
        {
            formatter = new BinaryFormatter();
            FileStream file = File.Open(UnityEngine.Device.Application.persistentDataPath + "/saves/MercRebirth.save", FileMode.Open);
            GameData data = (GameData)formatter.Deserialize(file);
            file.Close();
            player.GetComponent<Stats>().CopyStats(data.stats);
            Debug.Log("Игровой процесс загружен!");
        }
        else 
        {
            Debug.LogError("Сохранения отсутствуют!");
        }
    }

    public void Save(object SaveData) 
    {
        if (!Directory.Exists(UnityEngine.Device.Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(UnityEngine.Device.Application.persistentDataPath + "/saves");
        }
        FileStream file = File.Create(UnityEngine.Device.Application.persistentDataPath + "/saves/MercRebirth.save"); 
        formatter = new BinaryFormatter();
        GameData data = new GameData();
        data.stats = player.GetComponent<Stats>();
        formatter.Serialize(file, data);
        file.Close();
    }
}

public class GameData 
{
    public Stats stats;
}