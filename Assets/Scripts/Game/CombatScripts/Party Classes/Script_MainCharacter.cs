using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Script_MainCharacter : Script_Creatures {


   

	// Use this for initialization
	void Start ()
    {
        CurrentHealth = 700;
        MaxHealth = 700;
        CurrentMana = 100;
        MaxMana = 200 ;
        Strength = 200;
        Magic = 300;
        Dexterity = 50;
        Speed = 50;
        Name = "Knight";

        AmountOfTurns = 1;

        //m_BaseSkill = new Script_Skills();
        //

        m_Skills = new Script_Skills[5];
        m_BloodArts = new Script_Skills[5];
        m_Attack = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.Attack);

        m_Skills[0] = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.Attack);
        m_Skills[1] = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.HolyWater);
        m_Skills[2] = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.PheonixSpirit);
        m_Skills[3] = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.icerain);
        m_Skills[4] = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.ShadowBlast);

       


        m_BloodArts[0] = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.BloodRelief);

        SetCreature();

        Model = (GameObject)Resources.Load("Prefabs/Battle/PartyModels/Main_Character", typeof(GameObject));
        
        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_Knight", typeof(Material));



        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Fire;
        elementalWeakness = ElementalWeakness.Null;

    }
	
	// Update is called once per frame

}
