using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveView : MonoBehaviour
{

    public TMP_Text foodOutput, eggOutput;


    // Start is called before the first frame update
    void Start()
    {
        NestContainer.OnAddItem += UpdateFoodOutput;
        ObjectiveManager.OnStartLevel += UpdateEggOutput;
    }

    private void OnDestroy()
    {
        NestContainer.OnAddItem -= UpdateFoodOutput;
        ObjectiveManager.OnStartLevel -= UpdateEggOutput;
    }

    private void UpdateFoodOutput(int food)
    {
        foodOutput.text = food.ToString().PadLeft(2, '0');
    }

    private void UpdateEggOutput(int eggs)
    {
        eggOutput.text = eggs.ToString().PadLeft(2, '0');
    }


}
