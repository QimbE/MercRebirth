using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    public GameObject Gun;
    public GameObject Melee;
    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (Gun.activeSelf)
            {
                Gun.SetActive(false);
                Melee.SetActive(true);
            }
            else
            {
                Gun.SetActive(true);
                Melee.SetActive(false);
            }
        }
    }
}
