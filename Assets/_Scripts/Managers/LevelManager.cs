using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
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

    private void OnCompleteLevel(int obj)
    {
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
