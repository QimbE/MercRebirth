using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortalScript : MonoBehaviour
{
    public Animator anim;
    public bool isAppear = true;
    private bool isTrigging = false;
    public ProgMenuOpen openner;
    //public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        openner = GameObject.FindGameObjectWithTag("ProgressionMenu").GetComponent<ProgMenuOpen>();
        anim = this.GetComponent<Animator>();
        if (isAppear)
        {
            Appear();
        }
        else
        {
            Quit();
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (isTrigging && collision.CompareTag("Player"))
        {
            // Debug.Log("Задет триггер");
            // Тут впринципе можно замораживать время или еще чего делать для безопасности, но т.к. смысла в этом нет обойдёмся без этого
            openner.OpenProgMenu();
            Destroy(this);
        }
    }
    public void Appear()
    {
        anim.SetBool("isAppear", true);
    }
    public void PreDisappear()
    {
        anim.SetBool("isAppear", false);
        Invoke("Disappear", 4f);
    }
    public void Disappear()
    {
        anim.SetBool("isDisappear", true);
    }
    public void PostDisappear()
    {
        anim.SetBool("isDisappear", false);
        this.gameObject.SetActive(false);
    }
    public void Quit()
    {
        anim.SetBool("isExit", true);
    }
    public void PostQuit()
    {
        anim.SetBool("isExit", false);
        Invoke("StartTrigging", 2f);
    }
    public void StartTrigging()
    {
        isTrigging = true;
    }
}
