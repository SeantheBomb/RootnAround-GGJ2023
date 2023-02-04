using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdRooting : MonoBehaviour
{

    public KeyCode rootKey = KeyCode.E;

    public bool IsRooting
    {
        get; protected set;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IsRooting = Input.GetKey(rootKey);
    }
}
