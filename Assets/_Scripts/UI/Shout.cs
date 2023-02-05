using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class Shout : MonoBehaviour
{

    static Shout instance;

    TMP_Text output;

    // Start is called before the first frame update
    void Start()
    {
        output = GetComponent<TMP_Text>();
        output.text = "";
        instance = this;
    }

    void OnShout(string text, float duration)
    {
        StopAllCoroutines();
        output.text = text;
        StartCoroutine(ClearShout(duration));
    }

    IEnumerator ClearShout(float duration)
    {
        yield return new WaitForSeconds(duration);
        output.text = "";
    }


    public static void Show(string text, float duration = 3f)
    {
        instance.OnShout(text, duration);
    }


}
