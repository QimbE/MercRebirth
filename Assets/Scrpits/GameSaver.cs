using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SaveGame",3f);
    }
    public void SaveGame() => Storage.Save();
}
