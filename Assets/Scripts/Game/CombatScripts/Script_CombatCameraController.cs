using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CombatCameraController : MonoBehaviour
{
    public enum CameraState
    {
        Nothing,
        Default,
        Spawn,
        AllyHealingSelecting,
        AllyHealing,
        EnemyHealing,
        AllyAttacking,
        AllyAttackSelecting,
        EnemyAttacking,
        EnemyAttackingMelee,
        EnemyZoomIn,
        AllyBuff,
        EnemyBuff


    }

    public CameraState m_cameraState;

    public Script_Grid m_Grid;

    public Vector2Int m_CameraPositionInGrid;
    // Use this for initialization
    void Start()
    {
        m_CameraPositionInGrid = new Vector2Int(4, 4);
        Script_GameManager.Instance.m_BattleCamera = this;

        

        //m_Grid = Script_GameManager.Instance.m_Grid;
    }

    // Update is called once per frame
    void Update()
    {
        m_Grid.SetSelectoringrid(m_CameraPositionInGrid);
        transform.position = new Vector3(
            m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].transform.position.x + 5.5f,
            m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].transform.position.y + 5.9f,
            m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].transform.position.z - 5.5f);

        if (Input.GetKeyDown("up"))
        {
            m_Grid.DeSelectSelectoringrid(m_CameraPositionInGrid);
            m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x - 1, m_CameraPositionInGrid.y);
           
        }

        if (Input.GetKeyDown("down"))
        {
            m_Grid.DeSelectSelectoringrid(m_CameraPositionInGrid);
            m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x + 1, m_CameraPositionInGrid.y);

        }

        if (Input.GetKeyDown("left"))
        {
            m_Grid.DeSelectSelectoringrid(m_CameraPositionInGrid);
            m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y - 1);

        }

        if (Input.GetKeyDown("right"))
        {
            m_Grid.DeSelectSelectoringrid(m_CameraPositionInGrid);
            m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y + 1);

        }


    }



    
}
