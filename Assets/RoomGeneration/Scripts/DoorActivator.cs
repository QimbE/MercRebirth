using System.Collections;
using UnityEngine;

public class DoorActivator : MonoBehaviour
{
    private Vector2 boxSize = new Vector2(32f, 32f);
    private Vector3 startPos = new Vector3(0f, 0f, 0f);
    private LayerMask doorMask;
    private LayerMask enemyMask;
    public bool isActive = false;
    public bool isStarted = false;
    public bool isFinished = false;
    internal int enemyCount { get => enemies.Length; }

    Collider2D[] doors;
    Collider2D[] enemies;

    public void StartSequence()
    {
        doorMask = LayerMask.GetMask("Doors");
        enemyMask = LayerMask.GetMask("Enemies");
        doors = Physics2D.OverlapBoxAll(transform.position, boxSize, 0, doorMask);
        enemies = Physics2D.OverlapBoxAll(transform.position, boxSize, 0, enemyMask);
        isStarted = true;
        Deactivate();
        StartCoroutine("AsyncCount");
    }

    private void Start()
    {
        if (this.transform.position == startPos)
        {
            Invoke("DestroyDoors", 1f);
        }
    }

    private IEnumerator AsyncCount()
    {
        while (true)
        {
            if (isActive)
                enemies = Physics2D.OverlapBoxAll(transform.position, boxSize, 0, enemyMask);
            yield return new WaitForSeconds(1f);
        }
    }

    private void Update()
    {
        if (isStarted && isActive && !isFinished && enemyCount == 0)
        {
            DestroyDoors();
            isFinished = true;
        }
    }

    public void Activate()
    {
        Debug.Log("Doors were activated");
        Debug.Log($"Doors in room: {doors.Length}");
        Debug.Log($"Enemies in room: {enemies.Length}");
        isActive = true;
        foreach (var door in doors)
        {
            door.gameObject.SetActive(true);
        }
        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }

    public void Deactivate()
    {
        Debug.Log("Doors were deactivated");
        Debug.Log($"Doors in room: {doors.Length}");
        Debug.Log($"Enemies in room: {enemies.Length}");
        isActive = false;
        foreach (var door in doors)
        {
            door.gameObject.SetActive(false);
        }
        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    public void DestroyDoors()
    {
        foreach (var door in doors)
        {
            Destroy(door.gameObject);
        }
    }
}
