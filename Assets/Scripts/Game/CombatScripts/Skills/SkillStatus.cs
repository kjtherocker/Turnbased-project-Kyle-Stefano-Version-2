using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillStatus : MonoBehaviour
{

    public Skills m_Skill;

    public TextMeshProUGUI m_SkillName;
    public TextMeshProUGUI m_SkillElement;
    public TextMeshProUGUI m_SkillRange;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_SkillName.text = m_Skill.GetSkillName();
        m_SkillElement.text = m_Skill.GetElementalType().ToString();
        m_SkillRange.text = m_Skill.GetSkillRange().ToString();


    }
}
