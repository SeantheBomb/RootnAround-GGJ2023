//using Corrupted.XR;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(200), NodeTint(100, 20, 20), CreateNodeMenu("Debug/Skip")]
public class DebugSkipToNode : GraphNode
{

    //static DebugSkipToNode activeNode;
    static bool buttonPressed = false;

    [Output]
    public GraphNode interrupt;



    // Checkpoint checkpoint;

    public override IEnumerator PlayNode(SequenceSystemManager director)
    {
        //activeNode = this;
        //Debug.Log($"Sequence: Checkpint {checkpoint.name} completed!!");
        yield return new WaitUntil(()=>buttonPressed);
        buttonPressed = false;
        //director.StopCoroutines(this);
        foreach (GraphNode gn in GetPath("interrupt"))
            director.InterruptTask(gn);
        PlayNextInPath(director);
    }



    //private static void OnDebugPressed(XRInput input, XRControllerInput button)
    //{
    //    if (button == XRControllerInput.SecondaryBtn)
    //        buttonPressed = true;
    //}

    [Button]
    public void PressDebug()
    {
        buttonPressed = true;
    }


}