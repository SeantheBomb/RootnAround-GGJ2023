using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Corrupted;
//using Corrupted.XR;


[NodeWidth(200), NodeTint(80, 40, 40), CreateNodeMenu("Objects/Move Object (wip)")]

public class MoveObjectNode : GraphNode
{

    public KeyVariable objectKey;
    public KeyVariable portalKey;



    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        if (DynamicObjectIndex.HasObject(objectKey))
        {
            //Eventually
            DynamicObjectIndex.GetObject(objectKey).transform.position = DynamicObjectIndex.GetObject(portalKey).transform.position;
            DynamicObjectIndex.GetObject(objectKey).transform.rotation = DynamicObjectIndex.GetObject(portalKey).transform.rotation;
        }
        else
        {
            Debug.LogError("Sequence: Can not move object because it does not exist! Key: " + objectKey);
        }
        yield return null;
        PlayNextInPath(view);
    }

}
