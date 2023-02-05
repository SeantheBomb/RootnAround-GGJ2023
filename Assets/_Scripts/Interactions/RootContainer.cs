using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootContainer : MonoBehaviour
{

    public System.Action OnDepleted;

    public RootItem item;

    public int quantity = 1;

    public FloatVariable percentOfPoison = 0.1f;

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

    public void SetQuantity(int num)
    {
        quantity = num;
        if (quantity <= 0)
            OnDepleted?.Invoke();
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

        if(Random.Range(0f,1f) <= percentOfPoison)
        {
            BirdPoisonEffector.instance.PoisonPlayer(BirdPoisonEffector.effects[Random.Range(0, BirdPoisonEffector.effects.Length)]);
            Shout.Show("Oh no! You ate some rotten food...", 3f);
            return null;
        }

        quantity--;
        if (quantity == 0)
            OnDepleted?.Invoke();
        return Instantiate(item, transform.position, Quaternion.identity);
    }
}
