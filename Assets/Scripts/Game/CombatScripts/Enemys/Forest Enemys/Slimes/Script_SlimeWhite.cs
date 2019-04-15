using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_SlimeWhite : Script_Creatures
{

    // Use this for initialization
    void Start()
    {
        CurrentHealth = 200;
        MaxHealth = 200;
        CurrentMana = 10;
        MaxMana = 10;
        Strength = 350;
        Magic = 350;
        Dexterity = 10;
        Speed = 10;
        Name = "White Slime";

        AmountOfTurns = 1;

        SetCreature();

        m_Attack = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.HolyWater);

        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.HolyWater));
        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.LightRay));
        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.LightRay));
        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.HolyWater));
        m_Skills.Add(Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.HolyWater));

        //"Prefabs/Battle/Enemy/Forest/model_Slime"

        Model = (GameObject)Resources.Load("Prefabs/Battle/Enemy/Forest/Slime/Prefab_SlimeWhite", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_WhiteSlime", typeof(Material));

        charactertype = Charactertype.Enemy;
        elementalStrength = ElementalStrength.Light;
        elementalWeakness = ElementalWeakness.Shadow;
    }

    public override int EnemyAi()
    {
        int SkillChosen = Random.Range(0, m_Skills.Count);

        return SkillChosen;
    }
}
