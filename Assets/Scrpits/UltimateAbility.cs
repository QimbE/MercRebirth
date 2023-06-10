using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateAbility : MonoBehaviour
{
    public Stats playerStats;
    public int ultCost = 100;
    public KeyCode ultKey = KeyCode.Q;
    public GameObject gun;
    public GameObject melee;
    public Animator playerAnim;
    public Animator meleeAnim;

    private GameObject gunCopy;
    public float ultTime = 20;
    public float coolDown = 10;
    private float currentTime;
    public bool isUltActive = false;
    private float originalRechargeFreq;

    private void Start()
    {
        currentTime = ultTime + coolDown;
    }
    private void LateUpdate()
    {
        //Debug.Log($"{Input.GetKeyDown(ultKey)} , {playerStats.energy >= ultCost}, {gun.activeInHierarchy}, {!isUltActive}, {currentTime >= coolDown + ultTime}");
        if (Input.GetKeyDown(ultKey) && playerStats.energy >= ultCost && gun.activeInHierarchy && !isUltActive && currentTime >= coolDown + ultTime)
        {
            //Debug.Log("Ультанул");
            isUltActive = true;
            currentTime = 0;
            playerStats.energy = 0;
            originalRechargeFreq = playerStats.rechargeFreq;
            playerStats.rechargeFreq = 10000;
            gunCopy = Instantiate(gun, gun.GetComponent<Transform>().position+ new Vector3(gun.transform.localScale.x * 0.5f, 0), gun.GetComponent<Transform>().rotation, transform);
        }
        else if (Input.GetKeyDown(ultKey) && playerStats.energy >= ultCost && melee.activeInHierarchy && !isUltActive && currentTime >= coolDown + ultTime)
        {
            isUltActive = true;
            currentTime = 0;
            playerStats.energy = 0;
            originalRechargeFreq = playerStats.rechargeFreq;
            playerStats.rechargeFreq = 10000;
            meleeAnim.SetBool("isUlting", true);
            playerAnim.SetBool("isUlting", true);
        }
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
            }
            if (currentTime >= ultTime + coolDown)
            {
                playerStats.rechargeFreq = originalRechargeFreq;
                isUltActive = false;
            }
        }
        else if (isUltActive && melee.activeInHierarchy)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= ultTime && gunCopy != null)
            {
                meleeAnim.SetBool("isUlting", false);
                playerAnim.SetBool("isUlting", false);
            }
            if (currentTime >= ultTime + coolDown)
            {
                playerStats.rechargeFreq = originalRechargeFreq;
                isUltActive = false;
            }
        }
    }
}

