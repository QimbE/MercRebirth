using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;  //библиотека сцен

public class MainMenu : MonoBehaviour
{
    public GameObject toSpawn;
    //public void Start()
    //{
    //    QualitySettings.vSyncCount = 0;
    //    Application.targetFrameRate = 60;
    //}
    public void PlayGame() 
    {
        SpawnScene();
        RoomSpawner.counter = 0;
        Destroy(GameObject.FindGameObjectWithTag("MainMenu"));
    }

    public void ExitGame() 
    {
        Debug.Log("Игра закрылась");
        Application.Quit();
    }
    public GameObject SpawnScene()
    {
        return Instantiate<GameObject>(toSpawn, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
