using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{

    Animator anim;

    public BirdMovement movement;
    public BirdRooting rooting;
    public BirdDamage damage;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (movement == null) movement = GetComponentInParent<BirdMovement>();
        if (rooting == null) rooting = GetComponentInParent<BirdRooting>();
        if (damage == null) damage = GetComponentInParent<BirdDamage>();

        damage.OnTakeDamage += OnTakeDamage;
        damage.OnRecover += OnRecover;
    }

    private void OnDestroy()
    {
        damage.OnTakeDamage -= OnTakeDamage;
        damage.OnRecover -= OnRecover;
    }

    private void Update()
    {
        anim.SetBool("IsGrounded", movement.isGrounded);
        anim.SetBool("IsFlapping", movement.isFlapping);
        anim.SetBool("IsRooting", rooting.IsRooting);
    }

    private void OnTakeDamage()
    {
        anim.SetTrigger("Hurt");
    }

    private void OnRecover()
    {
        anim.SetTrigger("HurtRecover");
    }

}
