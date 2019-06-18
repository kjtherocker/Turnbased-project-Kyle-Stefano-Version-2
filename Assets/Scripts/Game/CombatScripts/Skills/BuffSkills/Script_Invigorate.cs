using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Invigorate : Script_Skills
{



    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Water;
        m_SkillType = SkillType.Buff;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_Damage = 3;
        m_CostToUse = 60;
        SkillName = "Invigorate";
        SkillDescription = "A buff that strongly increases damage";
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
