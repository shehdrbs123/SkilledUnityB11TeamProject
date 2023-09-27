using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(TextureMaker))]
public class TextureMakeES : Editor
{
    private TextureMaker tx;

    private void Awake()
    {
        tx = target as TextureMaker;
    }

    public override void OnInspectorGUI()
    {
        
        base.OnInspectorGUI();
        if (GUILayout.Button("TargetPrefabsPath 폴더 내 Texture 생성"))
        {
            tx.MakeTexture();
        }

        if (GUILayout.Button("현재 화면 Texture 생성"))
        {
            tx.SaveTexture();
        }
    }
}
