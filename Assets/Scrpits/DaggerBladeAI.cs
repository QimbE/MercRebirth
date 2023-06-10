using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DaggerBladeAI : MonoBehaviour
{
    /// <summary>
    /// ���������� ������
    /// </summary>
    public Transform playerTransform;
    /// <summary>
    /// ����� ������
    /// </summary>
    public Stats playerStats;
    /// <summary>
    /// ����, �� ������� ��������� �����
    /// </summary>
    public LayerMask playerLayer;

    /// <summary>
    /// �������� ����� �������
    /// </summary>
    public Animator anim;

    /// <summary>
    /// ������, � ������� ��������� ����
    /// </summary>
    public float damageRange;
    /// <summary>
    /// ����, ��������� ��� �����
    /// </summary>
    public int damage;
    /// <summary>
    /// ����� �� Y �������, � ������� ����� ���� ������� ����
    /// </summary>
    public float offsetY;
    /// <summary>
    /// ������, � ������� ����� ����� ��������
    /// </summary>
    public float attackRange;

    
    /// <summary>
    /// �������� �� ����� ������
    /// </summary>
    public float jumpSpeed = 5f;
    /// <summary>
    /// ����������������� ������ ������
    /// </summary>
    public float jumpDuration = 2f;
    /// <summary>
    /// ����� ����� ��������, ������� ����� ������ ������
    /// </summary>
    public float timeBetweenJumps = 3f;
    /// <summary>
    /// �������� �� ����� ������
    /// </summary>
    public float speed = 1f;
    /// <summary>
    /// ���� ������
    /// </summary>
    public float friction = 0.7f;
    /// <summary>
    /// ����� ����� ������ = timeBetweenJumps - jumpDuration
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
        //����� ������ � �������� � �����
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
        //���������� ����������� ��������
        if (roomRect.Contains(playerTransform.position) && !isJumpStarted)
        {
            //Debug.Log("����� � �������");
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
            //Debug.Log("����� �� � �������");
            rb.velocity = Vector3.zero;
            isRunning = false;
            anim.SetBool("isRunning", isRunning);
        }
    }
    private void FixedUpdate()
    {
        if (roomRect.Contains(playerTransform.position))
        {
            //���� ����� � �������, �� �� � ������� �����
            if (isRunning && !isInAttackRange)
            {
                if (currentTimeBetweenJumps >= timeAfterJump)
                {
                    MoveCharacter(movement);
                }
                isJumpStarted = false;
                Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
            }
            //���� ����� ����� � ������ �����
            else if (isInAttackRange)
            {
                //Debug.Log("����� � ������� �����");
                //����������� �� ������ ����� ������
                if (!isJumpStarted)
                {
                    //Debug.Log("������ �������� ��������");
                    //���� ������ ���������
                    if (currentTimeBetweenJumps >= timeBetweenJumps)
                    {
                        //Debug.Log("������ ����������");
                        rb.velocity = Vector3.zero;
                        
                        jumpSpot = new Vector2(transform.position.x, transform.position.y);
                        playerSpot = (new Vector2(playerTransform.position.x - jumpSpot.x, playerTransform.position.y - jumpSpot.y));
                        isJumpStarted = true;
                        currentJumpDuration = 0;
                        anim.SetBool("isInAttackRange", isInAttackRange);
                        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
                    }
                    //���� ������ �� ���������
                    else
                    {
                        //Debug.Log("������ �� ���������");
                        MoveCharacter(movement * 0.5f);
                    }
                }
                //����������� �� ����� ����� ������, ����� �������
                else
                {
                    //Debug.Log("������ ���");
                    currentJumpDuration += Time.deltaTime;
                    DealDamage();
                    MoveCharacter((playerSpot) * jumpSpeed);
                }
            }
            //��������� ������� � ����������� �� ����������� ��������
            if (movement.x < 0)
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            }
            else
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            //������ ����� ��������
            if (!isJumpStarted)
            {
                //Debug.Log("������ ����� ����� ��������");
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
