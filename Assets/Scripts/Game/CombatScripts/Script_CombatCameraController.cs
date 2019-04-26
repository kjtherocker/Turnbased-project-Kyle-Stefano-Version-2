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
    public Script_Creatures m_Creature;

    public Script_HealthBar m_StatusSheet;

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

        if (m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].m_CreatureOnGridPoint != null)
        {
            m_StatusSheet.gameObject.SetActive(true);
            m_StatusSheet.Partymember = m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].m_CreatureOnGridPoint;
        }
        else
        {
            m_StatusSheet.gameObject.SetActive(false);
        }


        if (Input.GetKeyDown("space"))
        {
            if (m_Creature == null)
            {
                if (m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].m_CreatureOnGridPoint != null)
                {
                    m_Creature = m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].m_CreatureOnGridPoint;
                    m_Creature.m_CreatureAi.SpawnWalkableTiles();
                }
            }
            else
            {
                if (m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].m_Walkable == true)
                {
                    m_Creature.m_CreatureAi.SetGoalPosition(m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].m_PositionInGrid);
                    m_Creature = null;
                }
            }
        }

    }



    
}
