using UnityEngine;

public class FirstDoorDeleter : MonoBehaviour
{
    void Start()
    {
        if (this.transform.parent.position == Vector3.zero)
            Destroy(gameObject);
    }
}
