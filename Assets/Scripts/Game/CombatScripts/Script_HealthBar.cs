using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_HealthBar : MonoBehaviour
{
    public Image Image_Healthbar;
    public Text Text_HealthRatio;

    public Image Image_Manahbar;
    public Text Text_ManaRatio;

    public Script_Creatures Partymember;

    public int m_CurrentHealth = 150;
    private int m_MaxHealth = 150;

    public int m_CurrentMana = 150;
    private int m_MaxMana = 150;


    // Use this for initialization
    void Start ()
    {
        UpdateHealthbar();

    }

    private void Update()
    {
        if (Partymember != null)
        {
            m_CurrentHealth = Partymember.CurrentHealth;
            m_MaxHealth = Partymember.MaxHealth;

            m_CurrentMana = Partymember.CurrentMana;
            m_MaxMana = Partymember.MaxMana;
        }
        else
        {
           
        }


        UpdateHealthbar();
    }
    // Update is called once per frame
    void UpdateHealthbar ()
    {
        if (m_CurrentHealth <= 0)
        {
            m_CurrentHealth = 0;
        }

        if (m_CurrentHealth >= m_MaxHealth)
        {
            m_CurrentHealth = m_MaxHealth;
        }

        if (m_CurrentMana <= 0)
        {
            m_CurrentMana = 0;
        }

        if (m_CurrentMana >= m_MaxMana)
        {
            m_CurrentMana = m_MaxMana;
        }

        float ManaRatio = (float)m_CurrentMana / (float)m_MaxMana;
        Image_Manahbar.rectTransform.localScale = new Vector3(ManaRatio * 0.65765f, 0.188084f, 0.188084f);
        Text_ManaRatio.text = (m_CurrentMana).ToString();

        float HealthRatio = (float)m_CurrentHealth / (float)m_MaxHealth;
        Image_Healthbar.rectTransform.localScale = new Vector3(HealthRatio * 0.65765f, 0.188084f, 0.188084f);
        Text_HealthRatio.text = (m_CurrentHealth).ToString();

    }

    private void TakeDamage(int damage)
    {
        m_CurrentHealth -= damage;


    }

    private void HealDamage(int heal)
    {
        m_CurrentHealth += heal;

    }


}
