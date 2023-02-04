using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(200), NodeTint(100, 50, 50), CreateNodeMenu("Actions/InterruptSequence")]
public class InterruptSequenceNode : GraphNode {


    public bool innerGraphOnly = true;

    public override IEnumerator PlayNode(SequenceSystemManager director)
    {
        if (innerGraphOnly)
            director.StopSequence(graph as SequenceGraph);
        else
            director.StopSequence();
        yield return null;
        PlayNextInPath(director);
    }
}