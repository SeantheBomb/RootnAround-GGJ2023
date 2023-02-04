using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueView : SequenceBehaviour
{
    const int maxResponse = 8;

    const float dialogueTime = 5f;

    public static System.Action ResetAllViews;

    public System.Action<DialogueNode> OnDialogue;

    int activeResponses = 0;

    DialogueNode current;
    //DialogueNode next;

    public TMP_Text output;
    public DialogueButton[] buttons;

  

    Transform canvas;


    public void SetText(string t)
    {
        output.text = t;
        ClearResponses();
    }

    public void SetCurrent(DialogueNode node)
    {
        current = node;
    }

    public override void Start()
    {
        base.Start();
        //sequence.PlaySequence();
        gameObject.SetActive(false);
        sequence.OnSequenceComplete += OnPlayerExit;
    }


    public void AddResponse(string response, System.Action action)
    {
        if (activeResponses < maxResponse)
        {
            buttons[activeResponses].UpdateButton(response, action);
            buttons[activeResponses].SetActive(true);
            activeResponses++;
        }
    }

    public void SkipDialgoue()
    {
        sequence.SkipCurrent();
    }

    public void ClearResponses()
    {
        activeResponses = 0;
        foreach (DialogueButton b in buttons)
        {
            b.SetActive(false);
        }
    }

    public void OnPlayerEnter()
    {
        if (sequence.inProgress)
            return;
        if (gameObject.activeSelf)
        {
            Debug.LogError("DialogueView: Dialogue start called when its already in session!");
            return;
        }
        gameObject.SetActive(true);
        Debug.Log("DialogueView: OnPlayerEnter");
        sequence.PlaySequence();
        //PlayerAudioHandle._Instance.SelectSnapShot(Snapshot.TalkingToNPC, .5f);
    }

    public void OnPlayerExit()
    {
        //Debug.Log(name + " stop all coroutines!");
        sequence.StopSequence();
        gameObject.SetActive(false);
        //PlayerAudioHandle._Instance.SelectSnapShot(Snapshot.Main, .5f);
    }
}
