using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;



[NodeWidth(100), NodeTint(100, 100, 100), CreateNodeMenu("Logic/Interrupt")]

public class InterruptNode : GraphNode
{

    //public GraphNode[] nodes;



    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        foreach(GraphNode node in path)
        {
            view.InterruptTask(node);
        }
        yield return null;
        //PlayNextInPath(view);
    }

}
