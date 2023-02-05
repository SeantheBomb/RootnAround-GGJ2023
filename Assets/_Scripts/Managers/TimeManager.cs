using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    /// <summary>
    /// Args totalTime, timeRemaining
    /// </summary>
    public static System.Action<float, float> OnTimeUpdate;

    public static System.Action OnTimeUp;

    public FloatVariable totalTime;
    public static float timeRemaining;

    public bool isRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        if (isRunning)
        {
            StartTimer();
        }
    }

    public void StartTimer(bool reset = true)
    {
        isRunning = true;
        timeRemaining = totalTime;
        StartCoroutine(TimerLoop());
    }

    public void StopTimer()
    {
        isRunning = false;
        StopAllCoroutines();
    }

    IEnumerator TimerLoop()
    {
        while(timeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining -= 1f;
            OnTimeUpdate?.Invoke(totalTime, timeRemaining);
        }
        OnTimeUp?.Invoke();
    }

}
