using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameObject[] enemies;

    void Start()
    {
        enemies = GameObject.FindGameObjectWithTag("EnemyTypes").GetComponent<EnemyTypes>().enemies;
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        int rnd = Random.Range(0, enemies.Length);
        Instantiate(enemies[rnd], transform.position, enemies[rnd].transform.rotation);
        Destroy(gameObject);
    }
}
