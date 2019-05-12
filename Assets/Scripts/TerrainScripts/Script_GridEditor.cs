using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Script_GridFormations))]
public class Script_GridEditor : Editor
{

    public int X;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Script_GridFormations myScript = (Script_GridFormations)target;
        if (GUILayout.Button("Build Grid"))
        {
            myScript.CreateGrid(new Vector2Int(myScript.m_GridDimensions.x, myScript.m_GridDimensions.y));
        }
        if (GUILayout.Button("Destroy Grid"))
        {
            myScript.DeleteGrid();
        }
    }
}