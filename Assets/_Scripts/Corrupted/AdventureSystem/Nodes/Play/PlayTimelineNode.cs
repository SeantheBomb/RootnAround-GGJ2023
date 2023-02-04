using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using XNode;
using Corrupted;



[NodeWidth(200), NodeTint(165, 100, 0), CreateNodeMenu("Play/Play Timeline")]

public class PlayTimelineNode : GraphNode
{

    public KeyVariable timelineKey;
    public bool blink = true;
    public float blinkLength = 1.2f;
    Coroutine coroutine;




    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        SequenceTimeline.GetInstance(timelineKey).PlayTimeline();
        Debug.Log("Sequence: Awaiting timeline " + timelineKey);
        if (blink)
        {
            //Blink.OpenEyes();
            yield return new WaitForSeconds(SequenceTimeline.GetInstance(timelineKey).GetDuration() - blinkLength);
            //Blink.CloseEyes();
        }
        yield return new WaitUntil(() => SequenceTimeline.GetInstance(timelineKey).IsPlaying == false);//coroutine == null);
        Debug.Log("Sequence: Awaiting timeline complete " + timelineKey);
        PlayNextInPath(view);
    }
}
