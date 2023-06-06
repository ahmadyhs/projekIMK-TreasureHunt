using RoboRyanTron.Unite2017.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(copyWorldRotation), editorForChildClasses: true)]
public class copyWorldRotationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        copyWorldRotation obj = (copyWorldRotation)target;

        if (GUILayout.Button("Copy Rotation"))
        {
            obj.copy();
        }
    }
}
