using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Corrupted;



[NodeWidth(200), NodeTint(102, 0, 102), CreateNodeMenu("Awaits/Flag")]

public class AwaitFlagNode : GraphNode
{

    public FlagValue flag;
    public bool invert = false;
    public bool toggleOnReach = false;




    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        Debug.Log($"Sequence: Awaiting flag {flag.name} with value {flag.Value} to equal {!invert}");
        yield return new WaitUntil(() => flag.Value != invert);
        if (toggleOnReach)
            flag.Toggle();
        Debug.Log($"Sequence: Awaiting flag complete {flag.name} has value {flag.Value}");
        PlayNextInPath(view);
    }

    void OnValidate()
    {
        if (flag == null)
            return;
        name = "Await " + flag.name;
    }

}
