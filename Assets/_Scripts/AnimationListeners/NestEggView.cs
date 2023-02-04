using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NestEggView : MonoBehaviour
{

    public Sprite[] views;

    public IntVariable count;

    public FloatVariable delay;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SetView(count);
    }

    public void SetView(int count)
    {
        if (count < 0 || count >= views.Length)
        {
            Debug.LogError("Nest: Can not set view outside length");
            return;
        }
        if (delay <= 0)
        {
            this.count = count;
            sr.sprite = views[count];
        }
        else
        {
            StartCoroutine(LerpView(count));
        }
    }

    IEnumerator LerpView(int count)
    {
        int iter = (int)Mathf.Sign(count - this.count);
        for (int i = 0; i <= count; i ++)
        {
            sr.sprite = views[i];
            //Debug.Log("Nest: Set view " + i);
            yield return new WaitForSeconds(delay);
        }
        this.count = count;
    }
}
