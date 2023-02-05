using Corrupted;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BirdDamage))]
public class PlayerVFXListener : MonoBehaviour
{

    public VFXType[] effects;

    BirdDamage bird;

    // Start is called before the first frame update
    void Start()
    {
        bird = GetComponent<BirdDamage>();
        DamageTrigger.OnDamage += OnTakeDamage;
        foreach(VFXType vfx in effects)
        {
            vfx.vfx.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        DamageTrigger.OnDamage -= OnTakeDamage;
    }

    private void OnTakeDamage(BirdDamage b, string key)
    {
        if (bird != b)
            return;
        if (string.IsNullOrWhiteSpace(key))
            return;
        foreach(VFXType vfx in effects)
        {
            if(vfx.key == key)
            {
                StartCoroutine(PlayerVFX(vfx.vfx, bird.stunTime));
                return;
            }
        }
    }

    private IEnumerator PlayerVFX(GameObject vfx, IntVariable stunTime)
    {
        vfx.SetActive(true);
        yield return new WaitForSeconds(stunTime);
        vfx.SetActive(false);
    }
}


[System.Serializable]
public class VFXType
{
    public StringVariable key;
    public GameObject vfx;
}
