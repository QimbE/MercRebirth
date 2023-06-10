using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UltimateAbility : MonoBehaviour
{
    public Stats playerStats;
    public int ultCost = 100;
    public KeyCode ultKey = KeyCode.Q;
    public GameObject gun;
    public GameObject melee;
    public float ultRadius;
    public float rotationSpeed = 10f;
    public Animator playerAnim;
    public Animator meleeAnim;

    private GameObject gunCopy;
    public float ultTime = 20;
    public float coolDown = 10;
    public float timeBetweenDealDamage = 0.3f;
    private float currentTimeBetweenDealDamage = 0f;
    private float currentTime;
    public bool isUltActive = false;
    private bool isCoolDown = false;
    private float originalRechargeFreq;
    private Vector3 curRotation;

    private void Start()
    {
        currentTime = ultTime + coolDown;
    }
    private void LateUpdate()
    {
        //Игрок прожал ульту с огнестрелом в руках
        if (Input.GetKeyDown(ultKey) && playerStats.energy >= ultCost && gun.activeInHierarchy && !isUltActive && currentTime >= coolDown + ultTime)
        {
            //Debug.Log("Ультанул");
            isUltActive = true;
            currentTime = 0;
            playerStats.energy = 0;
            originalRechargeFreq = playerStats.rechargeFreq;
            playerStats.rechargeFreq = 10000;
            gunCopy = Instantiate(gun, gun.GetComponent<Transform>().position + new Vector3(gun.transform.localScale.x * 0.5f, 0), gun.GetComponent<Transform>().rotation, transform);
        }
        //Игрок прожал ульту с холодным оружием в руках
        else if (Input.GetKeyDown(ultKey) && playerStats.energy >= ultCost && melee.activeInHierarchy && !isUltActive && currentTime >= coolDown + ultTime)
        {
            isUltActive = true;
            currentTime = 0;
            playerStats.energy = 0;
            originalRechargeFreq = playerStats.rechargeFreq;
            playerStats.rechargeFreq = 10000;
            meleeAnim.SetBool("isUlting", true);
            playerAnim.SetBool("isUlting", true);
            curRotation = new Vector3(0, 0, 0);
            currentTimeBetweenDealDamage = 0f;
        }
        //Идёт ульта с огнестрелом в руках
        else if (isUltActive && gun.activeInHierarchy)
        {
            currentTime += Time.deltaTime;
            if (gunCopy != null)
            {
                gunCopy.transform.position = gun.transform.position + new Vector3(gun.transform.localScale.x * 0.5f, 0);
                gunCopy.GetComponent<SpriteRenderer>().sortingOrder = -1;
            }
            if (currentTime >= ultTime && gunCopy != null)
            {
                GameObject.Destroy(gunCopy);
                isUltActive = false;
                isCoolDown = true;
            }
        }

        //Идёт ульта с холодным оружием в руках
        else if (isUltActive && melee.activeInHierarchy)
        {
            currentTime += Time.deltaTime;
            currentTimeBetweenDealDamage += Time.deltaTime;
            if (transform.localScale.x < 0)
            {
                curRotation += new Vector3(0, 0, rotationSpeed);
                curRotation.z %= 360;
                transform.rotation = Quaternion.Euler(curRotation);
            }
            else
            {
                curRotation += new Vector3(0, 0, -rotationSpeed);
                curRotation.z %= 360;
                transform.rotation = Quaternion.Euler(curRotation);
            }
            if (currentTimeBetweenDealDamage >= timeBetweenDealDamage)
            {
                MeleeUltDealDamage();
                currentTimeBetweenDealDamage = 0;
            }
            if (currentTime >= ultTime && melee.activeInHierarchy)
            {
                meleeAnim.SetBool("isUlting", false);
                playerAnim.SetBool("isUlting", false);
                isUltActive = false;
                isCoolDown = true;
                transform.rotation = Quaternion.identity;
            }
            if (currentTime >= ultTime + coolDown)
            {
                playerStats.rechargeFreq = originalRechargeFreq;
            }
        }
        if (isCoolDown)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= ultTime + coolDown)
            {
                playerStats.rechargeFreq = originalRechargeFreq;
                isCoolDown = false;
            }
        }
    }
    public void MeleeUltDealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, ultRadius);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].CompareTag("Enemy") || enemies[i].CompareTag("Box"))
            {
                if (UnityEngine.Random.Range(1, 100) <= playerStats.critChance)
                {
                    //Заменить 14 урона на что-то другое
                    enemies[i].GetComponent<Stats>().TakeDamage((int)(14 * (1 + playerStats.critMultiplier / 100f)));
                }
                else
                {
                    //Заменить 14 урона на что-то другое
                    enemies[i].GetComponent<Stats>().TakeDamage(14);
                }
                //enemies[i].GetComponent<Rigidbody2D>().velocity = (Vector2)(enemies[i].transform.position - transform.position)*2;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, ultRadius);
    }
}

