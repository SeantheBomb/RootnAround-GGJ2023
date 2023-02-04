using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootContainer : MonoBehaviour
{

    public RootItem item;

    public int quantity = 1;

    public bool depleted => quantity <= 0;


    private void Start()
    {
        if (item == null)
            quantity = 0;
    }

    public RootItem SpawnItem()
    {
        if (item == null)
        {
            quantity = 0;
            return null;
        }
        if (depleted)
        {
            return null;
        }
        quantity--;
        return Instantiate(item, transform.position, Quaternion.identity);
    }
}
