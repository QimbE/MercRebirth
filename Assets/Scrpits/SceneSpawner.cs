using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawner : MonoBehaviour
{
    public GameObject toSpawn;
    // Start is called before the first frame update
    void Start()
    {
        SpawnScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject SpawnScene()
    {
        return Instantiate<GameObject>(toSpawn, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
