using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class ButtonSkillWrapper : MonoBehaviour
{


    public Creatures m_ButtonTurnHolder;

    private List<Creatures> m_ListReference;
    public List<Material> m_ElementIconsList;
    public List<Material> m_CardDesigns;

    public Skills.ElementalType m_ElementalIconType;
    public Skills.SkillType m_SkillType;

    public Skills m_ButtonSkill;
    public UiSkillBoard m_SkillBoard;
    public Button m_Button;
    public TextMeshProUGUI m_CostToUseText;
    public TextMeshProUGUI m_Text_NameOfSkill;
    public RawImage m_Image_CardDesign;
    public Image m_Image_ElementalIcon;

    public string m_NameOfSkill;


    Color m_Color_TransparentWhite;
    Color m_Color_White;

    // Use this for initialization
    void Start()
    {
        // m_Color_TransparentWhite = new Color(1, 1, 1, 0.5f);
        // m_Color_White = new Color(1, 1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // SetElementalIcon(m_ElementalIconType);
        // SetCardDesign(m_SkillType);
        if (m_SkillBoard != null)
        {
            //  m_ButtonText.text = m_ButtonSkill.GetSkillName();
            //  m_CostToUseText.text = m_ButtonSkill.GetCostToUse().ToString();

            //if (m_ButtonTurnHolder != null)
            //{
            //    if (m_ButtonTurnHolder.CurrentMana <= m_ButtonSkill.GetCostToUse())
            //    {
            //        m_Text_NameOfSkill.color = m_Color_TransparentWhite;
            //    }
            //    else if (m_ButtonTurnHolder.CurrentMana >= m_ButtonSkill.GetCostToUse())
            //    {
            //        m_Text_NameOfSkill.color = m_Color_White;
            //    }
            //}
        }
    }


    public void SetElementalIcon(Skills.ElementalType aSkills, string sourceName = "Global")
    {
        m_Image_ElementalIcon.material = m_ElementIconsList[(int)aSkills];
    }

    public void SetCardDesign(Skills.SkillType aSkills, string sourceName = "Global")
    {
        m_Image_CardDesign.material = m_CardDesigns[(int)aSkills];
    }



    public void SetupButton(Creatures a_TurnHolder, Skills a_Skill, UiSkillBoard aSkillBoard)
    {
        m_ButtonTurnHolder = a_TurnHolder;
        m_ButtonSkill = a_Skill;
        m_SkillType = a_Skill.GetSkillType();
        SetCardDesign(m_SkillType);
        m_SkillBoard = aSkillBoard;
        SetElementalIcon(a_Skill.GetElementalType());
        m_Text_NameOfSkill.text = a_Skill.SkillName;
        m_CostToUseText.text = a_Skill.m_CostToUse.ToString();
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
