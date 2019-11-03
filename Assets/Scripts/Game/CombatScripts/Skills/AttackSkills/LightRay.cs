using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRay : Skills
{



    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Light;
        m_SkillType = SkillType.Attack;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_SkillParticleEffect = (ParticleSystem)Resources.Load("ParticleSystems/Waves/LightWave/ParticleEffect_LightWave", typeof(ParticleSystem));
        m_CostToUse = 40;
        m_Damage = 10;
        SkillName = "Light Ray";
        SkillDescription = "a Ray that will hit the whole enemy team";
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
