using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    public GameObject Gun;
    public GameObject Melee;
    public UltimateAbility ult;
    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && !ult.isUltActive)
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
