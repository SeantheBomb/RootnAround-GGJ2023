using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SequenceSystemManager))]
public abstract class SequenceBehaviour : CorruptedBehaviour<SequenceBehaviour>
{
    SequenceSystemManager _sequence;
    public SequenceSystemManager sequence
    {
        get
        {
            if (_sequence == null) _sequence = GetComponent<SequenceSystemManager>();
            return _sequence;
        }
    }

    public bool isBusy => sequence.inProgress;

    public virtual void OnEnable()
    {
        sequence.behaviours.Add(this);
    }

    public virtual void OnDisable()
    {
        sequence.behaviours.Remove(this);
    }
    

    protected virtual void OnValidate()
    {
        instanceKey = sequence.instanceKey;
    }

}
