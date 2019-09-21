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
        Strength = 800;
        Magic = 300;
        Dexterity = 50;
        Speed = 4;
        Name = "Doll";

        AmountOfTurns = 1;


        SetCreature();

        m_CreatureMovement = 8;

        m_Attack = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.Attack);

        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.HolyWater));
        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.ShadowBlast));
        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.PheonixSpirit));
        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.icerain));

        Model = (GameObject)Resources.Load("Objects/Battle/PartyModels/Wretched Doll/Prefab/3D_Doll", typeof(GameObject));
        
        m_Texture = (Material)Resources.Load("Objects/Portrait/Material_Knight", typeof(Material));



        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Fire;
        elementalWeakness = ElementalWeakness.Null;

    }
	
	// Update is called once per frame

}
