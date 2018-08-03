using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_FireBall : Script_Skills {

	// Use this for initialization
	void Start ()
    {

        m_ElementalType = ElementalType.Fire;
        m_SkillType = SkillType.Attack;
        m_SkillRange = SkillRange.FullTarget;
        m_Damagetype = DamageType.Magic;
        m_CostToUse = 40;
        m_Damage = 10;
        SkillName = "FireBall";
        SkillDescription = "FireBall that will hit the whole enemy team";
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public override int UseSkill(int BonusDamage)
    {
        int CulmativeDamage = m_Damage + BonusDamage / 3;

        return CulmativeDamage;
    }
}
