using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Monster))]
public class TestButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Monster generator = (Monster)target;
        if (GUILayout.Button("DAMAGE 1"))
        {
            generator.Hit(1, out bool test);
        }
    }
}
