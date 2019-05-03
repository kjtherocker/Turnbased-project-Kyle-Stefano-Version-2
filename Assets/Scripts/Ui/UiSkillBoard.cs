using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSkillBoard : UiScreen
{

    public Script_Creatures m_SkillBoardCreature;
    public Script_ButtonSkillWrapper m_ButtonReference;


    public List<Script_ButtonSkillWrapper> m_CurrentSkillMenuButtonsMenu;

    private void OnEnable()
    {
        for (int i = 0; i < m_SkillBoardCreature.m_Skills.Count; i++)
        {
            m_CurrentSkillMenuButtonsMenu.Add(Instantiate<Script_ButtonSkillWrapper>(m_ButtonReference, gameObject.transform));
            m_CurrentSkillMenuButtonsMenu[i].gameObject.transform.position = new Vector3(-1540, 115 + i * 31, 0);
            m_CurrentSkillMenuButtonsMenu[i].SetupButton(m_SkillBoardCreature, m_SkillBoardCreature.m_Skills[i], i, Script_GameManager.Instance.CombatManager);


            m_CurrentSkillMenuButtonsMenu[i].gameObject.transform.SetParent(this.transform, false);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
