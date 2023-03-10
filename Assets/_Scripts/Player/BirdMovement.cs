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

    public float jumpModifier = 1f;

    Rigidbody2D body;

    List<Collider2D> groundCollisions = new List<Collider2D>();

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

    //private void FixedUpdate()
    //{
    //    UpdateIsGrounded();
    //}


    void UpdateJump()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            float dirForce = sideForce;
            if (Mathf.Sign(body.velocity.x) != Mathf.Sign(direction) && Mathf.Abs(body.velocity.x) > 0.1f)
                 dirForce *= 2;
            body.AddForce(new Vector2(direction * dirForce, jumpForce * jumpModifier));
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

    //void UpdateIsGrounded()
    //{
    //    //body.i
    //    if(Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance))
    //    {
    //        isGrounded = true;
    //    }
    //    else
    //    {
    //        isGrounded = false;
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.GetContact(0).point.y <= transform.position.y)
        {
            groundCollisions.Add(collision.collider);
            isGrounded = true;
        }   
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (groundCollisions.Contains(collision.collider))
        {
            groundCollisions.Remove(collision.collider);
            if (groundCollisions.Count <= 0)
            {
                isGrounded = false;
            }
        }
    }

    void SetPlayerDirection(int dir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Clamp(dir, -1, 1);
        transform.localScale = scale;
    }

}
