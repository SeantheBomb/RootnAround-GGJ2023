using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XNode;

/// <summary> Base node for the DialogueGraph system </summary>
[NodeWidth(100), NodeTint(50, 100, 50), CreateNodeMenu("Logic/Entry")]
public class EntryNode : GraphNode
{
    public int visit = 0;

    //[Output] public DialogueNode output;

    public GraphNode[] path
    {
        get
        {
            return new List<GraphNode>(GetPort("output").GetInputValues<GraphNode>()).ToArray();
        }
    }





    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        Debug.Log("Sequence: Visited entry node to " + link, this);
        view.activeGraph.timesVisited++;
        if (graph is SequenceGraph)
        {
            SequenceGraph ag = graph as SequenceGraph;
            ag.inProgress = true;
        }
        yield return null;
        PlayNextInPath(view);// view.UpdateSequence(link);
    }

    public override void OnCreateConnection(NodePort from, NodePort to)
    {
        OnInputChanged();
    }
}
