using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_ButtonEnemyWrapper : MonoBehaviour
{

    public Script_Creatures m_ButtonTurnHolder;
    public Script_CombatManager m_CombatManagerRefrence;
    public Script_HealthBar m_Healthbar;
    public int m_EnemyNumber;
    Color m_Color_TransparentWhite;
    Color m_Color_White;

    // Use this for initialization
    void Start()
    {
        m_Color_TransparentWhite = new Color(1, 1, 1, 0.5f);
        m_Color_White = new Color(1, 1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {

       
    }

    public void SetupButton(Script_Creatures a_ButtonHolder, int a_Enemynumber, Script_CombatManager a_CombatManager)
    {
        m_ButtonTurnHolder = a_ButtonHolder;
        m_CombatManagerRefrence = a_CombatManager;
        m_EnemyNumber = a_Enemynumber;
        m_Healthbar.Partymember = a_ButtonHolder;
    }

    public void ButtonClick()
    {
    }

    public void ToDestroy()
    {
        Destroy(gameObject);
    }
}