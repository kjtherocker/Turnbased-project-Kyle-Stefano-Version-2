using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Script_HealthBar : MonoBehaviour
{
    public RawImage Image_Healthbar;
    public TextMeshProUGUI Text_HealthRatio;

    public RawImage Image_Manahbar;
    public TextMeshProUGUI Text_ManaRatio;

    public TextMeshProUGUI Text_Strength;
    public TextMeshProUGUI Text_Magic;
    public TextMeshProUGUI Text_Dexterity;
    public TextMeshProUGUI Text_Speed ;

    public TextMeshProUGUI Text_Name;
    public TextMeshProUGUI Text_Buff;
    public Image Image_Portrait;
    public Script_Creatures Partymember;

    public int m_CurrentHealth = 150;
    private int m_MaxHealth = 150;

    public int m_CurrentMana = 150;
    private int m_MaxMana = 150;
    public bool m_IsSelected;

    // Use this for initialization
    void Start()
    {
        UpdateHealthbar();
        m_IsSelected = false;
    }

    private void Update()
    {
        if (Partymember != null)
        {
            m_CurrentHealth = Partymember.CurrentHealth;
            m_MaxHealth = Partymember.MaxHealth;

            m_CurrentMana = Partymember.CurrentMana;
            m_MaxMana = Partymember.MaxMana;

            Image_Portrait.material = Partymember.m_Texture;


            Text_Strength.text = Partymember.Strength.ToString();
            Text_Magic.text = Partymember.Magic.ToString();
            Text_Dexterity.text = Partymember.Dexterity.ToString();
            Text_Speed.text = Partymember.Speed.ToString();


            if (Text_Buff != null)
            {
                Text_Buff.text = Partymember.BuffandDebuff.ToString();

                if (Partymember.BuffandDebuff == 0)
                {
                    Text_Buff.gameObject.SetActive(false);
                }
                else
                {
                    Text_Buff.gameObject.SetActive(true);
                }
            }

            if (Text_Name != null)
            {
                Text_Name.text = Partymember.Name;
            }
        }
        else
        {

        }


        UpdateHealthbar();
    }
    // Update is called once per frame
    void UpdateHealthbar()
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

        if (Image_Manahbar != null)
        {
            float ManaRatio = (float)m_CurrentMana / (float)m_MaxMana;
            Image_Manahbar.rectTransform.localScale = new Vector3(ManaRatio * 0.65765f, 0.188084f, 0.188084f);
            Text_ManaRatio.text = (m_CurrentMana).ToString();
        }

        float HealthRatio = (float)m_CurrentHealth / (float)m_MaxHealth;
        Image_Healthbar.rectTransform.localScale = new Vector3(HealthRatio * 0.65765f, 0.188084f, 0.188084f);
        Text_HealthRatio.text = (m_CurrentHealth).ToString();

    }

    public void SetIsSelected(bool a_isselected)
    {
        m_IsSelected = a_isselected;
    }

    public bool GetIsSelected()
    {
        return m_IsSelected;
    }
    private void TakeDamage(int damage)
    {
        m_CurrentHealth -= damage;


    }

    private void HealDamage(int heal)
    {
        m_CurrentHealth += heal;

    }

    public void SetHealthBarPosition(int a_Minimum, int a_Maximum)
    {
        if (m_IsSelected == true)
        {
            gameObject.transform.position = new Vector3(a_Maximum, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else
        {
            gameObject.transform.position = new Vector3(a_Minimum, gameObject.transform.position.y, gameObject.transform.position.z);
        }

    }


}
