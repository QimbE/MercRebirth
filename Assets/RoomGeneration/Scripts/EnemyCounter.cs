using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public int enemyCount;
    private Vector2 boxSize = new Vector2(32f, 32f);

    void Update()
    {
        LayerMask mask = LayerMask.GetMask("Enemies");
        enemyCount = Physics2D.OverlapBoxAll(transform.position, boxSize, 0, mask).Length;
        if (Input.GetKeyDown(KeyCode.I))
        {
            foreach (Collider2D col in Physics2D.OverlapBoxAll(transform.position, boxSize, 0, mask))
            {
                Destroy(col.gameObject);
            }
        }
    }
}
