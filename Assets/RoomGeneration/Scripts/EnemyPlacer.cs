using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPlacer : MonoBehaviour
{
    GameObject[] enemies;

    public int enemyCount = 5;
    static int count = 0;
    static int deletedCount = 0;
    private Vector2 boxSize = new Vector2(32f, 32f);

    Collider2D curr;

    void Start()
    {
        enemies = GameObject.FindGameObjectWithTag("EnemyTypes").GetComponent<EnemyTypes>().enemies;
        LayerMask mask = LayerMask.GetMask("EnemyTiles");
        Collider2D[] enemyTiles = Physics2D.OverlapBoxAll(transform.position, boxSize, 0, mask);
        while (count < enemyCount)
        {
            int ind = Random.Range(0, enemyTiles.Length);
            curr = enemyTiles[ind];
            Invoke("SpawnEnemy", 3f);
            count++;
        }
    }

    void Update()
    {
        if (enemyCount == deletedCount)
        {
            LayerMask mask = LayerMask.GetMask("EnemyTiles");
            Collider2D[] enemyTiles = Physics2D.OverlapBoxAll(transform.position, boxSize, 0, mask);
            foreach (var tile in enemyTiles)
                Destroy(tile.gameObject);
            deletedCount = -1;
        }
    }

    void SpawnEnemy()
    {
        int rnd = Random.Range(0, enemies.Length);
        Instantiate(enemies[rnd], curr.transform.position, enemies[rnd].transform.rotation);
        Destroy(curr.gameObject);
        deletedCount++;
    }
}
