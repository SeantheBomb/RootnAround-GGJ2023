using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfFlyHeightPoisonEffect : PoisonEffect
{
    public override float Duration => 10f;

    public override void StartEffect(BirdPoisonEffector effector)
    {
        BirdMovement bm = effector.GetComponent<BirdMovement>();
        bm.jumpModifier = 0.5f;
        Debug.Log("Poison: Set player half jump");
    }

    public override void StopEffect(BirdPoisonEffector effector)
    {
        BirdMovement bm = effector.GetComponent<BirdMovement>();
        bm.jumpModifier = 1f;
        Debug.Log("Poison: Set player half jump");
    }
}
