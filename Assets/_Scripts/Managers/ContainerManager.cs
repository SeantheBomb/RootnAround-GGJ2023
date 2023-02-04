using Corrupted;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{

    static List<RootContainer> containers = new List<RootContainer>();

    public IntVariable minItems = 1;
    public IntVariable maxItems = 30;
    public IntVariable maxItemsPerContainer = 3;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;
        DistributeItemsAcrossContainers();
    }


    public void DistributeItemsAcrossContainers()
    {
        Distribute(minItems, maxItems, maxItemsPerContainer, containers.ToArray());
    }



    public static void AddContainer(RootContainer c)
    {
        containers.Add(c);
    }

    public static void RemoveContainer(RootContainer c)
    {
        containers.Remove(c);
    }

    public static void Distribute(int min, int total, int perContainer, RootContainer[] containers)
    {
        int spawned = 0;
        List<RootContainer> toFill = containers.ToList();
        while(total > 0 && toFill.Count > 0)
        {
            RootContainer c = toFill[Random.Range(0, toFill.Count)];
            toFill.Remove(c);
            int quantity = Random.Range(spawned < min ? 1 : 0, perContainer);
            total -= quantity;
            spawned += quantity;
            c.quantity = quantity;
        }
    }
}
