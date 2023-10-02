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

        if (GUILayout.Button("밤으로 변경"))
        {
            GameManager.Instance._dayManager.time = 0.7f;
        }

        for (int i = 0; i < 5; ++i)
        {
            if (GUILayout.Button((i+1) +"일차 변경"))
            {
                GameManager.Instance._dayManager.day = i+1;
                GameManager.Instance._dayManager.textDay.text = (i+1).ToString();
            }
        }
        if (GUILayout.Button("마지막 날로 변경"))
        {
            GameManager.Instance._dayManager.day = 6;
            GameManager.Instance._dayManager.textDay.text = "????";
        }
    }
}
