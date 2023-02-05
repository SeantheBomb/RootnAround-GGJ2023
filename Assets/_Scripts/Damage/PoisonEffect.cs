using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoisonEffect 
{

    public abstract float Duration { get; }

    public abstract void StartEffect(BirdPoisonEffector effector);

    public abstract void StopEffect(BirdPoisonEffector effector);
}
