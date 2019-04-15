using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_RedEyes : Script_Creatures
{

    // Use this for initialization
    void Start()
    {
        CurrentHealth = 2000;
        MaxHealth = 2000;
        CurrentMana = 10;
        MaxMana = 10;
        Strength = 75;
        Magic = 250;
        Dexterity = 10;
        Speed = 10;
        Name = "Red Eyes";

        AmountOfTurns = 2;

        m_DomainStages = DomainStages.Encroaching;

        SetCreature();

        m_Attack = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.Attack);

        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.icerain));
        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.LightRay));
        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.FireBall));
        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.ShadowBlast));
        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.Invigorate));
  

        Model = (GameObject)Resources.Load("Prefabs/Battle/Enemy/Forest/Bosses/Prefab_RedEyes", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_RedEyes", typeof(Material));

        m_Domain = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.RedEyesEncroach);

        charactertype = Charactertype.Enemy;
        elementalStrength = ElementalStrength.Fire;
        elementalWeakness = ElementalWeakness.Water;
    }

    public override int EnemyAi()
    {
        int SkillChosen = Random.Range(0, m_Skills.Count);

        return SkillChosen;
    }

}