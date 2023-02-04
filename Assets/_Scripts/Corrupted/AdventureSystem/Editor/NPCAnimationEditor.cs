using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class NPCAnimationEditor : EditorWindow
{
    public Expression[] expression;
    AnimatorController animator;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Tools/Dialogue/NPCAnimation")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        NPCAnimationEditor window = (NPCAnimationEditor)EditorWindow.GetWindow(typeof(NPCAnimationEditor));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        ListField("expression");
        animator = (AnimatorController)EditorGUILayout.ObjectField(animator, typeof(AnimatorController), false);
        if (GUILayout.Button("Go"))
        {
            AddStates();
        }
    }

    public void AddStates()
    {
        foreach(Expression e in expression)
        {
            AnimatorState a = animator.AddMotion(e.motion);
            a.name = e.name;
        }
    }

    public void ListField<T>(T t)
    {
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);

        SerializedProperty property = so.FindProperty(GetPropertyName(t));
        EditorGUILayout.PropertyField(property, true);
        so.ApplyModifiedProperties();
    }

    public void ListField(string property)
    {
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);

        SerializedProperty p = so.FindProperty(property);
        EditorGUILayout.PropertyField(p, true);
        so.ApplyModifiedProperties();
    }

    public string GetPropertyName<T>(T t)
    {
        return GetPropertyName<T>(() => t);
    }

    public string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
    {
        var me = propertyLambda.Body as MemberExpression;

        if (me == null)
        {
            throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
        }

        return me.Member.Name;
    }
}
