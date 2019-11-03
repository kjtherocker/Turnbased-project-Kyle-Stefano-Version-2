using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Sigma : Creatures {


   

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
        Name = "Sigma";

        AmountOfTurns = 1;


        SetCreature();

        m_CreatureMovement = 8;

        m_Attack = GameManager.Instance.SkillList.SetSkills(SkillList.SkillEnum.Attack);

        m_Skills.Add(GameManager.Instance.SkillList.SetSkills(SkillList.SkillEnum.HolyWater));
        m_Skills.Add(GameManager.Instance.SkillList.SetSkills(SkillList.SkillEnum.ShadowBlast));
        m_Skills.Add(GameManager.Instance.SkillList.SetSkills(SkillList.SkillEnum.PheonixSpirit));
        m_Skills.Add(GameManager.Instance.SkillList.SetSkills(SkillList.SkillEnum.icerain));

        Model = (GameObject)Resources.Load("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma", typeof(GameObject));
        
        m_Texture = (Material)Resources.Load("Objects/Portrait/Material_Knight", typeof(Material));



        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Fire;
        elementalWeakness = ElementalWeakness.Null;

    }
	
	// Update is called once per frame

}
