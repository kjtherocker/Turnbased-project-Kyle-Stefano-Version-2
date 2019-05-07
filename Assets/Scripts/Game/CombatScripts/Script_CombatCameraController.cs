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
    public bool m_CommandBoardExists;

    public bool m_MovementHasBeenCalculated;
    public bool m_PlayerIsMoving;

    void Start()
    {
        m_CameraPositionInGrid = new Vector2Int(4, 4);
        Script_GameManager.Instance.m_BattleCamera = this;

        m_CommandBoardExists = false;
        //m_Grid = Script_GameManager.Instance.m_Grid;
    }

    // Update is called once per frame
    void Update()
    {

        CameraMovement();
        PlayerUiSelection();



    }




    public void CameraMovement()
    {
        if (m_PlayerIsMoving == false)
        {
            m_Grid.SetSelectoringrid(m_CameraPositionInGrid);
            transform.position = new Vector3(
                m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].transform.position.x + 13.5f,
                m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].transform.position.y + 13.9f,
                m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].transform.position.z - 13.5f);
        }
        else if (m_PlayerIsMoving == true)
        {
            transform.position = new Vector3(
                m_Creature.ModelInGame.transform.position.x + 13.5f,
                m_Creature.ModelInGame.transform.position.y + 13.9f,
                m_Creature.ModelInGame.transform.position.z - 13.5f);
        }


        if (m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].m_CreatureOnGridPoint != null)
        {
            m_StatusSheet.gameObject.SetActive(true);
            m_StatusSheet.Partymember = m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].m_CreatureOnGridPoint;
        }
        else
        {
            //m_StatusSheet.GetComponent<Animator>().SetTrigger("t_CommandBoardCrossOut");
            //m_StatusSheet.gameObject.SetActive(false);
        }

        if (Script_GameManager.Instance.UiManager.GetScreen(UiManager.Screen.CommandBoard) == null)
        {
            //Up and down Axis
            Script_GameManager.Instance.m_InputManager.SetXboxAxis
            (MoveUp, "Xbox_DPadY", true, ref Script_GameManager.Instance.m_InputManager.m_DPadY);
            Script_GameManager.Instance.m_InputManager.SetXboxAxis
                (MoveDown, "Xbox_DPadY", false, ref Script_GameManager.Instance.m_InputManager.m_DPadY);


            //Left and right Axis
            Script_GameManager.Instance.m_InputManager.SetXboxAxis
                (MoveRight, "Xbox_DPadX", true, ref Script_GameManager.Instance.m_InputManager.m_DPadX);
            Script_GameManager.Instance.m_InputManager.SetXboxAxis
                (MoveLeft, "Xbox_DPadX", false, ref Script_GameManager.Instance.m_InputManager.m_DPadX);

        }

        if (Input.GetButtonDown("Xbox_B"))
        {
            ReturnPlayerToInitalPosition();
        }

        if (Input.GetKeyDown("up"))
        {
            MoveUp();
        }
    }

    public void MoveUp()
    {
        m_Grid.DeSelectSelectoringrid(m_CameraPositionInGrid);
        m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x - 1, m_CameraPositionInGrid.y);
    }

    public void MoveDown()
    {
        m_Grid.DeSelectSelectoringrid(m_CameraPositionInGrid);
        m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x + 1, m_CameraPositionInGrid.y);
    }

    public void MoveLeft()
    {
        m_Grid.DeSelectSelectoringrid(m_CameraPositionInGrid);
        m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y - 1);
    }

    public void MoveRight()
    {
        m_Grid.DeSelectSelectoringrid(m_CameraPositionInGrid);
        m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y + 1);
    }


    public void PlayerUiSelection()
    {

        if (m_MovementHasBeenCalculated == true)
        {
            Script_GameManager.Instance.m_InputManager.SetXboxButton
             (PlayerWalk, "Xbox_A", ref Script_GameManager.Instance.m_InputManager.m_AButton);
        }

        if (Script_GameManager.Instance.UiManager.GetScreen(UiManager.Screen.CommandBoard) == null)
        {
            Script_GameManager.Instance.m_InputManager.SetXboxButton
            (CreateCommandBoard, "Xbox_A", ref Script_GameManager.Instance.m_InputManager.m_AButton);
        }   

        
    }

    public void PlayerWalk()
    {
        if (m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].m_Walkable == true)
        {

            m_Creature.m_CreatureAi.SetGoalPosition(m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].m_PositionInGrid);
            m_CommandBoardExists = false;
            m_MovementHasBeenCalculated = false;
        }
       
    }

    public void ReturnPlayerToInitalPosition()
    {
        if (m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].m_CreatureOnGridPoint != null)
        {
            //Checking to see if he has moved and if he hasnt attacked yet
            if (m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].
                    m_CreatureOnGridPoint.m_CreatureAi.m_HasMovedForThisTurn == true && 
                m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].
                    m_CreatureOnGridPoint.m_CreatureAi.m_HasAttackedForThisTurn == false)
            {
                //return the player to the original position
                m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].
                     m_CreatureOnGridPoint.m_CreatureAi.ReturnToInitalPosition();
            }


        }
    }

    public void CreateCommandBoard()
    {
      
         if (m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].m_CreatureOnGridPoint != null)
         {

             //Get creature on that point on the grid
             m_Creature =
                 m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].m_CreatureOnGridPoint;

             //Push Screen
             Script_GameManager.Instance.UiManager.PushScreen(UiManager.Screen.CommandBoard);

             //Get Screen
             UiScreenCommandBoard ScreenTemp =
                 Script_GameManager.Instance.UiManager.GetScreen(UiManager.Screen.CommandBoard) as UiScreenCommandBoard;

             //Set Screen Variables
             ScreenTemp.SetCreatureReference(m_Creature);
             m_CommandBoardExists = true;
         }
        
    }
    
}



