using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GlobalEnemyCounter : MonoBehaviour
{
    public int globalCount = 10;
    public UnityEvent onLevelCompletion;
    private List<DoorActivator> counters;
    private bool isDone = false;
    public GameObject portal;
    public Transform parent;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("FindCounters",5f);
    }
    public void FindCounters()
    {
        //Debug.Log(GameObject.FindGameObjectsWithTag("Anchor").Length);
        counters = new List<DoorActivator>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Layout"))
        {
            counters.Add(obj.GetComponent<DoorActivator>());
            StartCoroutine("AsyncCount");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (globalCount <= 0 && isDone)
        {
            Debug.Log("Враги закончились");
            isDone = false;
            OnLevelCompletion();
            StopCoroutine("AsyncCount");
        }
        if (Input.GetKeyDown(KeyCode.I) && !isDone)
        {
            Debug.Log("Вы - читер");
            isDone = true;
            OnLevelCompletion();
            StopCoroutine("AsyncCount");
        }
    }
    public void OnLevelCompletion()
    {
        onLevelCompletion?.Invoke();
    }
    private IEnumerator AsyncCount()
    {
        while (true)
        {
            Count();
            yield return new WaitForSeconds(4f);
        }
    }
    public void Count()
    {
        //Debug.Log("Веду подсчет врагов");
        globalCount = 0;
        foreach (DoorActivator counter in counters)
        {
            globalCount += counter.enemyCount;
        }
        if (globalCount == 0)
        {
            isDone = true;
        }
    }
    public void InstancePortal()
    {
        Vector3 posVec = counters[0].transform.position - player.position;
        Vector3 minVec = Vector3.zero;
        foreach(DoorActivator counter in counters)
        {
            if (posVec.magnitude > (counter.transform.position - player.position).magnitude)
            {
                //Debug.Log("Ищу место");
                posVec = counter.transform.position - player.position;
                minVec = counter.transform.position;
            }
        }
        Instantiate(portal, minVec + new Vector3(6,0,0), Quaternion.identity, parent);
    }
}
