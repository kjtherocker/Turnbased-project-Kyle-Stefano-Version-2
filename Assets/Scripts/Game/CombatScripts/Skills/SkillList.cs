using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillList : MonoBehaviour
{
    public enum SkillEnum
    {
        Poison,
        Rage,
        Sleep,
        Attack,
        FireBall,
        icerain,
        LightRay,
        ShadowBlast,
        BloodRelief,
        Invigorate,
        Restrict,
        HolyWater,
        PheonixSpirit,

        NumberOfSkills
    }

    //public List<Skills> m_SkillTypes;

    public Dictionary<int, Skills> m_SkillTypes = new Dictionary<int, Skills>();
    // Use this for initialization
    void Start ()
    {
        GameManager.Instance.SkillList = this;
        
        //AlimentEffects
        m_SkillTypes.Add((int)SkillEnum.Poison, new Poison());
        m_SkillTypes.Add((int)SkillEnum.Rage, new Rage());
        m_SkillTypes.Add((int)SkillEnum.Sleep, new Sleep());

        //Attack
        m_SkillTypes.Add((int)SkillEnum.Attack, new Attack());
        m_SkillTypes.Add((int)SkillEnum.FireBall, new FireBall());
        m_SkillTypes.Add((int)SkillEnum.icerain, new IceRain());
        m_SkillTypes.Add((int)SkillEnum.LightRay, new LightRay());
        m_SkillTypes.Add((int)SkillEnum.ShadowBlast, new ShadowBlast());


        //Buff

        m_SkillTypes.Add((int)SkillEnum.Invigorate, new Invigorate());

        //ReBuff

        m_SkillTypes.Add((int)SkillEnum.Restrict, new Restrict());

        //Heal

        m_SkillTypes.Add((int)SkillEnum.HolyWater, new HolyWater());

        //Resurrect

        m_SkillTypes.Add((int)SkillEnum.PheonixSpirit, new PhoenixSpirit());


        //BloodArt
        m_SkillTypes.Add((int)SkillEnum.BloodRelief, new BloodRelief());

        //m_SkillTypes.Add((int)Skills.Rage, new Rage());




        for (int i = 0; i < m_SkillTypes.Count; i++)
        {
            m_SkillTypes[i].Start();
            
        }
       // Debug.Log(m_SkillTypes[(int)Skills.HolyWater].GetSkillType().ToString());
    }
	


    public Skills SetSkills(SkillEnum aSkills, string sourceName = "Global")
    {
       return m_SkillTypes[(int)aSkills];
    }
}
