using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float offset = 0;
    public GameObject bullet;
    public Transform shotPoint;
    private float timeBtwShots;
    public float startTimeBtwShots;

    public Stats playerStats;

    public float bulletSpeed;
    public float bulletLifeTime;
    public float bulletDistance;
    public int bulletDamage;

    private void OnEnable()
    {
        Bullet temp = bullet.GetComponent<Bullet>();
        temp.speed = bulletSpeed;
        temp.lifetime= bulletLifeTime;
        temp.distance = bulletDistance;
        temp.damage = bulletDamage;
        temp.playerStats = playerStats;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shotPoint.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x)*Mathf.Rad2Deg;
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(bullet, shotPoint.position, Quaternion.Euler(0f, 0f, rotZ + offset));
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
