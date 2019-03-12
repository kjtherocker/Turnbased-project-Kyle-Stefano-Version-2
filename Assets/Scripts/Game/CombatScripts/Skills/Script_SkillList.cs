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
        RedEyesEncroach,
        HolyWater,
        PheonixSpirit,


        NumberOfSkills
    }

    public Script_Skills[] m_SkillTypes;
    public Script_Skills m_Skill;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public  Script_Skills SetSkills( Skills aPortrait, string sourceName = "Global")
    {
       return m_SkillTypes[(int)aPortrait];
    }
}
