using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{

    Animator anim;

    public BirdMovement movement;
    public BirdRooting rooting;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (movement == null) movement = GetComponentInParent<BirdMovement>();
        if (rooting == null) rooting = GetComponentInParent<BirdRooting>();
    }

    private void Update()
    {
        anim.SetBool("IsGrounded", movement.isGrounded);
        anim.SetBool("IsFlapping", movement.isFlapping);
        anim.SetBool("IsRooting", rooting.IsRooting);
    }
}
