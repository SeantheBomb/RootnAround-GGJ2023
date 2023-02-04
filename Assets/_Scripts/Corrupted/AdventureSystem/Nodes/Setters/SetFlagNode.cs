using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Corrupted;


[NodeWidth(200), NodeTint(100, 160, 60), CreateNodeMenu("Data/SetFlag")]

public class SetFlagNode : GraphNode
{

    public FlagValue flag;
    public BoolVariable value;




    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        flag.SetValue(value);
        yield return null;
        PlayNextInPath(view);
    }

}
