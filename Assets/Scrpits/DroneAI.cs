using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DroneAI : Enemy
{


    public GameObject bullet;
    /// <summary>
    /// Точка, из которой будут вылетать пули
    /// </summary>
    public Transform shotPoint;
    /// <summary>
    /// Текущее время между выстрелами
    /// </summary>
    private float timeBtwShots;
    /// <summary>
    /// Время после последнего выстрела, через которое можно снова стрелять
    /// </summary>
    public float startTimeBtwShots;
    /// <summary>
    /// Скорость полета пули
    /// </summary>
    public float bulletSpeed;
    /// <summary>
    /// Время жизни пули в секундах. По истечении срока, объект пули будет удалён со сцены.
    /// </summary>
    public float bulletLifeTime;
    /// <summary>
    /// Длина отрезка перед пулей, внутри которого проверяются столкновения
    /// </summary>
    public float bulletDistance;
    /// <summary>
    /// Урон наносимый пулей
    /// </summary>
    public int bulletDamage;
    public float timeBetweenDirectionSwitch = 2f;

    /// <summary>
    /// Скорость во время ходьбы
    /// </summary>
    public float speed = 1f;

    private float curTimeBetweenDirectionSwitch = 0;
    private float angleCir = 0;
    private int dir =1;
    private Vector2 curDirection;
    private bool isRunning;


    // Start is called before the first frame update
    void Start()
    {
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
        Bullet tempBullet = bullet.GetComponent<Bullet>();
        tempBullet.speed = bulletSpeed;
        tempBullet.lifetime = bulletLifeTime;
        tempBullet.distance = bulletDistance;
        tempBullet.damage = bulletDamage;
        tempBullet.playerStats = selfStats;
        roomRect = new Rect(anchor.position.x - 16, anchor.position.y - 16, 32, 32);
    }

    // Update is called once per frame
    void Update()
    {
        //Нахождение направления движения
        if (roomRect.Contains(playerTransform.position))
        {
            DealDamage();
            isRunning = true;
            //anim.SetBool("isRunning", isRunning);
            direction = playerTransform.position - transform.position;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            direction.Normalize();
            movement = direction;
            Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        }
        else
        {
            rb.velocity = Vector3.zero;
            isRunning = false;
            //anim.SetBool("isRunning", isRunning);
        }
        timeBtwShots += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (roomRect.Contains(playerTransform.position))
        {
            //Если игрок в комнате, но не в радиусе атаки
            if (isRunning)
            {
                MoveCharacter(movement);
            }
            //Отражение спрайта в зависимости от направления движения
            if (movement.x < 0)
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            else
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            }
            if (curTimeBetweenDirectionSwitch>=timeBetweenDirectionSwitch)
            {
                curTimeBetweenDirectionSwitch = 0;
                if (Random.Range(0,10)>=5)
                {
                    dir = 1;
                }
                else
                {
                    dir = -1;
                }
            }
            angleCir += Time.deltaTime*0.5f;
            angleCir %= 360;
            curTimeBetweenDirectionSwitch += Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    public override void DealDamage()
    {
        if (timeBtwShots >= startTimeBtwShots)
        {
            Vector3 difference = playerTransform.position - shotPoint.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Instantiate(bullet, shotPoint.position, Quaternion.Euler(0f, 0f, rotZ));
            timeBtwShots = 0;
        }
    }
    public override void MoveCharacter(Vector2 movement)
    {
        curDirection = new Vector2(Mathf.Cos(angleCir * dir), Mathf.Sin(angleCir * dir));
        rb.velocity+= (0.4f * speed * movement + 0.6f * direction.magnitude * curDirection)*0.5f;
    }
}
