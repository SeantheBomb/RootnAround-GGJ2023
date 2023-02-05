using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestManager : MonoBehaviour
{

    NestContainer[] nests;
    public NestContainer startNest;

    // Start is called before the first frame update
    void Start()
    {
        nests = FindObjectsOfType<NestContainer>();
        SetNest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetNest()
    {
        NestContainer nest;
        if(ObjectiveManager.level == 1 && startNest != null)
        {
            nest = startNest;
        }
        else
        {
            nest = nests[Random.Range(0, nests.Length)];
        }
        foreach(NestContainer n in nests)
        {
            n.gameObject.SetActive(n == nest);
        }
    }
}
