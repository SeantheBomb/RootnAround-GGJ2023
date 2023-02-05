using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{

    public AudioClip menuMusic, gameMusic;

    AudioSource audio;


    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = menuMusic;
        audio.loop = true;
        audio.Play();
        SceneManager.sceneLoaded += OnSceneLoad;
        MainMenuController.OnMenuStart += OnMenuStart;
    }

    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == "GameScene")
        {
            if (audio.clip == gameMusic)
                return;
            StartGameScene();
            //SceneManager.sceneLoaded -= OnSceneLoad;
        }
    }

    void StartGameScene()
    {
        audio.clip = gameMusic;
        audio.Play();
    }

    void OnMenuStart()
    {
        audio.clip = menuMusic;
        audio.Play();
    }

    
}
