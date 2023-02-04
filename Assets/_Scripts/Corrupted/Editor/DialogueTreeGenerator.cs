using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class DialogueTreeGenerator : EditorWindow
{
    

    public const char inCellDelim = ';';

    public Dictionary<string, GraphNode> nodes = new Dictionary<string, GraphNode>();
    public Dictionary<string, string[]> paths = new Dictionary<string, string[]>();

    public TextAsset input;
    public SequenceGraph output;

    public Expression[] expressions;
    //string outputPath;

    string literalInput = "*";
    string delimInput = ";";
    string ignoreInput = "\"“”";

    char literalChar = '*';
    char delimChar = ';';

    int deltaX = 300, deltaY = 400;

    float x = 0, y = 0;
    int entry;


    [MenuItem("Tools/Dialogue/GenerateTree")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(DialogueTreeGenerator));
    }

    void OnGUI()
    {
        input = (TextAsset)EditorGUILayout.ObjectField("Import data:", input, typeof(TextAsset), false);
        output = (SequenceGraph)EditorGUILayout.ObjectField("Output Graph:", output, typeof(SequenceGraph), false);
        deltaX = EditorGUILayout.IntField("Delta X: ", deltaX);
        deltaY = EditorGUILayout.IntField("Delta Y: ", deltaY);
        literalInput = EditorGUILayout.TextField("Literal Notation: ", literalInput);
        delimInput = EditorGUILayout.TextField("Deliminator Notation: ", delimInput);
        ignoreInput = EditorGUILayout.TextField("Ignore Characters: ", ignoreInput);
        CorruptedEditorLayout.ListField(this, "expressions");
        if (GUILayout.Button("Generate"))
        {
            ParseFile();
        }
    }


    void ParseFile()
    {
        //outputPath = AssetDatabase.GetAssetPath(output);
        if(delimInput.Length == 0 || literalInput.Length == 0)
        {
            Debug.LogWarning("Can't parse file because settings are not correct.");
            return;
        }
        literalChar = literalInput[0];
        delimChar = delimInput[0];
        nodes.Clear();
        paths.Clear();
        ClearOutputGraph();
        entry = 1;
        string[] rows = input.text.Split('\n');
        foreach(string r in rows)
        {
            string[] values = ParseCSV(r);
            string nodeType = values[0];
            switch (nodeType)
            {
                case "Dialogue":
                    ParseDialogue(values);
                    break;
                case "Response":
                    ParseResponse(values);
                    break;
                case "Entry":
                    ParseEntry(values);
                    break;
                case "Exit":
                    ParseExit(values);
                    break;
                case "Event":
                    ParseEvent(values);
                    break;
            }
        }
        //AssetDatabase.Refresh();
        foreach (string key in paths.Keys)
        {
            List<GraphNode> n = new List<GraphNode>();
            int i = 0;
            foreach(string p in paths[key])
            {
                if (nodes.ContainsKey(p) == false)
                {
                    Debug.LogError("Row " + key.Replace(' ', '_') + " contains invalid path: " + p.Replace(' ', '_'));
                    continue;
                }
                n.Add(nodes[p]);
                if(nodes[p].position == Vector2.zero)nodes[p].position = IncrementPosition(nodes[key].position, deltaX, deltaY * i++);
            }
            GraphNode nn = nodes[key];
            AddPaths(ref nn, n.ToArray());
            //EditorUtility.SetDirty(nn);
        }
        ValidateEntryPaths();
        //EditorUtility.SetDirty(output);
        //AssetDatabase.SaveAssets();
        //EditorUtility.
    }

    //s[0] = type, s[1] = key, s[2] = path, s[3] = text, s[4] = audio, s[5] = expression
    void ParseDialogue(string[] s)
    {
        if(s.Length != 6)
        {
            Debug.LogError("DialogueTreeGenerator: Row missing values - " + s[1]);
        }
        string key = s[1];
        string text = s[3];
        Debug.Log("Find audio clip: " + s[4]);
        AudioClip clip = Resources.Load<AudioClip>(s[4]);
        Debug.Log("Was audio found? " + (clip != null ? "Yes!" : "No..."));
        Expression emote = FindExpression(s[5]);
        AddPathToDictionary(key, s[2]);
        GenerateDialogueNode(key, text, clip, emote);
    }

    Expression FindExpression(string s)
    {
        foreach(Expression e in expressions)
        {
            if (e.name == s)
                return e;
        }
        Debug.LogError("DialogueTreeGenerator: Failed to find expression " + s);
        return expressions[0];
    }


    //s[0] = type, s[1] = key, s[2] = path, s[3] = text
    void ParseResponse(string[] s)
    {
        string key = s[1];
        string text = s[3];
        AddPathToDictionary(key, s[2]);
        GenerateResponseNode(key, text);
    }

    //s[0] = type, s[1] = key, s[2] = path, s[3] = visit
    void ParseEntry(string[] s)
    {
        string key = s[1];
        int visit = 0;
        if(string.IsNullOrWhiteSpace(s[3]) == false)
            visit = int.Parse(s[3]);
        AddPathToDictionary(key, s[2]);
        GenerateEntryNode(key, visit);
    }

    //s[0] = type, s[1] = key
    void ParseExit(string[] s)
    {
        GenerateExitNode(s[1]);
    }

    //s[0] = type, s[1] = key, s[2] = path, s[3] = eventName
    void ParseEvent(string[] s)
    {
        string key = s[1];
        string eventName = s[3];
        AddPathToDictionary(key, s[2]);
        GenerateFireEventNode(key, eventName);
    }


    DialogueNode GenerateDialogueNode(string key, string text, AudioClip vo, Expression emote)
    {
        if (nodes.ContainsKey(key))
            return (DialogueNode)nodes[key];
        DialogueNode node = output.AddNode<DialogueNode>();
        node.key = key;
        node.dialogue = text;
        node.voiceover = vo;
        node.expression = emote;
        nodes.Add(key, node);
        AssetDatabase.AddObjectToAsset(node, output);
        return node;
    }

    ResponseNode GenerateResponseNode(string key, string text)
    {
        if (nodes.ContainsKey(key))
            return (ResponseNode)nodes[key];
        ResponseNode node = output.AddNode<ResponseNode>();
        node.key = key;
        node.response = text;
        nodes.Add(key, node);
        AssetDatabase.AddObjectToAsset(node, output);
        return node;
    }

    PlayEventNode GenerateFireEventNode(string key, string text)
    {
        if (nodes.ContainsKey(key))
            return (PlayEventNode)nodes[key];
        PlayEventNode node = output.AddNode<PlayEventNode>();
        node.key = key;
        node.eventKey = text;
        nodes.Add(key, node);
        AssetDatabase.AddObjectToAsset(node, output);
        return node;
    }

    EntryNode GenerateEntryNode(string key, int visit)
    {
        if (nodes.ContainsKey(key))
            return (EntryNode)nodes[key];
        EntryNode node = output.AddNode<EntryNode>();
        node.key = key;
        node.visit = visit;
        nodes.Add(key, node);
        AssetDatabase.AddObjectToAsset(node, output);
        node.position.x += deltaX;
        node.position.y += deltaY * entry++;
        Debug.Log("Set entry position " + node.position.y);
        return node;
    }

    ExitNode GenerateExitNode(string key)
    {
        if (nodes.ContainsKey(key))
            return (ExitNode)nodes[key];
        ExitNode node = output.AddNode<ExitNode>();
        node.key = key;
        nodes.Add(key, node);
        AssetDatabase.AddObjectToAsset(node, output);
        return node;
    }

    void AddPaths(ref GraphNode node, GraphNode[] path)
    {
        foreach (GraphNode gn in path)
        {
            node.GetPort("output").Connect(gn.GetPort("input"));
        }
    }

    void AddPathToDictionary(string key, string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            Debug.LogError("Key " + key + " has no paths.");
        }
        string[] p = path.Split(delimChar);
        paths.Add(key, p);
    }

    void ValidateEntryPaths()
    {
        foreach(string gn in nodes.Keys)
        {
            if(nodes[gn] is EntryNode)
            {
                continue;
            }
            bool hasPath = false;
            foreach(string path in paths.Keys)
            {
                List<string> pList = new List<string>(paths[path]);
                if (pList.Contains(gn))
                {
                    if(path == gn)
                    {
                        Debug.LogError(gn + " has path to itself.");
                        continue;
                    }
                    hasPath = true;
                    break;
                }
            }
            if(hasPath == false)
            {
                Debug.LogError(gn + " has no path leading to it.", nodes[gn]);
            }
        }
    }


    string[] ParseCSV(string s)
    {
        List<string> result = new List<string>();
        string current = "";
        bool ignoreComma = false;
        foreach(char c in s)
        {
            if (ignoreInput.Contains(c.ToString()))//Ignore special quotations added by csv formatter
                continue;
            if(c == literalChar)
            {
                ignoreComma = !ignoreComma;
            }
            else if(c == ',' && ignoreComma == false)
            {
                result.Add(current);
                current = "";
            }
            else
            {
                current += c;
            }
        }
        result.Add(current);
        return result.ToArray();
    }

    Vector2 IncrementPosition(Vector2 pos, float addX, float addY)
    {
        Vector2 result = pos;
        result.x += addX;
        result.y += addY;
        return result;
    }

    void ClearOutputGraph()
    {
        foreach(XNode.Node n in output.nodes)
        {
            if (n == null)
                continue;
            AssetDatabase.RemoveObjectFromAsset(n);
        }
        output.Clear();
        AssetDatabase.SaveAssets();
    }
}
