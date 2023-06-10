using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Gun : MonoBehaviour
{
    /// <summary>
    /// ��������, ���� ������ ����� �������� ��� ���������� �����
    /// </summary>
    public float offset = 0;
    /// <summary>
    /// ������, ������� ����� �������� �����
    /// </summary>
    public GameObject bullet;
    /// <summary>
    /// �����, �� ������� ����� �������� ����
    /// </summary>
    public Transform shotPoint;
    /// <summary>
    /// ������� ����� ����� ����������
    /// </summary>
    private float timeBtwShots;
    /// <summary>
    /// ����� ����� ���������� ��������, ����� ������� ����� ����� ��������
    /// </summary>
    public float startTimeBtwShots;

    /// <summary>
    /// ��������� ������ ������, ��� ����� ������ � ���� ������� ����� ������� ���������
    /// </summary>
    public float playerRadius;
    /// <summary>
    /// �������� ����, ��� ������� �� ������� ����� ���������� ���� ���������
    /// </summary>
    public float meleeZoneShiftY;
    /// <summary>
    /// ���������� ������
    /// </summary>
    public Transform playerTransform;
    /// <summary>
    /// ����, ��������� ������ ��������
    /// </summary>
    public int meleeDamage;
    /// <summary>
    /// ������ ����� ���������, ������������ shotPoint
    /// </summary>
    public float attackRange;
    /// <summary>
    /// ���� � �������
    /// </summary>
    public LayerMask enemyLayer;

    /// <summary>
    /// �������������� ���������
    /// </summary>
    public Stats playerStats;

    /// <summary>
    /// �������� ������ ����
    /// </summary>
    public float bulletSpeed;
    /// <summary>
    /// ����� ����� ���� � ��������. �� ��������� �����, ������ ���� ����� ����� �� �����.
    /// </summary>
    public float bulletLifeTime;
    /// <summary>
    /// ����� ������� ����� �����, ������ �������� ����������� ������������
    /// </summary>
    public float bulletDistance;
    /// <summary>
    /// ���� ��������� �����
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
                //�������� z, ���� ��� ���������� �� "�����" ������
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
