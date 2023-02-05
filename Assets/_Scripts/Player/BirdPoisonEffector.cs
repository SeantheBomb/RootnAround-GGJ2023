using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPoisonEffector : MonoBehaviour
{

    public static BirdPoisonEffector instance;

    public static PoisonEffect[] effects = new PoisonEffect[]
    {
        new HalfFlyHeightPoisonEffect()
    };

    public System.Action OnPoisonStart, OnPoisonStop;


    private void Start()
    {
        instance = this;
    }

    public void PoisonPlayer(PoisonEffect effect)
    {
        StartCoroutine(DoPoison(effect));
    }

    IEnumerator DoPoison(PoisonEffect effect)
    {
        OnPoisonStart?.Invoke();
        effect.StartEffect(this);
        yield return new WaitForSeconds(effect.Duration);
        effect.StopEffect(this);
        OnPoisonStop?.Invoke();
    }
    
}


