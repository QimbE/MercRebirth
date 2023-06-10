using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGameSaver : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Storage.Save();
            Debug.Log("Сохранил на I");
        }
    }
}
