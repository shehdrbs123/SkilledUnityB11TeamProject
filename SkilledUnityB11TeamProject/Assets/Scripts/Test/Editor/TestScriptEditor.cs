using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(TestScript))]
public class TestScriptEditor : Editor
{
    private TestScript testScript;
    private void Awake()
    {
        testScript = target as TestScript;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("아이템 추가"))
        {
            testScript.ItemAdd();
        }
    }
}
