using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BirdRooting))]
[RequireComponent(typeof(BirdMovement))]
[RequireComponent(typeof(BirdDamage))]
public class PlayerStunOnDamage : MonoBehaviour
{

    BirdRooting rooting;
    BirdMovement move;
    BirdDamage damage;

    // Start is called before the first frame update
    void Start()
    {
        rooting = GetComponent<BirdRooting>();
        move = GetComponent<BirdMovement>();
        damage = GetComponent<BirdDamage>();
        damage.OnTakeDamage += StartStun;
        damage.OnRecover += EndStun;
    }

    private void OnDestroy()
    {
        damage.OnTakeDamage -= StartStun;
        damage.OnRecover -= EndStun;
    }

    private void StartStun()
    {
        move.enabled = false;
        rooting.enabled = false;
    }

    private void EndStun()
    {
        move.enabled = true;
        rooting.enabled = true;
    }
}
