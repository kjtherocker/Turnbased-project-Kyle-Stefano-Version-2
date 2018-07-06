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
        m_CostToUse = 25;


    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public override void UseSkill()
    {

    }
}
