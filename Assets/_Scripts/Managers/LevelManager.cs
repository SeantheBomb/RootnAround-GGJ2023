using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public Leaderboard leaderboard;

    // Start is called before the first frame update
    void Start()
    {
        ObjectiveManager.OnCompleteLevel += OnCompleteLevel;
        TimeManager.OnTimeUp += LoadGameOver;
    }

    private void OnDestroy()
    {
        ObjectiveManager.OnCompleteLevel -= OnCompleteLevel;
        TimeManager.OnTimeUp -= LoadGameOver;
    }

    private void OnCompleteLevel(int level)
    {
        leaderboard.UpdateScore((int)(level * TimeManager.timeRemaining));
        LoadLevelComplete();
    }


    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevelComplete()
    {
        SceneManager.LoadScene("LevelComplete");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }


}
