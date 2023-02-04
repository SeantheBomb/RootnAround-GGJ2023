using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Corrupted;



[NodeWidth(250), NodeTint(122, 20, 20), CreateNodeMenu("SFX/Narrator")]

public class NarratorNode : GraphNode
{
    //public KeyVariable key;
    public AudioClip voiceClip;
    public bool awaitFinish = true;




    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        if(voiceClip == null)
        {
            Debug.LogError("Sequence: Missing clip!", this);
            PlayNextInPath(view);
            yield break;
        }
        Debug.Log("Sequence: Playing clip " + voiceClip.name);
        //NarratorAudio.PlayClip(voiceClip);
        if (awaitFinish)
            yield return new WaitForSeconds(voiceClip.length);
        Debug.Log("Sequence: Playing clip " + voiceClip.name);
        PlayNextInPath(view);
    }

    protected override void StopNode(SequenceSystemManager director)
    {
        Debug.Log("Sequence: Stop playing clip: " + voiceClip.name);
        //NarratorAudio.Stop();
    }

}
