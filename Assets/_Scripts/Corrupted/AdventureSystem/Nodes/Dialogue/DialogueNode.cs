using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeWidth(200), NodeTint(100, 70, 70), CreateNodeMenu("Dialogue/Dialgoue")]
public class DialogueNode : GraphNode
{
    //[Input] public GraphNode input;
    [TextArea]
    public string dialogue;
    public AudioClip voiceover;
    public Expression expression;



    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        view.DoBehaviour((DialogueView view) => {
            view.SetText(dialogue);
            view.SetCurrent(this);
            if (view.OnDialogue != null) view.OnDialogue(this);
        });
        
        yield return new WaitForSeconds(voiceover != null ? voiceover.length : 5f);
        //if (providesTestimony != false && providesTestimony.hasCollected == false)
        //{
        //    providesTestimony.hasCollected = true;
        //}
        PlayNextInPath(view);
    }

}
