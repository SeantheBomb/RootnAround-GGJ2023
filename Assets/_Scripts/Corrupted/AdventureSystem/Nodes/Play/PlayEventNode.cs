using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;



[NodeWidth(300), NodeTint(100, 60, 160), CreateNodeMenu("Play/Play Event")]

public class PlayEventNode : GraphNode
{

    public string eventKey;
    public float waitBeforeContinue = 0;



    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        EventManager.PlayEvent(eventKey);
        if (waitBeforeContinue > 0)
            yield return new WaitForSeconds(waitBeforeContinue);
        PlayNextInPath(view);
    }

}
