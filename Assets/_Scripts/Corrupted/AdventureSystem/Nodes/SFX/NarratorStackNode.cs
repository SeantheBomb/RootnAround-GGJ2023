using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Corrupted;



[NodeWidth(250), NodeTint(122, 20, 20), CreateNodeMenu("SFX/NarratorStack")]

public class NarratorStackNode : GraphNode
{
    //public KeyVariable key;
    public AudioClip[] voiceClip;
    public bool awaitFinish = true;




    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        if (awaitFinish == false)//If we're not awaiting finish, lets go ahead and start the next tasks
            PlayNextInPath(view);
        foreach (AudioClip clip in voiceClip)
        {
            if(clip == null)
            {
                Debug.LogError("Sequence: Missing clip!", this);
                continue;
            }
            Debug.Log("Sequence: Playing clip " + clip.name);
            //NarratorAudio.PlayClip(clip);
            yield return new WaitForSeconds(clip.length);
            //Debug.Log("Sequence: Playing clip " + clip.name);
        }
        if(awaitFinish)//If we're awaiting finish, let's start the next tasks now that we're done
            PlayNextInPath(view);
    }

    protected override void StopNode(SequenceSystemManager director)
    {
        //Debug.Log("Sequence: Stop playing clip: " + voiceClip.name);
        //NarratorAudio.Stop();
    }

}
