using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeWidth(200), NodeTint(80,100,80), CreateNodeMenu("Dialogue/FlagResponse")]
public class FlagResponse : ResponseNode
{
    public FlagValue requiredFlag;
    public BoolVariable inverse;

    public bool isAvailable => requiredFlag.Value != inverse;

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        Debug.Log("Sequence: FlagResponse value " + requiredFlag.name + " is " + isAvailable);
        if(isAvailable)
            yield return base.PlayNode(view);
    }
}
