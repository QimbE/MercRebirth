using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public string Name;
    public string Description;
    public UpProperty Property;
    public float Delta;
    public float Value;
    public bool IsValue;
    public Stats player;

    public GameObject LevelSwithcer;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
    }
    public void ReStats()
    {
        switch (this.Property)
        {
            case UpProperty.MaxHealth:
                if (IsValue)
                {
                    player.maxHealth = (int)Value;
                }
                else
                {
                    player.maxHealth+= (int)Delta;
                }
                break;
            case UpProperty.MaxArmor:
                if (IsValue)
                {
                    player.maxArmor = (int)Value;
                }
                else
                {
                    player.maxArmor += (int)Delta;
                }
                break;
            case UpProperty.CritChance:
                if (IsValue)
                {
                    player.critChance = (int)Value;
                }
                else
                {
                    player.critChance += (int)Delta;
                }
                break;
            case UpProperty.CritMultiplier:
                if (IsValue)
                {
                    player.critMultiplier = (int)Value;
                }
                else
                {
                    player.critMultiplier += (int)Delta;
                }
                break;
        }
        Instantiate(LevelSwithcer);
    }
    public enum UpProperty
    {
        Health,
        MaxHealth,
        Armor,
        MaxArmor,
        CritChance,
        CritMultiplier,

    }
}
