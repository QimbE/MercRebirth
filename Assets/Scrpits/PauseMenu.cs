using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject MainMenu;
    public static KeyCode keyToMenu = KeyCode.Escape;

    public void Start()
    {
        keyToMenu = KeyCode.Escape;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToMenu))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void BackToMainMenu()
    {
        Instantiate(MainMenu, new Vector3(0, 0, 0), Quaternion.identity);
        Time.timeScale = 1.0f;
        Destroy(GameObject.FindGameObjectWithTag("SceneSpawner"));
    }
    public void HelpInit()
    {
        OpenHelper.HelpMe();
        Debug.Log("Хэлпанул (Implemented!!)");
    }
}
