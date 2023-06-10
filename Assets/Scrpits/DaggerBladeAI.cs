using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DaggerBladeAI : MonoBehaviour
{
    /// <summary>
    /// Координаты игрока
    /// </summary>
    public Transform playerTransform;
    /// <summary>
    /// Статы игрока
    /// </summary>
    public Stats playerStats;
    /// <summary>
    /// Слой, на котором находится игрок
    /// </summary>
    public LayerMask playerLayer;

    /// <summary>
    /// Аниматор этого объекта
    /// </summary>
    public Animator anim;

    /// <summary>
    /// Радиус, в котором наносится урон
    /// </summary>
    public float damageRange;
    /// <summary>
    /// Урон, наносимый при атаке
    /// </summary>
    public int damage;
    /// <summary>
    /// Сдвиг по Y радиуса, в котором может быть нанесен урон
    /// </summary>
    public float offsetY;
    /// <summary>
    /// Радиус, в котором атака может начаться
    /// </summary>
    public float attackRange;

    
    /// <summary>
    /// Скорость во время прыжка
    /// </summary>
    public float jumpSpeed = 5f;
    /// <summary>
    /// Продолжительность самого прыжка
    /// </summary>
    public float jumpDuration = 2f;
    /// <summary>
    /// Время между прыжками, включая время самого прыжка
    /// </summary>
    public float timeBetweenJumps = 3f;
    /// <summary>
    /// Скорость во время ходьбы
    /// </summary>
    public float speed = 1f;
    /// <summary>
    /// Сила трения
    /// </summary>
    public float friction = 0.7f;
    /// <summary>
    /// Время после прыжка = timeBetweenJumps - jumpDuration
    /// </summary>
    private float timeAfterJump;

    private Rigidbody2D rb;
    private bool isRunning;
    private bool isInAttackRange;
    private Transform anchor;
    private Rect roomRect;
    private Vector2 movement;
    private Vector3 direction;
    private Stats selfStats;

    private Vector2 jumpSpot;
    private Vector2 playerSpot;
    private float currentJumpDuration;
    private float currentTimeBetweenJumps = 0;
    private bool isJumpStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        timeAfterJump = timeBetweenJumps - jumpDuration;
        //Поиск игрока и привязка к якорю
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>();
        rb = GetComponent<Rigidbody2D>();
        selfStats = GetComponent<Stats>();
        List<GameObject> anchors = new List<GameObject>(GameObject.FindGameObjectsWithTag("Anchor"));
        for (int i = 0; i < anchors.Count; i++)
        {
            Vector3 temp = anchors[i].transform.position - transform.position;
            if (temp.magnitude < 23)
            {
                anchor = anchors[i].transform;
                break;
            }
        }
        roomRect = new Rect(anchor.position.x - 16, anchor.position.y - 16, 32, 32);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Нахождение направления движения
        if (roomRect.Contains(playerTransform.position) && !isJumpStarted)
        {
            //Debug.Log("Игрок в комнате");
            isRunning = true;
            anim.SetBool("isRunning", isRunning);
            isInAttackRange = (playerTransform.position - transform.position).magnitude <= attackRange;
            direction = playerTransform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            direction.Normalize();
            movement = direction;
        }
        else
        {
            //Debug.Log("Игрок не в комнате");
            rb.velocity = Vector3.zero;
            isRunning = false;
            anim.SetBool("isRunning", isRunning);
        }
    }
    private void FixedUpdate()
    {
        if (roomRect.Contains(playerTransform.position))
        {
            //Если игрок в комнате, но не в радиусе атаки
            if (isRunning && !isInAttackRange)
            {
                if (currentTimeBetweenJumps >= timeAfterJump)
                {
                    MoveCharacter(movement);
                }
                isJumpStarted = false;
                Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
            }
            //Если игрок попал в радиус атаки
            else if (isInAttackRange)
            {
                //Debug.Log("Игрок в радиусе атаки");
                //Срабатывает на первом кадре прыжка
                if (!isJumpStarted)
                {
                    //Debug.Log("Прыжок пытается начаться");
                    //Если прыжок отКДшился
                    if (currentTimeBetweenJumps >= timeBetweenJumps)
                    {
                        //Debug.Log("Прыжок начинается");
                        rb.velocity = Vector3.zero;
                        
                        jumpSpot = new Vector2(transform.position.x, transform.position.y);
                        playerSpot = (new Vector2(playerTransform.position.x - jumpSpot.x, playerTransform.position.y - jumpSpot.y));
                        isJumpStarted = true;
                        currentJumpDuration = 0;
                        anim.SetBool("isInAttackRange", isInAttackRange);
                        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
                    }
                    //Если прыжок не отКДшился
                    else
                    {
                        //Debug.Log("Прыжок не откдшился");
                        MoveCharacter(movement * 0.5f);
                    }
                }
                //Срабатывает на любом кадре прыжка, кроме первого
                else
                {
                    //Debug.Log("Прыжок идёт");
                    currentJumpDuration += Time.deltaTime;
                    DealDamage();
                    MoveCharacter((playerSpot) * jumpSpeed);
                }
            }
            //Отражение спрайта в зависимости от направления движения
            if (movement.x < 0)
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            }
            else
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            //Таймер между прыжками
            if (!isJumpStarted)
            {
                //Debug.Log("Считаю время между прыжками");
                currentTimeBetweenJumps += Time.deltaTime;
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    public void DealDamage()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position - new Vector3(0, offsetY, 0), damageRange, playerLayer);
        if (playerCollider != null)
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
    public void MoveCharacter(Vector2 movement)
    {
        rb.velocity = movement * speed;
    }
    public void StopAttackAnimation()
    {
        anim.SetBool("isInAttackRange", false);
        currentTimeBetweenJumps = 0;
        isRunning = true;
        isJumpStarted= false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, offsetY, 0), damageRange);
    }
}
