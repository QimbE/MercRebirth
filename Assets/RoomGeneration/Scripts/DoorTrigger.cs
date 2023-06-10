using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    DoorActivator parent;

    private void Start()
    {
        parent = transform.parent.GetComponent<DoorActivator>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (parent.isActive == false)
        {
            parent.Activate();
        }
    }
}
