using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemyAI : MonoBehaviour
{
    public Transform playerTransform;
    public Stats playerStats;
    public LayerMask playerLayer;

    public Animator anim;
    public int damage;
    public Transform attackPos;
    public float damageRange;
    public float attackRange;
    public float speed = 1f;
    public float friction = 0.7f;

    private Rigidbody2D rb;
    private bool isRunning;
    private bool isInAttackRange;
    private Transform anchor;
    private Rect roomRect;
    private Vector2 movement;
    private Vector3 direction;
    private Stats selfStats;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>();
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
            MoveCharacter(movement);
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
    public void MoveCharacter(Vector2 movement)
    {
        rb.MovePosition((Vector2)transform.position + (movement * speed * Time.deltaTime));
    }
    public void DealDamage()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(attackPos.position, damageRange, playerLayer);
        if (playerCollider != null)
        {
            if (playerCollider.CompareTag("Player"))
            {
                if (Random.Range(1, 100) <= selfStats.critChance)
                {
                    playerCollider.GetComponent<Stats>().TakeDamage((int)(damage * (1 + selfStats.critMultiplier / 100f)));
                }
                else
                {
                    playerCollider.GetComponent<Stats>().TakeDamage(damage);
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
