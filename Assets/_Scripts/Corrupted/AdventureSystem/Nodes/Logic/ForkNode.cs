using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Corrupted;


[NodeWidth(50), NodeTint(100, 60, 60), CreateNodeMenu("Logic/Fork")]

public class ForkNode : GraphNode
{




    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        yield return null;
        //foreach(GraphNode gn in path)
        //{
        //    view.StartCoroutine(view.StartAndTrack(gn.PlayNode(view)));
        //}
    }

}
