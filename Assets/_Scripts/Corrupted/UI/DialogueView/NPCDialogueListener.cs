using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider))]
public class NPCDialogueListener : CorruptedBehaviour<NPCDialogueListener>
{

    public DialogueView dialogue;
    Animator anim;
    AudioSource audio;
    bool isInTrigger = false;

    public override void Start()
    {
        base.Start();
        if(dialogue)
        dialogue.OnDialogue += SendTrigger;
        GetRefs();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        if(dialogue)
        dialogue.OnDialogue -= SendTrigger;
    }

    void GetRefs()
    {
        if(anim == null) anim = GetComponent<Animator>();
        if(audio == null) audio = GetComponent<AudioSource>();
    }

    void SendTrigger(DialogueNode d)
    {
        if (enabled == false)
            return;
        GetRefs();
        Debug.Log("Play animation " + d.expression.name);
        anim.CrossFade(d.expression.name, .1f,0);
        if (d.voiceover != null)
        {
            audio.clip = d.voiceover;
            audio.Play();
        }
    }

    public void SkipDialogue()
    {
        dialogue.SkipDialgoue();
    }

    public void StartWalk()
    {
        anim.SetBool("IsWalking", true);
        //dialogue.isBusy = true;
    }

    public void StopWalk()
    {
        anim.SetBool("IsWalking", false);
        //dialogue.isBusy = false;
    }

    private void OnDisable()
    {
        if (isInTrigger)
        {
            dialogue.OnPlayerExit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.activeSelf == false)
            return;
        Debug.Log("Enter: " + other.name);
        if (other.CompareTag("Player"))
        {
            if(dialogue)
            dialogue.OnPlayerEnter();
            isInTrigger = true;
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (isInTrigger)
    //        return;
    //    OnTriggerEnter(other);
    //}

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Enter: " + other.name);
        if (other.CompareTag("Player"))
        {
            if(dialogue)
            dialogue.OnPlayerExit();
            audio.Stop();
            isInTrigger = false;
        }
    }
}
