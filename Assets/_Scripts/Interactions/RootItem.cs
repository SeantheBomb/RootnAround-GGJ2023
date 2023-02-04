using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootItem : MonoBehaviour
{

    [SerializeField] GameObject[] itemViews;


    // Start is called before the first frame update
    void Start()
    {
        RandomizeView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void RandomizeView()
    {
        SetView(Random.Range(0, itemViews.Length));
    }

    void SetView(int index)
    {
        for (int i = 0; i < itemViews.Length; i++)
        {
            itemViews[i].SetActive(i == index);
        }
    }
}
