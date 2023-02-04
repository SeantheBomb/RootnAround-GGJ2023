using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;



[NodeWidth(300), NodeTint(30, 30, 30), CreateNodeMenu("Logic/Comment")]

public class CommentNode : GraphNode
{

    public string comment;




    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        Debug.Log("Sequence: " + comment);
        PlayNextInPath(view);
        yield return null;
    }

}
