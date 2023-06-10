using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantUseItem : MonoBehaviour
{
    /// <summary>
    /// ���������� ��� ������
    /// </summary>
    public enum StatsList
    {
        Armor,
        Health,
        Energy
    }
    /// <summary>
    /// ������������� ����
    /// </summary>
    public StatsList StatToIncrease;
    /// <summary>
    /// ��������� �����
    /// </summary>
    public int increase = 20;
    /// <summary>
    /// ����� ������
    /// </summary>
    private Stats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        //������� ������ ����� ��������� �� �����
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���� ����� ��������, �� ����������� ������ ���� � ����������������
        if (collision.tag == "Player")
        {
            playerStats.AddStat(StatToIncrease, increase);
            Destroy(gameObject);
        }
    }
}
