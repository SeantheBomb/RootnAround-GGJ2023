using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


[CreateAssetMenu(fileName = "DialogueGraph", menuName = "Corrupted/Sequence/Graph", order = 1)]
public class SequenceGraph : NodeGraph
{

    public int timesVisited = 0;

    public bool inProgress;

    public GraphNode GetEntry()
    {
        EntryNode initial = null;
        bool exactFound = false;
        //List<EntryNode> nodes = new List<EntryNode>();
        EntryNode highestVisit = null;
        foreach (Node n in nodes)
        {
            if (n is EntryNode)
            {
                //Debug.Log("Sequence: Entry node detected!", n);
                EntryNode en = (EntryNode)n;
                if (en.visit == timesVisited)
                {
                    exactFound = true;
                    initial = en;
                    break;
                }
                else if (highestVisit == null || en.visit > highestVisit.visit)
                {
                    highestVisit = en;
                }
            }
            //else
            //{
            //    Debug.Log("Sequence: Non-entry node detected", n);
            //}
        }
        if (initial != null && exactFound)
        {//We found our exact entry
            //UpdateView(initial.link);
            Debug.Log("Sequence: Exact Entry Found", initial);
            return initial;
            //coroutine = StartCoroutine(UpdateSequence(initial));
        }
        else if (highestVisit != null)//We haven't found our exact entry so let's use the highest count
        {
            //UpdateView(highestVisit.link);
            initial = highestVisit;
            Debug.Log("Sequence: Exact Entry Not Found, using highest...", initial);
            return initial;
            //coroutine = StartCoroutine(UpdateSequence((GraphNode)initial));
        }
        else
        {
            Debug.LogError("Sequence: Failed to find entry node... please ensure one exists in your dialogue graph!");
            return null;
        }
    }


}
