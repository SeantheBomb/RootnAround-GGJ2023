using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Corrupted;


[RequireComponent(typeof(PlayableDirector))]
public class SequenceTimeline : CorruptedBehaviour<SequenceTimeline>
{
    public bool IsPlaying => director.state == PlayState.Playing;
    [SerializeField]
    PlayableAsset timeline;

    PlayableDirector director;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        director = GetComponent<PlayableDirector>();

    }
    public float GetDuration()
    {
        return (float)timeline.duration;
    }

    public void PlayTimeline()
    {
        director.Play(timeline);
    }

    
}
