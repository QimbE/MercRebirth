using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public Stats playerStats;
    public int damage;
    protected Transform anchor;
    protected Rect roomRect;
    protected Vector2 movement;
    protected Vector3 direction;
    protected Stats selfStats;
    protected Rigidbody2D rb;
    public abstract void DealDamage();
    public abstract void MoveCharacter(Vector2 movement);
}
public class DefaultEnemyAI : Enemy
{
    public LayerMask playerLayer;

    public Animator anim;
    public Transform attackPos;
    public float damageRange;
    public float attackRange;
    public float speed = 1f;
    public float friction = 0.7f;

    private Rigidbody2D playerRb;
    private bool isRunning;
    private bool isInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        GameObject tempPlayer = GameObject.FindWithTag("Player");
        playerTransform = tempPlayer.GetComponent<Transform>();
        playerStats = tempPlayer.GetComponent<Stats>();
        playerRb = tempPlayer.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        selfStats = GetComponent<Stats>();
        List<GameObject> anchors= new List<GameObject>(GameObject.FindGameObjectsWithTag("Anchor"));
        for (int i = 0; i<anchors.Count; i++)
        {
            Vector3 temp = anchors[i].transform.position - transform.position;
            if (temp.magnitude<23)
            {
                anchor = anchors[i].transform;
                break;
            }
        }
        roomRect = new Rect(anchor.position.x-16, anchor.position.y-16, 32, 32);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isRunning", isRunning);
        if (roomRect.Contains(playerTransform.position))
        {
            isRunning = true;
            isInAttackRange = (playerTransform.position - transform.position).magnitude<=attackRange;
            direction = playerTransform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            direction.Normalize();
            movement = direction;
        }
        else
        {
            isRunning = false;
        }
    }
    private void FixedUpdate()
    {
        if (isRunning && !isInAttackRange)
        {
            MoveCharacter((movement+playerRb.velocity*0.01f).normalized);
        }
        else if (isInAttackRange)
        {
            rb.velocity= Vector2.zero;
            anim.SetBool("isInAttackRange", isInAttackRange);
        }
        if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }
        else
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
    }
    public override void MoveCharacter(Vector2 movement)
    {
        rb.MovePosition((Vector2)transform.position + (movement * speed * Time.deltaTime));
    }
    public override void DealDamage()
    {
        Collider2D[] playerCollider = Physics2D.OverlapCircleAll(attackPos.position, damageRange, playerLayer);
        for (int i = 0; i < playerCollider.Length; i++)
        {
            if (playerCollider[i] != null)
            {
                if (playerCollider[i].CompareTag("Player") || playerCollider[i].CompareTag("Box"))
                {
                    if (Random.Range(1, 100) <= selfStats.critChance)
                    {
                        playerCollider[i].GetComponent<Stats>().TakeDamage((int)(damage * (1 + selfStats.critMultiplier / 100f)));
                    }
                    else
                    {
                        playerCollider[i].GetComponent<Stats>().TakeDamage(damage);
                    }
                }
            }
        }
    }
    public void StopAttackAnimation()
    {
        anim.SetBool("isInAttackRange", false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, damageRange);
    }
}
