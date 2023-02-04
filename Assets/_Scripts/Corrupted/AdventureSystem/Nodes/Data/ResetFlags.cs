using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Corrupted;


[NodeWidth(200), NodeTint(50, 50, 90), CreateNodeMenu("Data/ResetFlags")]
public class ResetFlags : GraphNode
{



    public FlagValue[] flags;
    public bool value;




    public override IEnumerator PlayNode(SequenceSystemManager director)
    {
        foreach(FlagValue f in flags)
        {
            f.Value = value;
        }
        yield return null;
        PlayNextInPath(director);
    }

}