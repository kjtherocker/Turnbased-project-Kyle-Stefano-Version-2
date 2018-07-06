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
        Support,
        Defense

    }

    public enum SkillRange
    {
        SingleTarget,
        FullTarget
    }


    protected ElementalType m_ElementalType;
    protected SkillType m_SkillType;
    protected SkillRange m_SkillRange;


    protected int m_CostToUse;



    public int GetCostToUse()
    {
        return m_CostToUse;
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

    virtual public void UseSkill()
    {

    }


}
