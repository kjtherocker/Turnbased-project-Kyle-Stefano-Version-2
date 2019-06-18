using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_BloodRelief : Script_Skills
{



    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Blood;
        m_SkillType = SkillType.Blood;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_SkillParticleEffect = (ParticleSystem)Resources.Load("ParticleSystems/Waves/DarkWave/ParticleEffect_DarkWave", typeof(ParticleSystem));
        m_CostToUse = 4;
        m_Damage = 4;
        SkillName = "Blood Relief";
        SkillDescription = "Sacrifice 25% of your current health for 25% mana gain";
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
