using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class ClampVelocity : MonoBehaviour
{

    public FloatVariable maxVelocity;

    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxVelocity);
    }
}
