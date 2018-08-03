using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Skills : MonoBehaviour {

    // Use this for initialization


    public enum ElementalType
    {
        Null,
        Fire,
        Water,
        Wind,
        Lighting,
        Shadow,
        Light
    }

    public enum SkillType
    {
        Attack,
        Heal,
        Buff,
        Debuff,


    }
    public enum DamageType
    {
        Strength,
        Magic

    }

    public enum SkillRange
    {
        SingleTarget,
        FullTarget
    }


    protected ElementalType m_ElementalType;
    protected SkillType m_SkillType;
    protected SkillRange m_SkillRange;
    protected DamageType m_Damagetype;

    protected string SkillName;
    protected string SkillDescription;

    protected int m_CostToUse;

    protected int m_Damage;

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


}
