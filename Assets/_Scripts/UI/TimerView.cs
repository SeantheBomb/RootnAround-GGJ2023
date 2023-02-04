using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class TimerView : MonoBehaviour
{

    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        TimeManager.OnTimeUpdate += UpdateTimer;
        slider.maxValue = 1;
        slider.value = 1;
    }

    private void UpdateTimer(float totalTime, float timeRemaining)
    {
        slider.value = timeRemaining / totalTime;
    }
}
