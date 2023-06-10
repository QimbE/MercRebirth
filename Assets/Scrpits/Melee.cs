using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public float attackRange;
    public int damage;

    public Stats playerStats;


    public Transform attackPos;
    public LayerMask enemyLayer;
    public Animator anim;

    private bool startNextAttack = false;

    void Update()
    {
        //Вызов анимации атаки
        if (timeBtwAttack > 0 && Input.GetMouseButton(0) && !startNextAttack)
        {
            startNextAttack = true;
        }
        if (timeBtwAttack <= 0 && !anim.GetBool("isAttack"))
        {
            if (Input.GetMouseButton(0) || startNextAttack)
            {
                anim.SetBool("isAttack", true);
                startNextAttack = false;
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        
    }
    /// <summary>
    /// Во время изменения range показывает круг радуса range
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    /// <summary>
    /// Функция для нанесения дамага на последнем кадре анимации, а не в начале
    /// </summary>
    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (Random.Range(1,100)<=playerStats.critChance)
            {
                enemies[i].GetComponent<Stats>().TakeDamage((int)(damage*(1+playerStats.critMultiplier/100f)));
            }
            else
            {
                enemies[i].GetComponent<Stats>().TakeDamage(damage);
            }
        }
    }

    public void StopAttack()
    {
        anim.SetBool("isAttack", false);
    }
}
