using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using System.Linq;

[NodeWidth(190), NodeTint(70, 100, 70), CreateNodeMenu("Dialogue/Response")]
public class ResponseNode : GraphNode
{
    //[Input] public DialogueNode input;
    [TextArea(1, 2)]
    public string response;
    //public Testimony requireTestimony;
    //[Output] public ResponseNode output;
    


    protected override void Init()
    {
        base.Init();
        output = this;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        //if (requireTestimony != null && requireTestimony.hasCollected == false)
        //{
        //    yield return null;
        //}
        //else
        //{
        //bool awaitInput = false;
            view.DoBehaviour((DialogueView dv) =>
            {
                dv.AddResponse(response, () =>
                 {
                     Debug.Log("ResponseNode: Button pressed");
                     PlayNextInPath(view);
                 });
            });
            yield return null;
       // }
    }

    //public override void OnChangeClear()
    //{
    //    response.path = null;
    //    //DialogueNode[] newInput = GetPort("input").GetInputValues<DialogueNode>();
    //    //foreach (DialogueNode dn in newInput)
    //    //{
    //    //    dn.dialogue.AddResponse(response);
    //    //}
    //}

    //public override void OnChangeRedraw()
    //{
    //    //response.path = null;
    //    DialogueNode newPath = GetPort("output").GetInputValue<DialogueNode>();
    //    response.path = newPath.dialogue;
    //    Debug.Log(name + " added to " + newPath.name);
    //}

}
