using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantUseItem : MonoBehaviour
{
    /// <summary>
    /// Энумиратор для статов
    /// </summary>
    public enum StatsList
    {
        Armor,
        Health,
        Energy
    }
    /// <summary>
    /// Увеличиваемый стат
    /// </summary>
    public StatsList StatToIncrease;
    /// <summary>
    /// Инкремент стата
    /// </summary>
    public int increase = 20;
    /// <summary>
    /// Статы игрока
    /// </summary>
    private Stats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        //Находим игрока после появления на сцене
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Если игрок наступил, то увеличиваем нужный стат и самоуничтожаемся
        if (collision.tag == "Player")
        {
            playerStats.AddStat(StatToIncrease, increase);
            Destroy(gameObject);
        }
    }
}
