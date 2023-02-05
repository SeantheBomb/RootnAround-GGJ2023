using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithScreenSpace : MonoBehaviour
{

    [SerializeField]Vector2 size;

    void Start()
    {
        size = new Vector2(Screen.width / transform.localScale.x, Screen.height / transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {


        transform.localScale = new Vector2(Screen.width/size.x, Screen.height/size.y);
    }
}
