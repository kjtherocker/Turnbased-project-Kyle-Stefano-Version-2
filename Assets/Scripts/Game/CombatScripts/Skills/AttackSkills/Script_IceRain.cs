using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_IceRain : Script_Skills
{



    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Water;
        m_SkillType = SkillType.Attack;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_CostToUse = 40;
        m_Damage = 5;
        m_SkillParticleEffect = (ParticleSystem)Resources.Load("ParticleSystems/Waves/IceWave/ParticleEffect_IceWave", typeof(ParticleSystem));
        SkillName = "IceRain";
        SkillDescription = "IceRain that will hit the whole enemy team";
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