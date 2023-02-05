using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "Leaderboad", menuName = "Leaderboard")]
public class Leaderboard : ScriptableObject
{

    public PlayerScore active;
    public List<PlayerScore> scores;

    public void UpdateScore(int points)
    {
        if (active == null)
            active = new PlayerScore();
        active.score += points;
    }

    public void CompleteSession(string name = "AAA")
    {
        active.name = name;
        scores.Add(active);
        active = new PlayerScore();
    }

    public List<PlayerScore> GetHighscores()
    {
        return scores.OrderByDescending((PlayerScore s) => s.score).ToList();
    }

    public List<PlayerScore> GetHighscores(int count)
    {
        return scores.OrderByDescending((PlayerScore s) => s.score).Take(count).ToList();
    }

    //public PlayerScore GetActivePlayer(string player = null)
    //{
    //    foreach(PlayerScore s in scores)
    //    {
    //        if (s.sessionComplete)
    //            continue;
    //        if (player == null || s.name == player)
    //            return s;
    //    }
    //    return null;
    //}

    //public int GetActivePlayerIndex(string player = null)
    //{
    //    for (int i = 0; i < scores.Count; i++)
    //    {
    //        PlayerScore s = scores[i];
    //        if (s.sessionComplete)
    //            continue;
    //        if (player == null || s.name == player)
    //            return i;
    //    }
    //    return -1;
    //}
}

[System.Serializable]
public class PlayerScore
{
    public string name;
    public int score;
    //public bool sessionComplete;
}
