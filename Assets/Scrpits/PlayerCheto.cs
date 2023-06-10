using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheto : MonoBehaviour
{
    public int maxHealth = 100;
    public int health = 100;
    public int maxArmor = 10;
    public int armor = 10;
    public int money = 0;
    public int primaryWeaponDamage = 1;
    public int secondaryWeaponDamage = 1;
    public int Health
    {
        get => health;
        set
        {
            if (value < 0)
            {
                health = 0;
            }
            else if (value>maxHealth)
            {
                health = maxHealth;
            }
            else
            {
                health = value;
            }
        }
    }
    public int Armor
    {
        get => armor;
        set
        {
            if (value < 0)
            {
                armor = 0;
            }
            else if (value > maxArmor)
            {
                armor = maxArmor;
            }
            else
            {
                armor = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
