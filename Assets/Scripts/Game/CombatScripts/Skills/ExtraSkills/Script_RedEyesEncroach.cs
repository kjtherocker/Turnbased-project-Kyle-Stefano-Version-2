using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_RedEyesEncroach : Script_Skills
{


    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Null;
        m_SkillType = SkillType.Extra;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_SkillParticleEffect = (ParticleSystem)Resources.Load("ParticleSystems/Waves/FireWave/ParticleEffect_FireWave", typeof(ParticleSystem));
        m_Damage = 0;
        m_CostToUse = 0;
        SkillName = "EnroachDomain";
        SkillDescription = "Skip out on the current turn";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
