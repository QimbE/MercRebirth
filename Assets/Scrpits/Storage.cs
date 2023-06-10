using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Device;

public class Storage : MonoBehaviour
{
    public static GameObject player;
    public static void Load()
    {
        if (File.Exists(UnityEngine.Device.Application.persistentDataPath + "/MercRebirth.save"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(UnityEngine.Device.Application.persistentDataPath + "/MercRebirth.save", FileMode.Open);
            try
            {
                GameData data = (GameData)formatter.Deserialize(file);
                file.Close();
                player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<Stats>().CopyStats(data);
                Debug.Log("GameData loaded!");
            }
            catch
            {
                //Вывести на экран, что сохранения повреждены!
                file.Close();
                Debug.LogError("Вывести на экран, что сохранения повреждены!");
            }
        }
        else
        {
            Debug.LogError("Сохранения отсутствуют!");
        }
    }
    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(UnityEngine.Device.Application.persistentDataPath + "/MercRebirth.save");
        Debug.Log(UnityEngine.Device.Application.persistentDataPath);
        player = GameObject.FindGameObjectWithTag("Player");
        GameData data = new GameData(player.GetComponent<Stats>());
        formatter.Serialize(file, data);
        file.Close();
        Debug.Log("Игровой процесс сохранён!");
    }
    public static void Save(GameObject toSave)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(UnityEngine.Device.Application.persistentDataPath + "/MercRebirth.save");
        Debug.Log(UnityEngine.Device.Application.persistentDataPath);
        GameData data = new GameData(toSave.GetComponent<Stats>());
        formatter.Serialize(file, data);
        file.Close();
        Debug.Log("Игровой процесс сохранён!");
    }
    public static void DeleteSave()
    {
        FileStream file = File.Open(UnityEngine.Device.Application.persistentDataPath + "/MercRebirth.save", FileMode.Create);
        file.Close();
    }
}
[Serializable]
public class GameData
{
    public int health;
    public int maxHealth;

    public int armor;
    public int maxArmor;

    public int energy;
    public int maxEnergy;
    public int energyRechargePerSec;
    public int armorIncrease;
    public float timeBetweenArmorRegen;
    public float rechargeFreq;

    public float timeBetweenDamage;

    public int critChance;
    public int critMultiplier;

    public GameData(Stats newStats)
    {
        this.health = newStats.health;
        this.maxHealth = newStats.maxHealth;
        this.armor = newStats.armor;
        this.maxArmor = newStats.maxArmor;
        this.energy = newStats.energy;
        this.maxEnergy = newStats.maxEnergy;
        this.energyRechargePerSec = newStats.energyRechargePerSec;
        this.armorIncrease = newStats.armorIncrease;
        this.timeBetweenArmorRegen = newStats.timeBetweenArmorRegen;
        this.rechargeFreq = newStats.rechargeFreq;
        this.timeBetweenDamage = newStats.timeBetweenDamage;
        this.critChance = newStats.critChance;
        this.critMultiplier = newStats.critMultiplier;
    }
}

//public class Storage: MonoBehaviour
//{
//    private BinaryFormatter formatter;
//    private GameObject player;

//    public void Load() 
//    {
//        if (File.Exists(UnityEngine.Device.Application.persistentDataPath + "/saves/MercRebirth.save"))
//        {
//            formatter = new BinaryFormatter();
//            FileStream file = File.Open(UnityEngine.Device.Application.persistentDataPath + "/saves/MercRebirth.save", FileMode.Open);
//            GameData data = (GameData)formatter.Deserialize(file);
//            file.Close();
//            player.GetComponent<Stats>().CopyStats(data.stats);
//            Debug.Log("Игровой процесс загружен!");
//        }
//        else 
//        {
//            Debug.LogError("Сохранения отсутствуют!");
//        }
//    }

//    public void Save(object SaveData) 
//    {
//        if (!Directory.Exists(UnityEngine.Device.Application.persistentDataPath + "/saves"))
//        {
//            Directory.CreateDirectory(UnityEngine.Device.Application.persistentDataPath + "/saves");
//        }
//        FileStream file = File.Create(UnityEngine.Device.Application.persistentDataPath + "/saves/MercRebirth.save"); 
//        formatter = new BinaryFormatter();
//        GameData data = new GameData();
//        data.stats = player.GetComponent<Stats>();
//        formatter.Serialize(file, data);
//        file.Close();
//    }
//}

//public class GameData 
//{
//    public Stats stats;
//}