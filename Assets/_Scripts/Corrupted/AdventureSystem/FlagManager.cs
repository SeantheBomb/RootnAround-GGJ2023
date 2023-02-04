using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;

public class FlagManager : Singleton<FlagManager>
{

    public static System.Action<string, bool> OnFlagUpdated;

    public FlagIndexScriptableObject flagIndex;

    protected static Dictionary<string, bool> flags;

    // Start is called before the first frame update
    void Start()
    {
        if (flags == null)
        {
            flags = new Dictionary<string, bool>();
        }
        foreach (Flag f in flagIndex.flags)
        {
            if (flags.ContainsKey(f.key))
            {
                Debug.LogError("FlagManager: Can not add duplicate key: " + f.key);
                continue;
            }
            flags.Add(f.key, f.value);
        }
    }

    public static void SetFlag(string key, bool value)
    {
        if (flags.ContainsKey(key))
        {
            flags[key] = value;
            OnFlagUpdated?.Invoke(key, value);
        }
        else
        {
            Debug.LogError("FlagManager: Can not set flag because it does not exist!");
        }
    }

    public static void SetFlag(KeyVariable key, bool value)
    {
        SetFlag(key, value);
    }

    public static bool GetFlag(string key)
    {
        if (flags.ContainsKey(key))
        {
            return flags[key];
        }
        else
        {
            Debug.LogError("FlagManager: Can not set flag because it does not exist!");
            return false;
        }
    }

    public static bool GetFlag(KeyVariable keyVariable)
    {
        return GetFlag(keyVariable.Value);
    }

    public static void RaiseFlag(string key)
    {
        SetFlag(key, true);
    }

    public static void RaiseFlag(KeyVariable keyVariable)
    {
        RaiseFlag(keyVariable.Value);
    }

    public static void LowerFlag(string key)
    {
        SetFlag(key, false);
    }

    public static void LowerFlag(KeyVariable keyVariable)
    {
        LowerFlag(keyVariable.Value);
    }

    public static void ToggleFlag(string key)
    {
        SetFlag(key, GetFlag(key) == false);
    }

    public static void ToggleFlag(KeyVariable keyVariable)
    {
        ToggleFlag(keyVariable.Value);
    }

}
