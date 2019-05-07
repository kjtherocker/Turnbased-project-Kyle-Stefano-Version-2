using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Script_Skills
{

    // Use this for initialization


    public enum ElementalType
    {
        Null,
        Fire,
        Water,
        Wind,
        Lighting,
        Shadow,
        Light,
        Blood
    }

    public enum SkillType
    {
        Attack,
        Heal,
        Buff,
        Debuff,
        Extra,
        Resurrect,
        Aliment,
        Blood


    }
    public enum DamageType
    {
        Strength,
        Magic

    }

    public enum SkillRange
    {
        SingleNode,
        FourInAStraightLine,
        BrickOf4,
    }

    public enum SkillAilment
    {
        None,
        Poison,
        Daze,
        Sleep,
        Rage,

    }

    protected ElementalType m_ElementalType;
    protected SkillType m_SkillType;
    protected SkillRange m_SkillRange;
    protected DamageType m_Damagetype;
    protected SkillAilment m_SkillAilment;

    protected string SkillName;
    protected string SkillDescription;

    protected ParticleSystem m_SkillParticleEffect;

    protected int m_CostToUse;

    protected int m_Damage;


    public virtual void Start()
    {
        
    }

  
    public ParticleSystem GetSkillParticleEffect()
    {
        return m_SkillParticleEffect;
    }

    public int GetCostToUse()
    {
        return m_CostToUse;
    }

    public string GetSkillName()
    {
        return SkillName;
    }

    public string GetSkillDescription()
    {
        return SkillDescription;
    }

    public DamageType GetDamageType()
    {
        return m_Damagetype;
    }

    public ElementalType GetElementalType()
    {
        return m_ElementalType;
    }

    public SkillAilment GetAlimentType()
    {
        return m_SkillAilment;
    }

    public SkillType GetSkillType()
    {
        return m_SkillType;
    }

    public SkillRange GetSkillRange()
    {
        return m_SkillRange;
    }

    virtual public int UseSkill(int BonusDamage)
    {
        return m_Damage;
    }

    public int GetSkillDamage()
    {
        return m_Damage;
    }

}
