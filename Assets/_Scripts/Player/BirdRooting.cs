using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdRooting : MonoBehaviour
{

    public KeyCode rootKey = KeyCode.E;

    public FloatVariable rootTime = 3f;

    public bool IsRooting
    {
        get; protected set;
    }

    public bool IsCarrying
    {
        get; protected set;
    }

    public RootContainer target
    {
        get; protected set;
    }

    public RootItem item
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
        if (Input.GetKeyDown(rootKey))
        {
            StartRooting();
        }
        if (Input.GetKeyUp(rootKey))
        {
            StopRooting();
        }
    }

    void StartRooting()
    {
        if (item != null)
            return;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        if (hit.transform == null)
            return;
        RootContainer container = hit.transform.GetComponentInParent<RootContainer>();
        if (container == null)
            return;
        target = container;
        IsRooting = true;
        StartCoroutine(DoRooting());
    }

    void StopRooting()
    {
        target = null;
        IsRooting = false;
        StopAllCoroutines();
    }

    IEnumerator DoRooting()
    {
        yield return new WaitForSeconds(rootTime);
        item = target.SpawnItem();
        item.transform.position = transform.position;
        item.transform.SetParent(transform);
        StopRooting();
    }
}
