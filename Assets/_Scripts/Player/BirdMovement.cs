using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class BirdMovement : MonoBehaviour
{

    int direction = 1;
    public FloatVariable jumpForce = 100f;
    public FloatVariable sideForce = 100f;
    public FloatVariable downForce = 10f;

    public float groundCheckDistance = 0.1f;

    public KeyCode jumpKey = KeyCode.Space;

    public bool isGrounded
    {
        get; protected set;
    }

    public bool isFlapping
    {
        get; protected set;
    }

    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
        UpdateJump();
    }

    private void FixedUpdate()
    {
        UpdateIsGrounded();
    }


    void UpdateJump()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            float dirForce = sideForce;
            if (Mathf.Sign(body.velocity.x) != Mathf.Sign(direction) && Mathf.Abs(body.velocity.x) > 0.1f)
                 dirForce *= 2;
            body.AddForce(new Vector2(direction * dirForce, jumpForce));
            isFlapping = true;
        }
        else if (Input.GetKey(jumpKey) == false)
        {
            body.AddForce(Vector2.down * downForce);
            isFlapping = false;
        }
    }

    void UpdateDirection()
    {
        float input = Input.GetAxisRaw("Horizontal");
        if (input > 0)
            direction = 1;
        if (input < 0)
            direction = -1;
        SetPlayerDirection(direction);
    }

    void UpdateIsGrounded()
    {
        if(Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void SetPlayerDirection(int dir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Clamp(dir, -1, 1);
        transform.localScale = scale;
    }

}
