using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlacer : MonoBehaviour
{
    public List<GameObject> UpgradeButtons;
    public int count = 3;
    public Transform canvas;
    public Stats player;
    public int distanceBetweenButtons = 100;
    private void OnEnable()
    {
        Vector3 cords = new Vector3(Screen.width/9 * 2, Screen.height/3);
        PauseMenu.keyToMenu = KeyCode.None;
        for (int i= 0; i<count; i++)
        {
            int temp = Random.Range(0, UpgradeButtons.Count);
            Instantiate(UpgradeButtons[temp], cords, Quaternion.identity, canvas);
            UpgradeButtons.RemoveAt(temp);
            cords.x += Screen.width/9 * 2.5f;
        }
    }
}

