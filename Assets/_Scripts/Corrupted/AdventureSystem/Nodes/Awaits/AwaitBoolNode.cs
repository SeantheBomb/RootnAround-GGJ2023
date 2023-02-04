using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Corrupted;



[NodeWidth(200), NodeTint(51, 0, 102), CreateNodeMenu("Awaits/Bool")]

public class AwaitBoolNode : GraphNode
{

    public BoolVariable flag;
    public bool invert = false;




    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        //Debug.Log("Sequence: Awaiting flag " + flag);
        yield return new WaitUntil(() => flag != invert);
        //Debug.Log("Sequence: Awaiting flag complete " + flagKey);
        PlayNextInPath(view);
    }

}
