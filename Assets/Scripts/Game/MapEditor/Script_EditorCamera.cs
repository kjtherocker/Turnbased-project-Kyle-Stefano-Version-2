using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Script_EditorCamera : MonoBehaviour
{


    public Script_CombatNode[,] m_GridPathArray;

    public Script_CombatNode m_NodeTheCameraIsOn;

    public Script_GridFormations m_GridFormation;

    public Vector2Int m_CameraPositionInGrid;

    public Vector2Int m_GridDimensions;

    public GameObject m_Selector;

    public bool m_EditingHasStarted;

    public GameObject m_ThingToforceEditorTopdate;


    public int m_Test;

    public Script_PropList.Props m_EditorProp;
    public Script_CombatNode.CombatNodeTypes m_EditorNode;
    public Script_PropList.NodeReplacements m_NodeReplacements;

    #if (UNITY_EDITOR)
        
    // Start is called before the first frame update
    void Start()
    {

        m_CameraPositionInGrid = new Vector2Int(1, 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (m_EditingHasStarted == true)
        {
         //  transform.position = new Vector3(
         //              m_NodeTheCameraIsOn.transform.position.x + 13.5f,
         //              m_NodeTheCameraIsOn.transform.position.y + 13.9f,
         //              m_NodeTheCameraIsOn.transform.position.z - 13.5f);
         //

        }

    }


    public void Convert1DArrayto2D(List<Script_CombatNode> aNodeGroup, Vector2Int grid)
    {

        m_GridDimensions = grid;
        m_GridPathArray = new Script_CombatNode[m_GridDimensions.x, m_GridDimensions.y];

        for (int i = 0; i < m_GridDimensions.x * m_GridDimensions.y; i++)
        {

            m_GridPathArray[aNodeGroup[i].m_PositionInGrid.x, aNodeGroup[i].m_PositionInGrid.y] = aNodeGroup[i];



        }
    }

    public void MoveUp()
    {




        m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x - 1, m_CameraPositionInGrid.y);
        m_NodeTheCameraIsOn = m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y];
        m_Selector.gameObject.transform.position =
            new Vector3(m_NodeTheCameraIsOn.transform.position.x, m_NodeTheCameraIsOn.transform.position.y + Constants.Constants.m_HeightOffTheGrid + 0.8f, m_NodeTheCameraIsOn.transform.position.z);
    }

    public void MoveDown()
    {


        m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x + 1, m_CameraPositionInGrid.y);
        m_NodeTheCameraIsOn = m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y];
        m_Selector.gameObject.transform.position =
            new Vector3(m_NodeTheCameraIsOn.transform.position.x, m_NodeTheCameraIsOn.transform.position.y + Constants.Constants.m_HeightOffTheGrid + 0.8f, m_NodeTheCameraIsOn.transform.position.z);
    }

    public void MoveLeft()
    {

        m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y - 1);
        m_NodeTheCameraIsOn = m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y];
        m_Selector.gameObject.transform.position =
            new Vector3(m_NodeTheCameraIsOn.transform.position.x, m_NodeTheCameraIsOn.transform.position.y + Constants.Constants.m_HeightOffTheGrid + 0.8f, m_NodeTheCameraIsOn.transform.position.z);
    }

    public void MoveRight()
    {


        m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y + 1);
        m_NodeTheCameraIsOn = m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y];
        m_Selector.gameObject.transform.position =
            new Vector3(m_NodeTheCameraIsOn.transform.position.x, m_NodeTheCameraIsOn.transform.position.y + Constants.Constants.m_HeightOffTheGrid + 0.8f, m_NodeTheCameraIsOn.transform.position.z);
    }

    public void RotateObjectRight()
    {
        m_NodeTheCameraIsOn.m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        m_NodeTheCameraIsOn.m_NodeRotation = 1;
    }
    public void RotateObjectLeft()
    {
        m_NodeTheCameraIsOn.m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        m_NodeTheCameraIsOn.m_NodeRotation = 2;
    }

    public void RotateObjectUp()
    {
        m_NodeTheCameraIsOn.m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
        m_NodeTheCameraIsOn.m_NodeRotation = 3;
    }

    public void RotateObjectDown()
    {
        m_NodeTheCameraIsOn.m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 360, 0));
        m_NodeTheCameraIsOn.m_NodeRotation = 4;
    }

    public void SwitchProp()
    {
        m_ThingToforceEditorTopdate.gameObject.transform.position = new Vector3(0, m_Test++, 0);
        m_NodeTheCameraIsOn.m_PropOnNode = m_EditorProp;
    }

    public void SwitchNodeType()
    {
        m_ThingToforceEditorTopdate.gameObject.transform.position = new Vector3(0, m_Test++, 0); 
        m_NodeTheCameraIsOn.m_CombatsNodeType = m_EditorNode;
    }

    public void ChangeNodeRotation(int aRotation)
    {

        m_NodeTheCameraIsOn.m_NodeRotation = aRotation;
    }


    public void SwitchNodeReplacement()
    {
        m_NodeTheCameraIsOn.m_NodeReplacementOnNode = m_NodeReplacements;
    }

    //m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
    //m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 360, 0));
#endif
}
