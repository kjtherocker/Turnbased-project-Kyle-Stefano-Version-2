using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Poison : Script_Skills
{


        // Use this for initialization
    public override void Start()
    {

        //m_ElementalType = ElementalType.Water;
        m_SkillType = SkillType.Aliment;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_SkillAilment = SkillAilment.Poison;
        m_Damage = 0;
        m_CostToUse = 75;
        SkillName = "Poison";
        SkillDescription = "Try to Poison the enemy";
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
