using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_ShadowBlast : Script_Skills
{


    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Shadow;
        m_SkillType = SkillType.Attack;
        m_SkillRange = SkillRange.SingleNode;
        m_Damagetype = DamageType.Magic;
        
        m_CostToUse = 40;
        m_Damage = 10;
        SkillName = "Shadow Blast";
        SkillDescription = "Blast that will hit the whole enemy team";
        m_SkillParticleEffect = (ParticleSystem)Resources.Load("ParticleSystems/Waves/DarkWave/ParticleEffect_DarkWave", typeof(ParticleSystem));
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