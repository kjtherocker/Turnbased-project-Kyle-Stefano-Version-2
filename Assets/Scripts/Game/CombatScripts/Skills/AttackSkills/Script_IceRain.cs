using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_IceRain : Script_Skills
{

    // Use this for initialization
    void Start()
    {

        m_ElementalType = ElementalType.Water;
        m_SkillType = SkillType.Attack;
        m_SkillRange = SkillRange.FullTarget;
        m_CostToUse = 40;
        m_Damage = 5;
        SkillName = "IceRain";
        SkillDescription = "IceRain that will hit the whole enemy team";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override int UseSkill(int BonusDamage)
    {
        int CulmativeDamage = m_Damage + BonusDamage / 3;

        return CulmativeDamage;
    }
}