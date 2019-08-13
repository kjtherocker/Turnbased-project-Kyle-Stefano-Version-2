using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_SkillList : MonoBehaviour
{
    public enum Skills
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

    //public List<Script_Skills> m_SkillTypes;
    public Script_Skills m_Skill;

    public Dictionary<int, Script_Skills> m_SkillTypes = new Dictionary<int, Script_Skills>();
    // Use this for initialization
    void Start ()
    {
        Script_GameManager.Instance.SkillList = this;
        m_Skill = new Script_Poison();
        
        //AlimentEffects
        m_SkillTypes.Add((int)Skills.Poison, new Script_Poison());
        m_SkillTypes.Add((int)Skills.Rage, new Script_Rage());
        m_SkillTypes.Add((int)Skills.Sleep, new Script_Sleep());

        //Attack
        m_SkillTypes.Add((int)Skills.Attack, new Script_Attack());
        m_SkillTypes.Add((int)Skills.FireBall, new Script_FireBall());
        m_SkillTypes.Add((int)Skills.icerain, new Script_IceRain());
        m_SkillTypes.Add((int)Skills.LightRay, new Script_LightRay());
        m_SkillTypes.Add((int)Skills.ShadowBlast, new Script_ShadowBlast());


        //Buff

        m_SkillTypes.Add((int)Skills.Invigorate, new Script_Invigorate());

        //ReBuff

        m_SkillTypes.Add((int)Skills.Restrict, new Script_Restrict());

        //Heal

        m_SkillTypes.Add((int)Skills.HolyWater, new Script_HolyWater());

        //Resurrect

        m_SkillTypes.Add((int)Skills.PheonixSpirit, new Script_PhoenixSpirit());


        //BloodArt
        m_SkillTypes.Add((int)Skills.BloodRelief, new Script_BloodRelief());

        //m_SkillTypes.Add((int)Skills.Rage, new Script_Rage());




        for (int i = 0; i < m_SkillTypes.Count; i++)
        {
            m_SkillTypes[i].Start();
            
        }
       // Debug.Log(m_SkillTypes[(int)Skills.HolyWater].GetSkillType().ToString());
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_SkillTypes[10].Update();

    }

    public Script_Skills SetSkills( Skills aSkills, string sourceName = "Global")
    {
       return m_SkillTypes[(int)aSkills];
    }
}
