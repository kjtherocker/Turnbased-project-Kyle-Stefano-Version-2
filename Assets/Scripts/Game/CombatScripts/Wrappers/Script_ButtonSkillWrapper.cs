using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Script_ButtonSkillWrapper : MonoBehaviour
{
    public Script_Creatures m_ButtonTurnHolder;
    private List<Script_Creatures> m_ListReference;
    public Script_Skills m_ButtonSkill;
    public UiSkillBoard m_SkillBoard;
    public Button m_Button;
    public TextMeshProUGUI m_ButtonText;
    public TextMeshProUGUI m_CostToUseText;
    int m_SkillNumber;
    Color m_Color_TransparentWhite;
    Color m_Color_White;

    // Use this for initialization
    void Start ()
    {
        m_Color_TransparentWhite = new Color(1, 1, 1, 0.5f);
        m_Color_White = new Color(1, 1, 1, 1);
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (m_SkillBoard != null)
        {
            m_ButtonText.text = m_ButtonSkill.GetSkillName();
            m_CostToUseText.text = m_ButtonSkill.GetCostToUse().ToString();

            if (m_ButtonTurnHolder.CurrentMana <= m_ButtonSkill.GetCostToUse())
            {
                m_ButtonText.color = m_Color_TransparentWhite;
            }
            else if (m_ButtonTurnHolder.CurrentMana >= m_ButtonSkill.GetCostToUse())
            {
                m_ButtonText.color = m_Color_White;
            }
        }
    }

    public void SetupButton(Script_Creatures a_TurnHolder, Script_Skills a_Skill, int a_Skillnumber, UiSkillBoard aSkillBoard)
    {
        m_ButtonTurnHolder = a_TurnHolder;
        m_ButtonSkill = a_Skill;
        m_SkillNumber = a_Skillnumber;
        m_SkillBoard = aSkillBoard;
        
    }

    public void SetAsNotInteractable()
    {
        m_Button.interactable = false;
    }

    public void HoveringOverButton()
    {
        m_SkillBoard.m_DescriptionText.text = m_ButtonSkill.GetSkillDescription();
    }

    public void ButtonClick()
    {
        if (m_ButtonTurnHolder.CurrentMana >= m_ButtonSkill.GetCostToUse() || m_ListReference.Count >= 0)
        {
           // m_CombatManagerRefrence.SetBattleStateToSelect();
           // m_CombatManagerRefrence.SetTurnHolderSkills(m_SkillNumber);
        }
    }

    public void ToDestroy()
    {
        Destroy(gameObject);
    }
}
