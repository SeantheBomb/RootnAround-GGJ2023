using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using System.Linq;

[NodeWidth(200), NodeTint(100, 60, 60)]
public class PlayOnceDialogueNode : DialogueNode
{

    bool hasPlayed = false;

    protected override void Init()
    {
        base.Init();
        hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        if (hasPlayed && link != null)
        {
            Debug.Log("Skipping " + name + " has been played.");
            PlayNextInPath(view);
        }
        else
        {
            hasPlayed = true;
            Debug.Log(name + " has been played.");
            yield return base.PlayNode(view);
        }
    }

}
