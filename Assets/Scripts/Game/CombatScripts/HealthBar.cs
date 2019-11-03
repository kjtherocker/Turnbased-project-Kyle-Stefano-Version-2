using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Image Image_Healthbar;
    public TextMeshProUGUI Text_HealthRatio;

    public Image Image_Manahbar;
    public TextMeshProUGUI Text_ManaRatio;

    public TextMeshProUGUI Text_Name;
    public Image Image_Portrait;
    public Creatures Partymember;

    public Camera m_PortraitCamera;

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

    public void SetCharacter(Creatures Character)
    {
        if (Partymember != null)
        {
            Partymember.gameObject.layer = 0;

        }

        Partymember = Character;

        m_CurrentHealth = Partymember.CurrentHealth;
        m_MaxHealth = Partymember.MaxHealth;

        m_CurrentMana = Partymember.CurrentMana;
        m_MaxMana = Partymember.MaxMana;

      
        

        if (Text_Name != null)
        {
            Text_Name.text = Partymember.Name;
        }
    }

    private void Update()
    {
        UpdateHealthbar();

        if (Partymember != null)
        {

            m_CurrentHealth = Partymember.CurrentHealth;
            m_MaxHealth = Partymember.MaxHealth;

            m_CurrentMana = Partymember.CurrentMana;
            m_MaxMana = Partymember.MaxMana;

            m_PortraitCamera.gameObject.transform.position = new Vector3(Partymember.ModelInGame.transform.position.x,
            Partymember.ModelInGame.transform.position.y + 1.7f, Partymember.ModelInGame.transform.position.z) +
             Partymember.ModelInGame.transform.forward;
            m_PortraitCamera.gameObject.transform.rotation = Partymember.ModelInGame.transform.rotation;

            Quaternion Rotation = Partymember.ModelInGame.transform.rotation;

            m_PortraitCamera.transform.eulerAngles = new Vector3(Rotation.eulerAngles.x, Rotation.eulerAngles.y + 180, Rotation.eulerAngles.z);
        }
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
            Image_Manahbar.fillAmount = ManaRatio;
        }

        float HealthRatio = (float)m_CurrentHealth / (float)m_MaxHealth;
        Image_Healthbar.fillAmount = HealthRatio;

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
