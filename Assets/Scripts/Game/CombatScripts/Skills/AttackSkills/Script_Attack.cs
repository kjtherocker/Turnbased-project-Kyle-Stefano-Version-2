using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Attack : Script_Skills
{



    public override void Start()
    {

        m_ElementalType = ElementalType.Null;
        m_SkillType = SkillType.Attack;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Strength;
        m_CostToUse = 0;
        m_Damage = 75;
        SkillName = "Attack";
        SkillDescription = "Attack a single enemy";
        m_AnimationName = "t_IsAttack";
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
