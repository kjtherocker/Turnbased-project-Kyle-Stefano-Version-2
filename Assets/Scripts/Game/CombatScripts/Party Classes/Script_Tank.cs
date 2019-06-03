using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Tank : Script_Creatures
{




    // Use this for initialization
    void Start()
    {
        CurrentHealth = 700;
        MaxHealth = 700;
        CurrentMana = 100;
        MaxMana = 200;
        Strength = 200;
        Magic = 300;
        Dexterity = 50;
        Speed = 4;
        Name = "Fide";

        AmountOfTurns = 1;


        SetCreature();

        m_Attack = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.Attack);

        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.HolyWater));
        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.ShadowBlast));
        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.PheonixSpirit));
        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.icerain));
        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.FireBall));

        m_BloodArts.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.BloodRelief));

        Model = (GameObject)Resources.Load("Objects/Battle/PartyModels/Fide/fide", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_Knight", typeof(Material));



        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Fire;
        elementalWeakness = ElementalWeakness.Null;

    }
}
