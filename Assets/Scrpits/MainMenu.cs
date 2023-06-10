using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //библиотека сцен

public class MainMenu : MonoBehaviour
{
    //To start a game
    public void PlayGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void ExitGame() 
    {
        Debug.Log("Игра закрылась");
        Application.Quit();
    }
}
