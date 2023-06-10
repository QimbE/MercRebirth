using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InstantUseItem;

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
    public int armorIncrease;
    public float timeBetweenArmorRegen = 15f;
    public float rechargeFreq = 1;

    public float timeBetweenDamage = 0.5f;

    public int critChance;
    public int critMultiplier;

    public bool isPlayer = false;

    private float currentTimeBetweenRecharge = 0;
    private float currentTimeBetweenDamage = 0;
    private float currentTimeBetweenArmorRegen = 0;
    
    public int Armor
    {
        get => armor;
        set
        {
            if (armor != maxArmor && value >= maxArmor && isPlayer)
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
    public void CopyStats(Stats newStats)
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnergyCharging();
        ArmorRegen();
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
        currentTimeBetweenArmorRegen += Time.deltaTime;
        currentTimeBetweenDamage += Time.deltaTime;
    }
    public void TakeDamage(int damage)
    {
        if ((isPlayer && currentTimeBetweenDamage >= timeBetweenDamage) || (!isPlayer))
        {
            Armor -= damage;
            if (Armor < 0)
            {
                Health += Armor;
                Armor = 0;
            }
            currentTimeBetweenDamage = 0;
            currentTimeBetweenArmorRegen = 0;
        }
    }
    public void ArmorRegen()
    {
        if (currentTimeBetweenArmorRegen>=timeBetweenArmorRegen)
        {
            Armor += armorIncrease;
            currentTimeBetweenArmorRegen = 0;
        }
    }
    public void EnergyCharging()
    {
        if (Energy < maxEnergy)
        {
            if (currentTimeBetweenRecharge >= rechargeFreq)
            {
                Energy += energyRechargePerSec;
                currentTimeBetweenRecharge = 0;
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
        currentTimeBetweenRecharge += Time.deltaTime;
    }
    public void AddStat(StatsList StatToIncrease, int increase)
    {
        switch (StatToIncrease)
        {
            case StatsList.Armor: 
                this.Armor += increase;
                break;
            case StatsList.Health: 
                this.Health += increase;
                break;
            case StatsList.Energy: 
                this.Energy += increase;
                break;
        }
    }
}
