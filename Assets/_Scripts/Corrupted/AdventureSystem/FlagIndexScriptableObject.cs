using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;


[CreateAssetMenu(fileName = "FlagIndex", menuName = "Corrupted/Sequence/FlagIndex", order = 1)]
public class FlagIndexScriptableObject : ScriptableObject
{

    public Flag[] flags;

}

[System.Serializable]
public struct Flag
{
    public KeyVariable key;
    public BoolVariable value;
}
