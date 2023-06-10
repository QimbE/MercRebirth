using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public int armor;
    public int maxArmor;

    public int critChance;
    public int critMultiplier;

    public bool isPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !isPlayer)
        {
            Destroy(gameObject);
        }
        else
        {
            //on player death
        }
    }
    public void TakeDamage(int damage)
    {
        armor -= damage;
        if (armor < 0)
        {
            health += armor;
            armor = 0;
        }
    }
}
