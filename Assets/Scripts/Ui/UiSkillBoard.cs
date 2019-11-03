using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiSkillBoard : UiScreen
{

    public Creatures m_SkillBoardCreature;
    public ButtonSkillWrapper m_ButtonReference;
    public List<ButtonSkillWrapper> m_CurrentSkillMenuButtonsMenu;

    public TextMeshProUGUI m_DescriptionText;
    public int m_SkillBoardPointerPosition;

    public Vector3 m_CenterCardPosition;
    // Use this for initialization
    void Start ()
    {
        m_SkillBoardPointerPosition = 0;
        m_CenterCardPosition = new Vector3(-38, -211, 0);

    }
	
	// Update is called once per frame
	void Update ()
    {



        if (m_InputActive == true)
        {
         //
         // if (Constants.Constants.m_XboxController == true)
         // {
         //     GameManager.Instance.m_InputManager.SetXboxAxis
         //     (MoveCommandBoardPositionUp, "Xbox_DPadX", false, ref GameManager.Instance.m_InputManager.m_DPadY);
         //     GameManager.Instance.m_InputManager.SetXboxAxis
         //         (MoveCommandBoardPositionDown, "Xbox_DPadX", true, ref GameManager.Instance.m_InputManager.m_DPadY);
         //
         //     GameManager.Instance.m_InputManager.SetXboxButton
         //         (SetSkill, "Xbox_A", ref GameManager.Instance.m_InputManager.m_AButton);
         //
         //     if (Input.GetButton("Xbox_B"))
         //     {
         //         GameManager.Instance.UiManager.PopScreen();
         //     }
         // }
         // if (Constants.Constants.m_PlaystationController == true)
         // {
         //     GameManager.Instance.m_InputManager.SetXboxAxis
         //     (MoveCommandBoardPositionUp, "Ps4_DPadX", false, ref GameManager.Instance.m_InputManager.m_DPadY);
         //     GameManager.Instance.m_InputManager.SetXboxAxis
         //         (MoveCommandBoardPositionDown, "Ps4_DPadX", true, ref GameManager.Instance.m_InputManager.m_DPadY);
         //
         //     GameManager.Instance.m_InputManager.SetXboxButton
         //         (SetSkill, "Ps4_Cross", ref GameManager.Instance.m_InputManager.m_AButton);
         //
         //     if (Input.GetButton("Ps4_Circle"))
         //     {
         //         GameManager.Instance.UiManager.PopScreen();
         //     }
         // }
        }
    }

    public void SetSkill()
    {
        GameManager.Instance.BattleCamera.SetAttackPhase(m_SkillBoardCreature.m_Skills[m_SkillBoardPointerPosition]);
        GameManager.Instance.UiManager.PopAllInvisivble();
    }

    public override void OnPop()
    {
        base.OnPop();

    }

    public void SpawnSkills(Creatures aCreatures)
    {
        if (m_CurrentSkillMenuButtonsMenu.Count > 0)
        {
            for (int i = 0; i < m_SkillBoardCreature.m_Skills.Count; i++)
            {

                Destroy(m_CurrentSkillMenuButtonsMenu[i].gameObject);
                m_CurrentSkillMenuButtonsMenu.RemoveAt(i);
            }
        }

        m_SkillBoardCreature = aCreatures;

        for (int i = 0; i < m_SkillBoardCreature.m_Skills.Count; i++)
        {
            m_CurrentSkillMenuButtonsMenu.Add(Instantiate<ButtonSkillWrapper>(m_ButtonReference, gameObject.transform));
            m_CurrentSkillMenuButtonsMenu[i].SetupButton(m_SkillBoardCreature, m_SkillBoardCreature.m_Skills[i], this);
            m_CurrentSkillMenuButtonsMenu[i].gameObject.transform.SetParent(this.transform, false);

            m_CurrentSkillMenuButtonsMenu[i].gameObject.transform.position = new Vector3(200 + i * 100, 200, 0);

        }

        AnimatedCardMovementToCenter(m_CurrentSkillMenuButtonsMenu[0]);
    }

    public void AnimatedCardMovementToCenter(ButtonSkillWrapper a_SkillWrapper)
    {
        //a_SkillWrapper.transform.position = 

       // m_DescriptionText.text = a_SkillWrapper.m_ButtonSkill.SkillDescription;
    }

    public void MoveCommandBoardPositionUp()
    {
        m_SkillBoardPointerPosition++;

        if (m_SkillBoardPointerPosition < 0)
        {
            m_SkillBoardPointerPosition = m_CurrentSkillMenuButtonsMenu.Count - 1;
        }
        else if (m_SkillBoardPointerPosition > m_CurrentSkillMenuButtonsMenu.Count -1)
        {
            m_SkillBoardPointerPosition = 0;
        }
        AnimatedCardMovementToCenter(m_CurrentSkillMenuButtonsMenu[m_SkillBoardPointerPosition]);
    }

    public void MoveCommandBoardPositionDown()
    {
        m_SkillBoardPointerPosition--;

        if (m_SkillBoardPointerPosition < 0)
        {
            m_SkillBoardPointerPosition = m_CurrentSkillMenuButtonsMenu.Count - 1;
        }
        else if (m_SkillBoardPointerPosition > m_CurrentSkillMenuButtonsMenu.Count - 1)
        {
            m_SkillBoardPointerPosition = 0;
        }
        m_CurrentSkillMenuButtonsMenu[m_SkillBoardPointerPosition].gameObject.transform.localScale = new Vector3(1, 1, 1);
    }
}
