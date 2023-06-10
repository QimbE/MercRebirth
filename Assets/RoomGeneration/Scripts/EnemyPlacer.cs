using UnityEngine;

public class EnemyPlacer : MonoBehaviour
{
    GameObject[] enemies;

    public int enemyCount = 25;
    int count = 0;
    int deletedCount = 0;
    System.Random random = new System.Random();
    int ind;
    int rnd;
    private Vector2 boxSize = new Vector2(32f, 32f);
    LayerMask mask;
    Collider2D[] enemyTiles;
    Collider2D curr;

    void Start()
    {
        enemies = GameObject.FindGameObjectWithTag("EnemyTypes").GetComponent<EnemyTypes>().enemies;
        mask = LayerMask.GetMask("EnemyTiles");
    }

    void Update()
    {
        if (count < enemyCount)
        {
            enemyTiles = Physics2D.OverlapBoxAll(transform.position, boxSize, 0, mask);
            ind = random.Next(0, enemyTiles.Length);
            curr = enemyTiles[ind];
            SpawnEnemy();
            count++;
        }
        if (enemyCount == deletedCount)
        {
            enemyTiles = Physics2D.OverlapBoxAll(transform.position, boxSize, 0, mask);
            foreach (var tile in enemyTiles)
                Destroy(tile.gameObject);
            deletedCount = -1;
        }
    }

    void SpawnEnemy()
    {
        rnd = random.Next(0, enemies.Length);
        Instantiate(enemies[rnd], curr.transform.position, enemies[rnd].transform.rotation);
        Destroy(curr.gameObject);
        deletedCount++;
    }
}
