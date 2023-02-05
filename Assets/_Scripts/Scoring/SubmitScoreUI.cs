using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SubmitScoreUI : MonoBehaviour
{

    public TMP_InputField inputField;
    public Leaderboard leaderboard;

   public void Submit()
    {
        string name = inputField.text;
        if (string.IsNullOrWhiteSpace(name))
        {
            name = "AAA";
        }
        leaderboard.CompleteSession(name);
    }
}
