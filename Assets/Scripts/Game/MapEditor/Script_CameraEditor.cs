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
            // Editor.Repaint;
            myScript.ChangeNodeRotation(X);
            myScript.SwitchProp();
            
            
        }

        if (GUILayout.Button("Switch Node Type"))
        {
            myScript.SwitchNodeType();
        }


        if (GUILayout.Button("Switch Node Replacement"))
        {

            myScript.ChangeNodeRotation(X);
            myScript.SwitchNodeReplacement();
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
            X = 1;
        }
        if (GUILayout.Button("Rotate 180"))
        {
            myScript.RotateObjectRight();
            X = 2;
        }
        if (GUILayout.Button("Rotate 270"))
        {
            myScript.RotateObjectUp();
            X = 3;
        }
        if (GUILayout.Button("Rotate 360"))
        {
            myScript.RotateObjectDown();
            X = 4;
        }





    }
}