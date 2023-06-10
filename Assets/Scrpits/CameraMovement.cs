using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform player;
    public float scale=-10;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        transform.position+= (new Vector3(player.position.x, player.position.y, scale) - transform.position)*Time.deltaTime*3f;
    }
}
