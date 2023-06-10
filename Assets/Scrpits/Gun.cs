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
    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f, rotZ + offset);
        if (difference.x < 0)
        {
            transform.localScale = new Vector3(-1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(bullet, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
