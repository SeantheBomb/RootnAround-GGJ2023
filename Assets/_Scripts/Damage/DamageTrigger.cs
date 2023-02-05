using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BirdDamage bird = collision.GetComponentInParent<BirdDamage>();
        if(bird != null)
        {
            bird.TakeDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BirdDamage bird = collision.collider.GetComponentInParent<BirdDamage>();
        if (bird != null)
        {
            bird.TakeDamage();
        }
    }

}
