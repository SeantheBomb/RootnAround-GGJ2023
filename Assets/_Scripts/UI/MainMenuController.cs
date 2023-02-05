using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public static System.Action OnMenuStart;
    

    // Start is called before the first frame update
    void Start()
    {
        OnMenuStart?.Invoke();
    }


    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
}
