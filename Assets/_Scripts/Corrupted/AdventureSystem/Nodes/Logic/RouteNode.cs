using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;



[NodeWidth(20), NodeTint(100, 100, 100), CreateNodeMenu("Logic/Route")]

public class RouteNode : GraphNode
{





    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        yield return null;
        PlayNextInPath(view);
    }

}
