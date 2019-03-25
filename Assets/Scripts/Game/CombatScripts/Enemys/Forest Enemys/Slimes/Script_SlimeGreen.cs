using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_SlimeGreen : Script_Creatures
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
        Name = "Green Slime";

        m_Skills = new Script_Skills[5];
        m_BloodArts = new Script_Skills[5];
        m_Attack = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.Attack);

        m_Skills[0] = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.Attack);
        m_Skills[1] = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.FireBall);
        m_Skills[2] = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.FireBall);
        m_Skills[3] = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.Attack);
        m_Skills[4] = Script_GameManager.Instance.SkillList.SetSkills(Script_SkillList.Skills.Attack);

        AmountOfTurns = 1;

        Model = (GameObject)Resources.Load("Prefabs/Battle/Enemy/Forest/Slime/Prefab_SlimeGreen", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_GreenSlime", typeof(Material));

        charactertype = Charactertype.Enemy;
        elementalStrength = ElementalStrength.Water;
        elementalWeakness = ElementalWeakness.Fire;
    }

    public override int EnemyAi()
    {
        int SkillChosen = Random.Range(0, m_Skills.Length);

        return SkillChosen;
    }

}
