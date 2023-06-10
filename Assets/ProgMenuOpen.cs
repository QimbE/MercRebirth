using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgMenuOpen : MonoBehaviour
{
    public GameObject progMenu;

    public void OpenProgMenu()
    {
        progMenu.SetActive(true);
    }
}
