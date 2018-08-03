using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Invigorate : Script_Skills
{

    // Use this for initialization
    void Start()
    {

        m_ElementalType = ElementalType.Water;
        m_SkillType = SkillType.Buff;
        m_SkillRange = SkillRange.FullTarget;
        m_Damagetype = DamageType.Magic;
        m_Damage = 2;
        m_CostToUse = 75;
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
