using UnityEngine;

public class EnemyPlacer : MonoBehaviour
{
    GameObject[] enemies;
    public Transform parent;
    public int enemyCount = 5;
    int count = 0;
    int deletedCount = 0;
    System.Random random = new System.Random();
    int ind;
    int rnd;
    private Vector2 boxSize = new Vector2(32f, 32f);
    LayerMask mask;
    Collider2D[] enemyTiles;
    Collider2D curr;

    DoorActivator doorActivator;

    void Start()
    {
        enemies = GameObject.FindGameObjectWithTag("EnemyTypes").GetComponent<EnemyTypes>().enemies;
        parent = GameObject.FindGameObjectWithTag("SceneSpawner").GetComponent<Transform>();
        mask = LayerMask.GetMask("EnemyTiles");
        doorActivator = this.GetComponent<DoorActivator>();
        Invoke("Costil", 2f);
    }
    public void Costil()
    {
        RoomSpawner.counter = 0;
        //Debug.Log("Костыль работает!");
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
            doorActivator.StartSequence();
        }
    }

    void SpawnEnemy()
    {
        rnd = random.Next(0, enemies.Length);
        Instantiate(enemies[rnd], curr.transform.position, enemies[rnd].transform.rotation, parent);
        Destroy(curr.gameObject);
        deletedCount++;
    }
}
