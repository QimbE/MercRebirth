using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public Animator barAnimator;

    public int health;
    public int maxHealth;

    public int armor;
    public int maxArmor;

    public int energy;
    public int maxEnergy;
    public int energyRechargePerSec;

    public int critChance;
    public int critMultiplier;

    public bool isPlayer = false;

    private float currentTime = 0;

    public int Armor
    {
        get => armor;
        set
        {
            if (armor != value && value >= maxArmor && isPlayer)
            {
                armor = maxArmor;
                barAnimator.SetBool("isArmorCharged", true);
            }
            if (value > maxArmor)
            {
                armor = maxArmor;
            }
            else
            {
                armor = value;
            }
        }
    }
    public int Health
    {
        get => health;
        set
        {
            if (health != value && value >= maxHealth && isPlayer)
            {
                health = maxHealth;
                barAnimator.SetBool("isHpCharged", true);
            }
            if (value > maxHealth)
            {
                health = maxHealth;
            }
            else if (value < 0)
            {
                health = 0;
            }
            else
            {
                health = value;
            }
        }
    }
     public int Energy
    {
        get => energy;
        set
        {
            if (energy != value && value >= maxEnergy && isPlayer)
            {
                energy = maxEnergy;
                barAnimator.SetBool("isEnergyCharged", true);
            }
            else if (value > maxEnergy)
            {
                energy = maxEnergy;
            }
            else if (value < 0)
            {
                energy = 0;
            }
            else
            {
                energy = value;
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
        EnergyCharging();

        if (Health <= 0)
        {
            if (!isPlayer)
            {
                Destroy(gameObject);
            }
            else
            {
                //on player death
            }
        } 


    }
    public void TakeDamage(int damage)
    {
        Armor -= damage;
        if (Armor < 0)
        {
            Health += Armor;
            Armor = 0;
        }
    }
    public void EnergyCharging()
    {
        if (Energy < maxEnergy)
        {
            if (currentTime >= 1)
            {
                Energy += energyRechargePerSec;
                currentTime = 0;
            }
        }
        else if (isPlayer)
        {
            //Press Q to activate ur ultimate skill
        }
        else
        {
            //Enemy activates smth
        }
        currentTime += Time.deltaTime;
    }
}
