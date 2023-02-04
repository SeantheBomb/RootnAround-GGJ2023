using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Corrupted;


[NodeWidth(200), NodeTint(100, 60, 60), CreateNodeMenu("Logic/Branch")]

public class BranchNode : GraphNode
{

    [Output] public GraphNode fail;

    public FlagValue flag;



    private void OnValidate()
    {
        name = flag.ToString();
    }

    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        yield return null;
        if (flag.Value)
        {
            Debug.Log("Sequence: " + flag.name + " is collected.");
            PlayNextFromPort(view, "output");
            //view.UpdateSequence(GetLink("output"));
        }
        else 
        {
            Debug.Log("Sequence: " + flag.name + " is not collected.");
            PlayNextFromPort(view, "fail");
            //view.UpdateSequence(GetLink("fail"));
        }
    }

}
