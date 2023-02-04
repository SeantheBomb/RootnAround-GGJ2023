using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XNode;

/// <summary> Base node for the DialogueGraph system </summary>
public abstract class GraphNode : Node 
{
    [Input] public GraphNode input;
    [Output] public GraphNode output;


    public Action onStateChange;
    public static Action OnAnyChangeClear, OnDChangeRedraw, OnRChangeRedraw;

    [HideInInspector]public string key;

    public Coroutine coroutine;
    public bool isPlaying => coroutine != null;


    public GraphNode[] path
    {
        get
        {
            return new List<GraphNode>(GetPort("output").GetInputValues<GraphNode>()).ToArray();
        }
    }

    public GraphNode link
    {
        get
        {
            return GetLink("output");
        }
    }


    public abstract IEnumerator PlayNode(SequenceSystemManager director);

    protected virtual void StopNode(SequenceSystemManager director) { }


    public void RunTask(SequenceSystemManager director)
    {
        if (isPlaying)
        {
            Debug.LogError("Sequence: Can not start node because it is already playing!", this);
            return;
        }
        //coroutine = director.StartCoroutine(RunTaskCR(director));
        coroutine = director.StartTrack(this, coroutine);
    }

    public IEnumerator RunTaskCR(SequenceSystemManager director)
    {
        yield return PlayNode(director);
        director.StopTrack(this);
    }

    public void StopTask(SequenceSystemManager director)
    {
        if(isPlaying == false)
        {
            //Debug.LogError("Sequence: Can not stop node because it is not playing!", this);
            return;
        }
        //director.StopCoroutine(coroutine);
        director.StopTrack(this);
        StopNode(director);
    }

    public bool HasNextNode<K>() where K : GraphNode
    {
        foreach(GraphNode gn in path)
        {
            Debug.Log($"Sequence: Check if {name}'s next node {gn.name} is expected node: {gn is K}");
            if (gn is K)
            {
                return true;
            }
        }
        Debug.Log($"Sequence: {name} has no next nodes of this type.");
        return false;
    }

    public GraphNode[] GetPath(string port)
    {
        return new List<GraphNode>(GetPort(port).GetInputValues<GraphNode>()).ToArray();
    }

    public T[] GetPath<T>(string port) where T : GraphNode
    {
        List<T> list = new List<T>();
        foreach(var n in GetPath(port))
        {
            if(n is T)
            {
                list.Add(n as T);
            }
        }
        return list.ToArray();
    }

    public GraphNode GetLink(string port)
    {
        foreach (GraphNode gn in GetPath(port))
        {
            if (gn != null)
                return gn;
        }
        Debug.Log("GraphNode: Port " + port + " has no link!");
        return null;
    }

    public virtual void PlayNextInPath(SequenceSystemManager view)
    {
        PlayNextFromPort(view, "output");
    }

    public virtual void PlayNextFromPort(SequenceSystemManager view, string port)
    {
        foreach (GraphNode gn in GetPath(port))
        {
            Debug.Log($"Sequence: {name} starting next task {gn.name}", gn);
            gn.RunTask(view);
        }
    }


    //protected override void Init()
    //{
    //    OnAnyChangeClear += OnChangeClear;
    //    if (this is ResponseNode )OnRChangeRedraw += OnChangeRedraw;
    //    if (this is DialogueNode) OnDChangeRedraw += OnChangeRedraw;
    //}

    //protected override void DeInit()
    //{
    //    OnAnyChangeClear -= OnChangeClear;
    //    if (this is ResponseNode) OnRChangeRedraw -= OnChangeRedraw;
    //    if (this is DialogueNode) OnDChangeRedraw -= OnChangeRedraw;
    //}

    public void SendSignal(NodePort output)
    {
        // Loop through port connections
        int connectionCount = output.ConnectionCount;
        for (int i = 0; i < connectionCount; i++)
        {
            NodePort connectedPort = output.GetConnection(i);

            // Get connected ports logic node
            GraphNode connectedNode = connectedPort.node as GraphNode;

            // Trigger it
            if (connectedNode != null) connectedNode.OnInputChanged();
        }
        if (onStateChange != null) onStateChange();
        //OnAnyChange();
    }

    protected virtual void OnInputChanged() { }

    //public virtual void OnChangeClear() { }
    //public virtual void OnChangeRedraw() { }

    //public static void OnAnyChange()
    //{
    //    if (OnAnyChangeClear != null) OnAnyChangeClear();
    //    if (OnRChangeRedraw != null) OnDChangeRedraw();
    //    if (OnRChangeRedraw != null) OnRChangeRedraw();
    //}


    protected override void Init()
    {
        base.Init();
        output = this;
    }


    public override void OnRemoveConnection(NodePort port)
    {

    }

    public override void OnCreateConnection(NodePort from, NodePort to)
    {
        OnInputChanged();
    }

    public override object GetValue(NodePort port)
    {
        return output;
    }
}
