using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdDamage : MonoBehaviour
{

    public System.Action OnTakeDamage, OnRecover, OnImmunityStart, OnImmunityEnd;

    public IntVariable hitCooldown;

    public IntVariable stunTime;

    public bool IsImmune;


    public void TakeDamage()
    {
        if (IsImmune)
        {
            return;
        }
        StartCoroutine(HandleDamage());
    }

    IEnumerator HandleDamage()
    {
        IsImmune = true;
        OnTakeDamage?.Invoke();
        yield return new WaitForSeconds(stunTime);
        OnRecover?.Invoke();
        OnImmunityStart?.Invoke();
        yield return new WaitForSeconds(hitCooldown);
        IsImmune = false;
        OnImmunityEnd?.Invoke();
    }

}
