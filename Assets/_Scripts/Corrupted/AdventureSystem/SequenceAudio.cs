using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;


[RequireComponent(typeof(AudioSource))]
public class SequenceAudio : CorruptedBehaviour<SequenceAudio>
{
    public bool IsPlaying => audioS.isPlaying;


    AudioSource audioS;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        audioS = GetComponent<AudioSource>();
    }

    public void PlayClip()
    {
        audioS.Stop();
        audioS.Play();
    }

    public void PlayClip(AudioClip clip)
    {
        audioS.Stop();
        audioS.PlayOneShot(clip);
    }

    public void Stop()
    {
        audioS.Stop();
    }

    
}
