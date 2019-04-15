using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PassTurn : Script_Skills
{



    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Null;
        m_SkillType = SkillType.Extra;
        m_SkillRange = SkillRange.FullTarget;
        m_Damagetype = DamageType.Magic;
        m_Damage = 0;
        m_CostToUse = 0;
        SkillName = "Pass Turn";
        SkillDescription = "Skip out on the current turn";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override int UseSkill(int BonusDamage)
    {
        return 0;
    }
}
