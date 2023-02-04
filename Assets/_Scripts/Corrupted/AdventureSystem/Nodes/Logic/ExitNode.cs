using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XNode;

/// <summary> Base node for the DialogueGraph system </summary>
[NodeWidth(100), NodeTint(100, 50, 50), CreateNodeMenu("Logic/Exit")]
public class ExitNode : GraphNode
{

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
        Debug.Log("Sequence: Reached Exit!", this);
        //view.activeGraph.timesVisited++;
        if(graph is SequenceGraph)
        {
            SequenceGraph ag = graph as SequenceGraph;
            ag.inProgress = false;
            view.OnSequenceComplete?.Invoke();
        }
        yield break;// return view.UpdateSequence(link);
    }

    public override void OnCreateConnection(NodePort from, NodePort to)
    {
        OnInputChanged();
    }
}
