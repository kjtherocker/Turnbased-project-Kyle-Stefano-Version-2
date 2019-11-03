using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : Skills
{



    // Use this for initialization
    public override void Start()
    {

        //m_ElementalType = ElementalType.Water;
        m_SkillType = SkillType.Defence;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_SkillAilment = SkillAilment.Rage;
        m_Damage = 0;
        m_CostToUse = 75;
        SkillName = "Rage";
        SkillDescription = "Make the enemy unable to do anything but attack";
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
