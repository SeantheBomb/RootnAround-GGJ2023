using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class BirdMovement : MonoBehaviour
{

    int direction = 1;
    public float jumpForce = 100f;
    public float sideForce = 100f;
    public float downForce = 10f;

    public KeyCode jumpKey = KeyCode.Space;

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


    void UpdateJump()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            float dirForce = sideForce;
            if (Mathf.Sign(body.velocity.x) != Mathf.Sign(direction) && Mathf.Abs(body.velocity.x) > 0.1f)
                 dirForce *= 2;
            body.AddForce(new Vector2(direction * dirForce, jumpForce));
        }
        else if (Input.GetKey(jumpKey) == false)
        {
            body.AddForce(Vector2.down * downForce);
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

    void SetPlayerDirection(int dir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Clamp(dir, -1, 1);
        transform.localScale = scale;
    }

}
