using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Corrupted;



[NodeWidth(200), NodeTint(102, 0, 102), CreateNodeMenu("Awaits/Flags")]

public class AwaitFlagsNode : GraphNode
{

    public FlagGate[] flags;
    public bool requireAll;




    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        //Debug.Log($"Sequence: Awaiting flag {flag.name} with value {flag.Value} to equal {!invert}");
        if (requireAll)
        {
            yield return new WaitUntil(() => AndEval(flags));
        }
        else
        {
            yield return new WaitUntil(() => OrEval(flags));
        }
        //Debug.Log($"Sequence: Awaiting flag complete {flag.name} has value {flag.Value}");
        PlayNextInPath(view);
    }

    /// <summary>
    /// Tests for falseness, returns false if any of the flags evaluate to false
    /// </summary>
    /// <param name="flags"></param>
    /// <returns></returns>
    public bool AndEval(FlagGate[] flags)
    {
        foreach(FlagGate f in flags)
        {
            if (f.isTrue == false)
                return false;
        }
        return true;
    }

    /// <summary>
    /// Tests for trueness, returns true if any of the flags evaluate to true
    /// </summary>
    /// <param name="flags"></param>
    /// <returns></returns>
    public bool OrEval(FlagGate[] flags)
    {
        foreach (FlagGate f in flags)
        {
            if (f.isTrue)
                return true;
        }
        return false;
    }

    [System.Serializable]
    public struct FlagGate
    {
        public FlagValue flag;
        public bool invert;
        //public bool toggleOnReach;

        public bool isTrue => flag.Value != invert;
    }

}
