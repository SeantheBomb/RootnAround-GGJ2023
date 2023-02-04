using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Corrupted;



[NodeWidth(300), NodeTint(102, 0, 102), CreateNodeMenu("Play/Play Sequence")]

public class PlaySequenceNode : GraphNode
{

    public SequenceGraph Sequence;
    public bool await = true;
    Coroutine coroutine;




    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        view.UpdateSequence(Sequence.GetEntry());
        //coroutine = view.StartCoroutine(PlaySequence(view));
        //Debug.Log("Sequence: Awaiting flag " + flagKey);
        if (await)
            yield return new WaitUntil(() => Sequence.inProgress == false);//coroutine == null);
        //Debug.Log("Sequence: Awaiting flag complete " + flagKey);
        PlayNextInPath(view);
    }

    protected override void StopNode(SequenceSystemManager director)
    {
        director.StopSequence(Sequence);
    }

    IEnumerator PlaySequence(SequenceSystemManager view)
    {
        yield return null;
        view.UpdateSequence(Sequence.GetEntry());
        coroutine = null;
    }
}
