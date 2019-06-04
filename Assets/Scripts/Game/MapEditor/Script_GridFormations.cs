using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[ExecuteInEditMode]
[System.Serializable]
public class Script_GridFormations : MonoBehaviour
{

    public Vector2Int m_GridDimensions;

    public GameObject m_PrefabNode;
    public Script_CombatNode[,] m_GridPathArray;

    public List<Script_CombatNode> m_ListToConvert;
    public Material m_SelectedMaterial;
    public Script_Grid m_Grid;

    public int PlayerX;
    public int PlayerY;

    public int m_Movement;

    public bool m_GotPathNodes;



    void Start()
    {

    }
    
    public void CreateGrid(Vector2Int grid)
    {

        m_GridPathArray = new Script_CombatNode[m_GridDimensions.x, m_GridDimensions.y];

        for (int x = 0; x < grid.x; x++)
        {
            for (int y = 0; y < grid.y; y++)
            { 

                m_GridPathArray[x, y] = Instantiate<Script_CombatNode>(m_PrefabNode.GetComponent<Script_CombatNode>(), transform);

                m_ListToConvert.Add(m_GridPathArray[x, y]);


                m_GridPathArray[x, y].transform.position = new Vector3(2 * x, 0.5f, 2 * y);



                m_GridPathArray[x, y].m_PositionInGrid = new Vector2Int(x, y);


            }
        }
        m_Movement = 1;
    }

    public void DeleteGrid()
    {
        if (m_GridPathArray.Length > 0)
        {
            for (int x = 0; x < m_GridDimensions.x; x++)
            {
                for (int y = 0; y < m_GridDimensions.y; y++)
                {

                    DestroyImmediate(m_GridPathArray[x, y]);

                }
            }
        }

    }

    public void StartCameraEditor()
    {
        Script_GameManager.Instance.m_EditorCamera.Convert1DArrayto2D(m_ListToConvert,m_GridDimensions);
        Script_GameManager.Instance.m_EditorCamera.m_NodeTheCameraIsOn = Script_GameManager.Instance.m_EditorCamera.m_GridPathArray[1, 1];
        Script_GameManager.Instance.m_EditorCamera.m_EditingHasStarted = true;

    }

    public void StopCameraEditor()
    {
        Script_GameManager.Instance.m_EditorCamera.m_EditingHasStarted = false;
        Script_GameManager.Instance.m_EditorCamera.m_GridPathArray = null;
        Script_GameManager.Instance.m_EditorCamera.m_NodeTheCameraIsOn = null;

    }


}



