using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class Text_ShowLeaderboard : MonoBehaviour
{

    public Leaderboard leaderboard;
    public int padSize = 6;
    public int maxCount = 10;

    TMP_Text output;

    // Start is called before the first frame update
    void Start()
    {
        output = GetComponent<TMP_Text>();
        output.text = "";
        int count = 1;
        foreach (PlayerScore ps in leaderboard.GetHighscores(maxCount))
        {
            output.text += count++ + ") ";
            output.text += ps.name + ".....";
            output.text += ps.score.ToString().PadLeft(padSize, '0');
            output.text += "\n";
        }
    }

}
