using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    /// <summary>
    /// Õ» ¬  Œ≈Ã —À”◊¿≈ Õ≈ Ã≈Õﬂ“‹!!!!!!!!!!!!!!!!!!11!1!11!!!!
    /// </summary>
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    private float start;

    public Stats playerStats;
    public bool isEnemies = false;

    private string hitTag;
    private void OnEnable()
    {
        start = Time.time;
        if (isEnemies)
        {
            hitTag = "Player";
        }
        else
        {
            hitTag = "Enemy";
        }
    }
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag(hitTag) || hitInfo.collider.CompareTag("Box"))
            {
                if (Random.Range(1, 100) <= playerStats.critChance)
                {
                    hitInfo.collider.GetComponent<Stats>().TakeDamage((int)(damage * (1 + playerStats.critMultiplier / 100f)));
                }
                else
                {
                    hitInfo.collider.GetComponent<Stats>().TakeDamage(damage);
                }
            }
            else if (hitInfo.collider.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        //Bullet delete
        if (Time.time-start >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
