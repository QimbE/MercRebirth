using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public GameObject scene;
    // Start is called before the first frame update
    void Start()
    {
        GameObject tempPlayer = Instantiate(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("SceneSpawner"));
        Storage.Save(tempPlayer);
        Destroy(tempPlayer);
        Instantiate(scene);
        Debug.Log("Загрузка на тоненького");
        Invoke("TempFunc",1.5f);
        //Storage.Load();
    }
    public void TempFunc() => Storage.Load();
}
//public static class PlayerHolder
//{
//    public static GameObject PlayerPrefab { get; set; }
//}
