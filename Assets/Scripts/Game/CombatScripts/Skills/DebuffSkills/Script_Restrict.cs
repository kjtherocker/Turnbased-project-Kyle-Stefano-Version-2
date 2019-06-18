using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Restrict : Script_Skills
{



    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Shadow;
        m_SkillType = SkillType.Debuff;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_Damage = 3;
        m_CostToUse = 60;
        SkillName = "Restrict";
        SkillDescription = "Make the enemies damage be weaker";
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