using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Corrupted;



[NodeWidth(200), NodeTint(60, 90, 30), CreateNodeMenu("Objects/Set Active")]

public class SetActiveNode : GraphNode
{

    public KeyVariable objectKey;
    public bool isActive = false;



    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        if (DynamicObjectIndex.HasObject(objectKey))
        {
            DynamicObjectIndex.GetObject(objectKey).SetActive(isActive);
            Debug.Log("trust me, I tried!");
        }
        else
        {
            Debug.LogError("Sequence: Can not set object active because it does not exist! Key: " + objectKey);
        }
        yield return null;
        PlayNextInPath(view);
    }

}
