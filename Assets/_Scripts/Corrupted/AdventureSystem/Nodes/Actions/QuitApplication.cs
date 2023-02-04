using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(200), NodeTint(100, 20, 20), CreateNodeMenu("Actions/QuitApplication")]
public class QuitApplication : GraphNode {



    public override IEnumerator PlayNode(SequenceSystemManager director)
    {
        Debug.Log("Sequence: Quit application node reached");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        yield break;
    }
}