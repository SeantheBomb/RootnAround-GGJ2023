using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootContainer : MonoBehaviour
{

    public System.Action OnDepleted;

    public RootItem item;

    public int quantity = 1;

    public bool depleted => quantity <= 0;


    private void Start()
    {
        if (item == null)
            quantity = 0;
        ContainerManager.AddContainer(this);
    }

    private void OnDestroy()
    {
        ContainerManager.RemoveContainer(this);
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
        if (quantity == 0)
            OnDepleted?.Invoke();
        return Instantiate(item, transform.position, Quaternion.identity);
    }
}
