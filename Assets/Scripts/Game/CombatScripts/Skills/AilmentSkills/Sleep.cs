using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : Skills
{


    // Use this for initialization
    public override void Start()
    {

        //m_ElementalType = ElementalType.Water;
        m_SkillType = SkillType.Defence;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_SkillAilment = SkillAilment.Sleep;
        m_Damage = 0;
        m_CostToUse = 35;
        SkillName = "Sleep";
        SkillDescription = "Try to make the enemy party Sleep";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override int UseSkill(int BonusDamage)
    {
        return m_Damage;
    }
}
