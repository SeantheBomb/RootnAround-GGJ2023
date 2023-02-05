using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(TMP_Text))]
public class Text_ShowScore : MonoBehaviour
{

    public Leaderboard leaderboard;
    public int padSize = 6;

    TMP_Text output;

    // Start is called before the first frame update
    void Start()
    {
        output = GetComponent<TMP_Text>();
        output.text = leaderboard.active.score.ToString().PadLeft(padSize, '0');
    }

}
