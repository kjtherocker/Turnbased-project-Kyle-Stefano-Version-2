﻿using System.Collections;
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

    public Script_Skills m_CreatureAttackingSkill;

    public Script_HealthBar m_StatusSheet;

    public Script_CombatNode m_NodeTheCameraIsOn;

    public Vector2Int m_CameraPositionInGrid;
    // Use this for initialization
    public bool m_CommandBoardExists;

    public bool m_MovementHasBeenCalculated;
    public bool m_PlayerIsMoving;
    public bool m_PlayerIsAttacking;

    void Start()
    {
        m_CameraPositionInGrid = new Vector2Int(1, 1);
        Script_GameManager.Instance.m_BattleCamera = this;

        m_CommandBoardExists = false;
        m_PlayerIsAttacking = false;
        //m_Grid = Script_GameManager.Instance.m_Grid;
        m_NodeTheCameraIsOn = m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y];
    }

    // Update is called once per frame
    void Update()
    {
        

        CameraMovement();
        PlayerUiSelection();



    }




    public void CameraMovement()
    {
        if (m_PlayerIsAttacking == false)
        {
            
            if (m_PlayerIsMoving == false)
            {
                m_Grid.SetSelectoringrid(m_CameraPositionInGrid);
                m_NodeTheCameraIsOn = m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y];
                transform.position = new Vector3(
                    m_NodeTheCameraIsOn.transform.position.x + 13.5f,
                    m_NodeTheCameraIsOn.transform.position.y + 13.9f,
                    m_NodeTheCameraIsOn.transform.position.z - 13.5f);
            }
            else if (m_PlayerIsMoving == true)
            {

                transform.position = new Vector3(
                    m_Creature.ModelInGame.transform.position.x + 13.5f,
                    m_Creature.ModelInGame.transform.position.y + 13.9f,
                    m_Creature.ModelInGame.transform.position.z - 13.5f);
            }
        }
        if (m_PlayerIsAttacking == true)
        {
            m_Grid.SetAttackingTileInGrid(m_CameraPositionInGrid);

            transform.position = new Vector3(
                m_NodeTheCameraIsOn.transform.position.x + 13.5f,
                m_NodeTheCameraIsOn.transform.position.y + 13.9f,
                m_NodeTheCameraIsOn.transform.position.z - 13.5f);

            Script_GameManager.Instance.m_InputManager.SetXboxAxis
           (MoveUp, "Xbox_DPadY", true, ref Script_GameManager.Instance.m_InputManager.m_DPadY);
            Script_GameManager.Instance.m_InputManager.SetXboxAxis
                (MoveDown, "Xbox_DPadY", false, ref Script_GameManager.Instance.m_InputManager.m_DPadY);


            //Left and right Axis
            Script_GameManager.Instance.m_InputManager.SetXboxAxis
                (MoveRight, "Xbox_DPadX", true, ref Script_GameManager.Instance.m_InputManager.m_DPadX);
            Script_GameManager.Instance.m_InputManager.SetXboxAxis
                (MoveLeft, "Xbox_DPadX", false, ref Script_GameManager.Instance.m_InputManager.m_DPadX);

            Script_GameManager.Instance.m_InputManager.SetXboxButton
            (AttackingIndividual, "Xbox_A", ref Script_GameManager.Instance.m_InputManager.m_AButton);
        }

        

        if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint != null)
        {
            m_StatusSheet.gameObject.SetActive(true);
            m_StatusSheet.Partymember = m_NodeTheCameraIsOn.m_CreatureOnGridPoint;
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

    public void SetAttackPhase(Script_Skills aSkill)
    {
        m_CreatureAttackingSkill = aSkill;
        m_PlayerIsAttacking = true;
    }

    public void MoveUp()
    {
        m_Grid.DeSelectSelectoringrid(m_CameraPositionInGrid);
        m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x - 1, m_CameraPositionInGrid.y);
        m_NodeTheCameraIsOn = m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y];
    }

    public void MoveDown()
    {
        m_Grid.DeSelectSelectoringrid(m_CameraPositionInGrid);
        m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x + 1, m_CameraPositionInGrid.y);
        m_NodeTheCameraIsOn = m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y];
    }

    public void MoveLeft()
    {
        m_Grid.DeSelectSelectoringrid(m_CameraPositionInGrid);
        m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y - 1);
        m_NodeTheCameraIsOn = m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y];
    }

    public void MoveRight()
    {
        m_Grid.DeSelectSelectoringrid(m_CameraPositionInGrid);
        m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y + 1);
        m_NodeTheCameraIsOn = m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y];
    }

    public void AttackingIndividual()
    {
        StartCoroutine(m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y]
            .m_CreatureOnGridPoint.DecrementHealth
            (m_CreatureAttackingSkill.GetSkillDamage(), m_CreatureAttackingSkill.GetElementalType(), 0.1f,0.1f, 1));

        Script_GameManager.Instance.UiManager.PopAllScreens();
    }

    public void PlayerUiSelection()
    {

        if (m_MovementHasBeenCalculated == true)
        {
            Script_GameManager.Instance.m_InputManager.SetXboxButton
             (PlayerWalk, "Xbox_A", ref Script_GameManager.Instance.m_InputManager.m_AButton);
        }

        if (Script_GameManager.Instance.UiManager.GetScreen(UiManager.Screen.CommandBoard) == null && m_PlayerIsAttacking == false)
        {
            Script_GameManager.Instance.m_InputManager.SetXboxButton
            (CreateCommandBoard, "Xbox_A", ref Script_GameManager.Instance.m_InputManager.m_AButton);
        }   

        
    }

    public void PlayerWalk()
    {
        if (m_NodeTheCameraIsOn.m_Walkable == true)
        {

            m_Creature.m_CreatureAi.SetGoalPosition(m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y].m_PositionInGrid);
            m_Grid.m_GridPathArray[m_Creature.m_CreatureAi.m_InitalPosition.x, m_Creature.m_CreatureAi.m_InitalPosition.y].m_CreatureOnGridPoint = null;
            m_CommandBoardExists = false;
            m_MovementHasBeenCalculated = false;
        }
       
    }

    public void ReturnPlayerToInitalPosition()
    {
        if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint != null)
        {
            //Checking to see if he has moved and if he hasnt attacked yet
            if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint.m_CreatureAi.m_HasMovedForThisTurn == true
                && m_NodeTheCameraIsOn.m_CreatureOnGridPoint.m_CreatureAi.m_HasAttackedForThisTurn == false && 
                m_Grid.m_GridPathArray[m_NodeTheCameraIsOn.m_CreatureOnGridPoint.m_CreatureAi.m_InitalPosition.x,
                m_NodeTheCameraIsOn.m_CreatureOnGridPoint.m_CreatureAi.m_InitalPosition.y].m_CreatureOnGridPoint == null)
            {
                //return the player to the original position
                m_NodeTheCameraIsOn.
                     m_CreatureOnGridPoint.m_CreatureAi.ReturnToInitalPosition();
            }


        }
    }

    public void CreateCommandBoard()
    {
      
         if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint != null)
         {

             //Get creature on that point on the grid
             m_Creature =
                 m_NodeTheCameraIsOn.m_CreatureOnGridPoint;

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



