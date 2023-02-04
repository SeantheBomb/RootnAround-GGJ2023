using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{

    public static System.Action<int> OnStartLevel, OnCompleteLevel;
    public static ObjectiveManager instance;

    public int level; 


    // Start is called before the first frame update
    IEnumerator Start()
    {
        instance = this;
        yield return null;
        StartLevel(1);
    }

    public void StartLevel()
    {
        OnStartLevel?.Invoke(level);
    }

    public void StartLevel(int i)
    {
        level = i;
        StartLevel();
    }

    public void CompleteLevel()
    {
        OnCompleteLevel?.Invoke(level);
        level++;
    }
}
