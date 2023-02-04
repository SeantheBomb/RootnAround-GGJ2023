using Corrupted;
using System.Collections.Generic;
using UnityEngine;

public class SequenceSystemManager : CorruptedBehaviour<SequenceSystemManager>
{

    public System.Action OnSequenceComplete;

   

    public SequenceGraph activeGraph;

    public bool playOnAwake;

    public bool inProgress
    {
        get
        {
            return current != null;
        }
    }
    public GraphNode current
    {
        get; protected set;
    }

    public List<SequenceBehaviour> behaviours;




    //Coroutine coroutine;
    Dictionary<GraphNode, Coroutine> innerCoroutines;



    private void OnDisable()
    {
        StopSequence(activeGraph);
    }


    public void OnEnable()
    {
        if (innerCoroutines == null)
            innerCoroutines = new Dictionary<GraphNode, Coroutine>();
        if (playOnAwake)
        {
            PlaySequence();
        }
    }

    public void UpdateSequence(GraphNode node)
    {
        if (node == null)
        {
            Debug.LogError("DialogueView: Null node received!", gameObject);
        }
        else
        {
            node.RunTask(this);
        }

    }


    public void PlaySequence()
    {
        if (inProgress)
        {
            Resume();
            return;
        }
        UpdateSequence(activeGraph.GetEntry());
    }

    public void StopSequence(SequenceGraph graph)
    {
        foreach(GraphNode n in graph.nodes)
        {
            InterruptTask(n);
        }
    }

    public void StopSequence()
    {
        StopSequence(activeGraph);
    }


    public void Resume()
    {
        UpdateSequence(current);
    }

    public void SkipCurrent()
    {
        if (current.link != null)
        {
            StopCoroutines();
            current = current.link;
            PlaySequence();
        }
    }

    public Coroutine StartTrack(GraphNode n, Coroutine cr)
    {
        //Debug.Log("Sequence: Start tracking node " + n.name, n);
        if (innerCoroutines == null)
            innerCoroutines = new Dictionary<GraphNode, Coroutine>();
        if (innerCoroutines.ContainsKey(n))
        {
            StopTrack(n);
        }
        innerCoroutines.Add(n, cr);
        return StartCoroutine(n.RunTaskCR(this));
        //yield return StartCoroutine(cr);
        //innerCoroutines.Remove(cr);
    }

    public void StopTrack(GraphNode n)
    {
        //Debug.Log("Sequence: Stop tracking node " + n.name, n);
        innerCoroutines.Remove(n);
        StopCoroutine(n.coroutine);
        n.coroutine = null;
    }

    public void InterruptTask(GraphNode n)
    {
        if (innerCoroutines.ContainsKey(n) == false)
        {
            //Debug.LogError("SequenceSystem: Interrupted coroutine is not running!");
            return;
        }
        n.StopTask(this);
        //StopCoroutine(innerCoroutines[n]);
    }

    public void StopCoroutines()
    {
        //StopCoroutine(coroutine);
        foreach (var cr in innerCoroutines)
        {
            StopCoroutine(cr.Value);
            cr.Key.coroutine = null;
        }
        innerCoroutines.Clear();
    }

    public void StopCoroutines(GraphNode g)
    {
        foreach (var cr in innerCoroutines)
        {
            if (cr.Key == g)
                continue;
            StopCoroutine(cr.Value);
            cr.Key.coroutine = null;
        }
        innerCoroutines.Clear();
    }

    //public void DoBehaviour<T>(System.Action<T> action) where T : MonoBehaviour
    //{
    //    T t = GetComponentInChildren<T>();
    //    if(t == null)
    //    {
    //        Debug.LogError($"SequenceSystem: {name} failed to do behaviour because it does not contain type {typeof(T)}!", gameObject);
    //        return;
    //    }
    //    action?.Invoke(t);
    //}


    public T GetSQBehaviour<T>()  where T : SequenceBehaviour
    {
        foreach (SequenceBehaviour mn in behaviours)
        {
            if (mn is T)
            {
                return mn as T;
            }
        }
        return null;
    }




    public static GameObject GetDynamicObject(string objectKey)
    {
        return DynamicObjectIndex.GetObject(objectKey);
    }

   




}


