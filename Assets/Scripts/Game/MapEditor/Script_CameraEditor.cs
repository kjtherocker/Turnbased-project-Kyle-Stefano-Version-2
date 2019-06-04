using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Script_EditorCamera))]
public class Script_CameraEditor : Editor
{

    public int X;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Script_EditorCamera myScript = (Script_EditorCamera)target;


        if (GUILayout.Button("Switch Prop"))
        {
            myScript.SwitchProp();
        }

        if (GUILayout.Button("Switch Node Type"))
        {
            myScript.SwitchNodeType();
        }

        if (GUILayout.Button("Up"))
        {
            myScript.MoveUp();
        }
        if (GUILayout.Button("Down"))
        {
            myScript.MoveDown();
        }
        if (GUILayout.Button("Left"))
        {
            myScript.MoveLeft();
        }
        if (GUILayout.Button("Right"))
        {
            myScript.MoveRight();
        }

        if (GUILayout.Button("Rotate 90"))
        {
            myScript.RotateObjectLeft();
        }
        if (GUILayout.Button("Rotate 180"))
        {
            myScript.RotateObjectRight();
        }
        if (GUILayout.Button("Rotate 270"))
        {
            myScript.RotateObjectUp();
        }
        if (GUILayout.Button("Rotate 360"))
        {
            myScript.RotateObjectDown();
        }





    }
}