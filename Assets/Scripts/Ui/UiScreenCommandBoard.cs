using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UiScreenCommandBoard : UiScreen
{
    public Animator m_CommandBoardAnimator;
    public Script_Creatures m_CommandboardCreature;
    public Button m_MovementButton;
    public TextMeshProUGUI m_MovementText;
    public TextMeshProUGUI m_Attack;
    public TextMeshProUGUI m_Skill;
    public int m_CommandBoardPointerPosition;
    
    // Use this for initialization
	void Start ()
    {
        m_CommandBoardPointerPosition = 0;

    }
	
	// Update is called once per frame
	void Update ()
    {
       

        if (m_CommandBoardPointerPosition == 0)
        {
            m_MovementText.color = Color.red;
            m_Attack.color = Color.white;
            m_Skill.color = Color.white;

        }
        if (m_CommandBoardPointerPosition == 1)
        {
            m_MovementText.color = Color.white;
            m_Attack.color = Color.red;
            m_Skill.color = Color.white;
        }
        if (m_CommandBoardPointerPosition == 2)
        {
            m_MovementText.color = Color.white;
            m_Attack.color = Color.white;
            m_Skill.color = Color.red;
        }
        if (m_CommandBoardPointerPosition == 3)
        {

        }
        if (m_CommandBoardPointerPosition == 4)
        {

        }

        


        if (m_InputActive == true)
        {
            if (Input.GetKeyDown("a") || Input.GetButtonDown("Ps4_Circle"))
            {
                Script_GameManager.Instance.m_UiManager.PopScreen();
            }

            if (Input.GetKeyDown("a")  || Input.GetButtonDown("Ps4_Cross"))
            {
                if (m_CommandBoardPointerPosition == 0)
                {
                    PlayerMovement();
                }
                if (m_CommandBoardPointerPosition == 1)
                {
                    Script_GameManager.Instance.UiManager.PopScreen();
                    Script_GameManager.Instance.BattleCamera.SetAttackPhase(m_CommandboardCreature.m_Attack);
                }
                if (m_CommandBoardPointerPosition == 2)
                {
                    SpawnSkillBoard();
                }
                if (m_CommandBoardPointerPosition == 3)
                {

                }
                if (m_CommandBoardPointerPosition == 4)
                {

                }

            }

            if (Constants.Constants.m_XboxController == true)
            {
                Script_GameManager.Instance.m_InputManager.SetXboxAxis
                (MoveCommandBoardPositionUp, "Xbox_DPadX", true, ref Script_GameManager.Instance.m_InputManager.m_DPadY);
                Script_GameManager.Instance.m_InputManager.SetXboxAxis
                    (MoveCommandBoardPositionDown, "Xbox_DPadX", false, ref Script_GameManager.Instance.m_InputManager.m_DPadY);
            }
            if (Constants.Constants.m_PlaystationController == true)
            {
                Script_GameManager.Instance.m_InputManager.SetPlaystationAxis
                (MoveCommandBoardPositionUp, "Ps4_DPadX", true, ref Script_GameManager.Instance.m_InputManager.m_DPadY);
                Script_GameManager.Instance.m_InputManager.SetPlaystationAxis
                    (MoveCommandBoardPositionDown, "Ps4_DPadX", false, ref Script_GameManager.Instance.m_InputManager.m_DPadY);
            }
        }


        if (m_CommandBoardPointerPosition < 0)
        {
            m_CommandBoardPointerPosition = 0;
        }
        else if (m_CommandBoardPointerPosition > 2)
        {
            m_CommandBoardPointerPosition = 2;
        }
    }

    public override void OnPop()
    {
        //m_CommandBoardAnimator.SetTrigger("t_CommandBoardCrossOut");
        TurnCommandBoardOff();
        

    }

    public override void OnPush()
    {
        gameObject.SetActive(true);
        
        m_CommandBoardAnimator.SetTrigger("t_CommandBoardCrossIn");
    }

    public void TurnCommandBoardOff()
    {
        gameObject.SetActive(false);
        m_CommandBoardPointerPosition = 0;


    }

    public void SetCreatureReference(Script_Creatures aCreature)
    {
        m_CommandboardCreature = aCreature;

        if (m_CommandboardCreature.m_CreatureAi.m_HasMovedForThisTurn == true)
        {
            m_MovementButton.interactable = false;
        }
        else
        {
            m_MovementButton.interactable = true;
        }

    }

    public void PlayerMovement()
    {
        if (m_CommandboardCreature.m_CreatureAi.m_HasMovedForThisTurn == false)
        {
            Script_GameManager.Instance.m_Grid.SetWalkingHeuristic(m_CommandboardCreature.m_CreatureAi.m_Position);
            Script_GameManager.Instance.UiManager.PopScreen();
        }
    }


    public void SpawnSkillBoard()
    {
        Script_GameManager.Instance.UiManager.PushScreen(UiManager.Screen.SkillBoard);

        UiSkillBoard ScreenTemp =
            Script_GameManager.Instance.UiManager.GetScreen(UiManager.Screen.SkillBoard) as UiSkillBoard;

        ScreenTemp.SpawnSkills(m_CommandboardCreature);
    }

    public void MoveCommandBoardPositionUp()
    {
        m_CommandBoardPointerPosition++;
    }

    public void MoveCommandBoardPositionDown()
    {
        m_CommandBoardPointerPosition--;
    }
}
