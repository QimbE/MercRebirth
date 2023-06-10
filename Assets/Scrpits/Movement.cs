using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    public float speed = 1f;
    public float friction = 0.7f;
    public Animator anim;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        verticalMove = Input.GetAxisRaw("Vertical") * speed;
    }
    private void FixedUpdate()
    {
        if (horizontalMove==0 && verticalMove==0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
        Vector2 targetVelocity = new Vector2(horizontalMove+ rigidBody.velocity.x, rigidBody.velocity.y+ verticalMove);
        if (targetVelocity.magnitude > speed)
        {
            targetVelocity /= 1.414f;
        }
        rigidBody.velocity = targetVelocity * friction;
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (difference.x < 0)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }
        else
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
    }
}
