using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PhoenixSpirit : Script_Skills
{
    // Use this for initialization
    void Start()
    {

        m_ElementalType = ElementalType.Light;
        m_SkillType = SkillType.Resurrect;
        m_SkillRange = SkillRange.SingleTarget;
        m_Damagetype = DamageType.Magic;
        m_Damage = 0;
        m_CostToUse = 40;
        SkillName = "Phoenix Spirit";
        SkillDescription = "Resurrect one dead party member";
    }

    // Update is called once per frame
    void Update()
    {

    }

}

