using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiSkillBoard : UiScreen
{

    public Script_Creatures m_SkillBoardCreature;
    public Script_ButtonSkillWrapper m_ButtonReference;
    public List<Script_ButtonSkillWrapper> m_CurrentSkillMenuButtonsMenu;

    public TextMeshProUGUI m_DescriptionText;
    public int m_SkillBoardPointerPosition;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("a") || Input.GetButtonDown("Xbox_A"))
        {
            m_CurrentSkillMenuButtonsMenu[m_SkillBoardPointerPosition].m_ButtonText.color = Color.red;
        }
       // m_CurrentSkillMenuButtonsMenu[m_SkillBoardPointerPosition].m_ButtonText.color = Color.red;

        if (m_SkillBoardPointerPosition < 0)
        {
            m_SkillBoardPointerPosition = m_CurrentSkillMenuButtonsMenu.Count - 1;
        }
        else if (m_SkillBoardPointerPosition > m_CurrentSkillMenuButtonsMenu.Count -1)
        {
            m_SkillBoardPointerPosition = 0;
        }

        Script_GameManager.Instance.m_InputManager.SetXboxAxis
            (MoveCommandBoardPositionUp, "Xbox_DPadY", false, ref Script_GameManager.Instance.m_InputManager.m_DPadY);
        Script_GameManager.Instance.m_InputManager.SetXboxAxis
            (MoveCommandBoardPositionDown, "Xbox_DPadY", true, ref Script_GameManager.Instance.m_InputManager.m_DPadY);

    }



    public void SpawnSkills(Script_Creatures aCreatures)
    {
        m_SkillBoardCreature = aCreatures;

        for (int i = 0; i < m_SkillBoardCreature.m_Skills.Count; i++)
        {
            m_CurrentSkillMenuButtonsMenu.Add(Instantiate<Script_ButtonSkillWrapper>(m_ButtonReference, gameObject.transform));
            m_CurrentSkillMenuButtonsMenu[i].gameObject.transform.position = new Vector3(262, 180 + i * 90, 0);
            m_CurrentSkillMenuButtonsMenu[i].SetupButton(m_SkillBoardCreature, m_SkillBoardCreature.m_Skills[i], i, this);


            m_CurrentSkillMenuButtonsMenu[i].gameObject.transform.SetParent(this.transform, false);
        }
    }

    public void MoveCommandBoardPositionUp()
    {
        m_SkillBoardPointerPosition++;
    }

    public void MoveCommandBoardPositionDown()
    {
        m_SkillBoardPointerPosition--;
    }
}
