using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Corrupted;



[NodeWidth(250), NodeTint(51, 0, 102), CreateNodeMenu("Awaits/Event")]

public class AwaitEventNode : GraphNode
{

    public CorruptedEvent eventFlag;
    bool eventHappened = false;



    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        eventFlag.RegisterListener(OnEvent);
        Debug.Log("Sequence: Awaiting event " + eventFlag.name);
        yield return new WaitUntil(() => eventHappened);
        eventFlag.UnRegisterListener(OnEvent);
        eventHappened = false;
        //Debug.Log("Sequence: Awaiting flag complete " + flagKey);
        PlayNextInPath(view);
    }

    void OnEvent()
    {
        eventHappened = true;
        Debug.Log($"Sequence: Event {eventFlag.name} happened.");
    }

    void OnValidate()
    {
        if (eventFlag == null)
            return;
        name = "Await " + eventFlag.name;
    }

}
