using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Gun : MonoBehaviour
{
    /// <summary>
    /// Смещение, если спрайт пушки повернут под интересным углом
    /// </summary>
    public float offset = 0;
    /// <summary>
    /// Объект, которым будет стрелять пушка
    /// </summary>
    public GameObject bullet;
    /// <summary>
    /// Точка, из которой будут вылетать пули
    /// </summary>
    public Transform shotPoint;
    /// <summary>
    /// Текущее время между выстрелами
    /// </summary>
    private float timeBtwShots;
    /// <summary>
    /// Время после последнего выстрела, через которое можно снова стрелять
    /// </summary>
    public float startTimeBtwShots;

    /// <summary>
    /// Примерный радиус игрока, при клике мышкой в этом радиусе игрок ударяет прикладом
    /// </summary>
    public float playerRadius;
    /// <summary>
    /// Смещение зоны, при нажатии на которую будет произведен удар прикладом
    /// </summary>
    public float meleeZoneShiftY;
    /// <summary>
    /// Координаты игрока
    /// </summary>
    public Transform playerTransform;
    /// <summary>
    /// Урон, наносимый ударом приклада
    /// </summary>
    public int meleeDamage;
    /// <summary>
    /// Радиус атаки прикладом, относительно shotPoint
    /// </summary>
    public float attackRange;
    /// <summary>
    /// Слой с врагами
    /// </summary>
    public LayerMask enemyLayer;

    /// <summary>
    /// Характеристики персонажа
    /// </summary>
    public Stats playerStats;

    /// <summary>
    /// Скорость полета пули
    /// </summary>
    public float bulletSpeed;
    /// <summary>
    /// Время жизни пули в секундах. По истечении срока, объект пули будет удалён со сцены.
    /// </summary>
    public float bulletLifeTime;
    /// <summary>
    /// Длина отрезка перед пулей, внутри которого проверяются столкновения
    /// </summary>
    public float bulletDistance;
    /// <summary>
    /// Урон наносимый пулей
    /// </summary>
    public int bulletDamage;
    public Transform parent;

    private void OnEnable()
    {
        Bullet temp = bullet.GetComponent<Bullet>();
        temp.speed = bulletSpeed;
        temp.lifetime= bulletLifeTime;
        temp.distance = bulletDistance;
        temp.damage = bulletDamage;
        temp.playerStats = playerStats;
        parent = GameObject.FindGameObjectWithTag("SceneSpawner").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                //Зануляем z, ведь это расстояние от "линзы" камеры
                Vector3 tempMouseCord = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                tempMouseCord.z = 0;
                Vector3 temp = tempMouseCord - (playerTransform.position - new Vector3(0,meleeZoneShiftY));
                if (math.pow(temp.magnitude,2) <= math.pow(playerRadius,2))
                {
                    GetComponent<Animator>().SetBool("isStockAttack", true);
                }
                else
                {
                    Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shotPoint.position;
                    float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    Instantiate(bullet, shotPoint.position, Quaternion.Euler(0f, 0f, rotZ + offset), parent);
                    GetComponent<Animator>().SetBool("isShot", true);
                }
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        //transform.position = new Vector3(UnityEngine.Random.Range(0, 2), UnityEngine.Random.Range(0, 2));
    }
    public void LateUpdate()
    {
        Vector3 difference1 = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerTransform.position;
        float rotZ1 = Mathf.Atan2(difference1.y, difference1.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ1 + offset);
        transform.position = transform.position  + difference1.normalized*0.7f;
        if (math.abs(rotZ1) > 90)
        {
            transform.localScale = new Vector3(-1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void StockAttackDealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(shotPoint.position, attackRange, enemyLayer);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].CompareTag("Enemy") || enemies[i].CompareTag("Box"))
            {
                if (UnityEngine.Random.Range(1, 100) <= playerStats.critChance)
                {
                    enemies[i].GetComponent<Stats>().TakeDamage((int)(meleeDamage * (1 + playerStats.critMultiplier / 100f)));
                }
                else
                {
                    enemies[i].GetComponent<Stats>().TakeDamage(meleeDamage);
                }
            }
        }
    }
    public void StopStockAttack()
    {
        GetComponent<Animator>().SetBool("isStockAttack", false);
    }
    public void StopShot()
    {
        GetComponent<Animator>().SetBool("isShot", false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerTransform.position - new Vector3(0, meleeZoneShiftY), playerRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(shotPoint.position , attackRange);
    }
}
