using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fide : Creatures
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

        m_Attack = GameManager.Instance.SkillList.SetSkills(SkillList.SkillEnum.Attack);

        m_Skills.Add(GameManager.Instance.SkillList.SetSkills(SkillList.SkillEnum.HolyWater));
        m_Skills.Add(GameManager.Instance.SkillList.SetSkills(SkillList.SkillEnum.ShadowBlast));
        m_Skills.Add(GameManager.Instance.SkillList.SetSkills(SkillList.SkillEnum.PheonixSpirit));
        m_Skills.Add(GameManager.Instance.SkillList.SetSkills(SkillList.SkillEnum.icerain));
        m_Skills.Add(GameManager.Instance.SkillList.SetSkills(SkillList.SkillEnum.FireBall));

        m_BloodArts.Add(GameManager.Instance.SkillList.SetSkills(SkillList.SkillEnum.BloodRelief));

        Model = (GameObject)Resources.Load("Objects/Battle/PartyModels/Fide/Pref_Fide", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_Knight", typeof(Material));



        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Fire;
        elementalWeakness = ElementalWeakness.Null;

    }
}
