using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using System.Linq;

[NodeWidth(150), NodeTint(70, 70, 100)]
public class WalkNode : GraphNode
{
    //[Input] public GraphNode input;
    public int walkToIndex;
    public int maxSecondsWait = 10;
    bool hasReachedDest = false;
    //[Output] public GraphNode output;




    protected override void Init()
    {
        base.Init();
        output = this;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        hasReachedDest = false;
        //if(view.OnWalk != null)view.OnWalk(this);
        int time = maxSecondsWait;
        while(time > 0 && hasReachedDest == false)
        {
            time--;
            //Debug.Log("Exiting Loop in " + time);
            yield return new WaitForSeconds(1);
        }
        //Debug.Log("Loop exited");
        PlayNextInPath(view);
    }

    public void DestReached()
    {
        hasReachedDest = true;
        //Debug.Log("DestReached");
    }

    public override object GetValue(NodePort port)
    {
        return output;
    }
}
