using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameObject[] enemies;
    public Transform parent;
    void Start()
    {
        enemies = GameObject.FindGameObjectWithTag("EnemyTypes").GetComponent<EnemyTypes>().enemies;
        parent = GameObject.FindGameObjectWithTag("SceneSpawner").GetComponent<Transform>();
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        int rnd = Random.Range(0, enemies.Length);
        Instantiate(enemies[rnd], transform.position, enemies[rnd].transform.rotation, parent);
        Destroy(gameObject);
    }
}
