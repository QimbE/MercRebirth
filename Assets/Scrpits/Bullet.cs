using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            //Тут поставить столкновение с врагом
            //if(hitInfo.collider != null)
            //{
            //    if(hitInfo.collider.CompareTag("Enemy"))
            //    {
            //        hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            //    }
            //    Destroy(gameObject);
            //}
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        //Дописать таймер удаления
    }
}
