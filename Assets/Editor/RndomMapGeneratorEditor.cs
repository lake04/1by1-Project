using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractMapGenerator), true)]
public class RndomMapGeneratorEditor : Editor
{
    AbstractMapGenerator generator;

    protected void Awake()
    {
        generator = (AbstractMapGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Map"))
        {
                generator.GenerateMap();
        }
    }
}
