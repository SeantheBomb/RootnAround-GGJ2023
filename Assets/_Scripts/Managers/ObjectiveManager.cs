using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{

    public static System.Action<int> OnStartLevel, OnCompleteLevel;
    public static ObjectiveManager instance;

    public static int level = 1; 


    // Start is called before the first frame update
    IEnumerator Start()
    {
        instance = this;
        yield return null;
        StartLevel(level);
        NestContainer.OnAddItem += CheckLevelComplete;

        yield return new WaitForSeconds(1f);

        Shout.Show("Bring food to your nest before your eggs hatch!!!", 5f);
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

    public void CheckLevelComplete(int i)
    {
        if(i >= level)
        {
            CompleteLevel();
        }
    }

    public void CompleteLevel()
    {
        OnCompleteLevel?.Invoke(level);
        level++;
    }
}
