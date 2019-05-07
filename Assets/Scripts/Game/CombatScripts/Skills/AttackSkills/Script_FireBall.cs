using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_FireBall : Script_Skills
{



    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Fire;
        m_SkillAilment = SkillAilment.Poison;
        m_SkillType = SkillType.Attack;
        m_SkillRange = SkillRange.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_SkillParticleEffect = (ParticleSystem)Resources.Load("ParticleSystems/Waves/FireWave/ParticleEffect_FireWave", typeof(ParticleSystem));
        m_CostToUse = 40;
        m_Damage = 10;
        SkillName = "FireBall";
        SkillDescription = "FireBall that will hit the whole enemy team";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override int UseSkill(int BonusDamage)
    {
        int CulmativeDamage = m_Damage + BonusDamage / 3;

        return CulmativeDamage;
    }
}
