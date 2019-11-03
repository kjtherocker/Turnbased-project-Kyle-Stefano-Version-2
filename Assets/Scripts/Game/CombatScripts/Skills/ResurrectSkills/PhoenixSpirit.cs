using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoenixSpirit : Skills
{



    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Light;
        m_SkillType = SkillType.Heal;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_Damage = 0;
        m_CostToUse = 40;
        SkillName = "Phoenix Spirit";
        SkillDescription = "Resurrect one dead party member";
    }

}

