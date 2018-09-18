using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_PartyMenu : MonoBehaviour
{

    public Script_PartyManager m_PartyManager;
    public List<Button> m_MenuButtons;

    public List<Script_HealthBar> m_ReserveHealthBar;
    public List<Script_HealthBar> m_Healthbars;
    public Script_HealthBar m_HealthbarReference;
    public Script_HealthBar m_HealthbarReferenceReserve;
    public int m_CurrentParty;
    public int m_ReservePosition;

    bool IsReserveHealthBarsSpawned;

    public enum MenuState
    {
        Default,
        SelectingReserve,
        SelectingParty

    }

    public MenuState m_MenuState;

    // Use this for initialization
    void Start()
    {
        m_MenuState = MenuState.Default;
        IsReserveHealthBarsSpawned = false;

        for (int i = 0; i < 4; i++)
        {
            m_Healthbars.Add(Instantiate<Script_HealthBar>(m_HealthbarReference, gameObject.transform));
            m_Healthbars[i].gameObject.transform.localPosition = new Vector3(-200, -80 + i * 80, 0);
            m_Healthbars[i].Partymember = m_PartyManager.m_CurrentParty[i];
        }

    }

    // Update is called once per frame
    void Update()
    {
        m_Healthbars[0].Partymember = m_PartyManager.m_CurrentParty[0];
        m_Healthbars[1].Partymember = m_PartyManager.m_CurrentParty[1];
        m_Healthbars[2].Partymember = m_PartyManager.m_CurrentParty[2];
        m_Healthbars[3].Partymember = m_PartyManager.m_CurrentParty[3];

        if (Input.GetKeyDown("escape"))
        {
            m_MenuState = MenuState.Default;

        }

        if (m_MenuState == MenuState.SelectingReserve)
        {


            m_ReservePosition = NumberMinandMaxSetUp(m_ReservePosition, m_PartyManager.m_ReservePartymembers.Count);
            LoopingHealthbarIsSelected(m_ReserveHealthBar, m_ReservePosition, 420, 440);

            if (Input.GetKeyDown("q"))
            {
                m_MenuState = MenuState.SelectingParty;
            }
        }
        if (m_MenuState == MenuState.SelectingParty)
        {
            m_CurrentParty = NumberMinandMaxSetUp(m_CurrentParty, m_PartyManager.m_CurrentParty.Count);

            LoopingHealthbarIsSelected(m_Healthbars, m_CurrentParty, 80, 100);


            if (Input.GetKeyDown("w"))
            {
                m_MenuState = MenuState.Default;
                SwapReserveToParty();
                m_Healthbars[m_CurrentParty].gameObject.transform.position = new Vector3(100, m_Healthbars[m_CurrentParty].gameObject.transform.position.y, m_Healthbars[m_CurrentParty].gameObject.transform.position.z);

                for (int i = 0; i < m_PartyManager.m_CurrentParty.Count; i++)
                {
                    m_Healthbars[i].SetIsSelected(false);
                }
                for (int i = m_ReserveHealthBar.Count; i > 0; i--)
                {
                    Destroy(m_ReserveHealthBar[0].gameObject);
                    m_ReserveHealthBar.RemoveAt(0);
                }

                IsReserveHealthBarsSpawned = false;
            }
        }
        if (m_MenuState != MenuState.Default)
        {
            m_MenuButtons[0].gameObject.SetActive(false);
        }
        else
        {
            m_MenuButtons[0].gameObject.SetActive(true);
        }

        m_MenuButtons[0].onClick.AddListener(SetMenuStateToReserve);

    }

    public void LoopingHealthbarIsSelected(List<Script_HealthBar> a_Healthbarlist, int a_PositionInList, int a_MaxiumPos, int a_Minimumpos)
    {
        for (int i = 0; i < a_Healthbarlist.Count; i++)
        {
            if (i == a_PositionInList)
            {
                a_Healthbarlist[i].SetIsSelected(true);
            }
            else
            {
                a_Healthbarlist[i].SetIsSelected(false);
            }

            a_Healthbarlist[i].SetHealthBarPosition(a_Minimumpos, a_MaxiumPos);
        }

    }

    public int NumberMinandMaxSetUp(int a_ReferenceToNumber, int max)
    {
        if (Input.GetKeyDown("up"))
        {
            a_ReferenceToNumber++;
        }
        if (Input.GetKeyDown("down"))
        {
            a_ReferenceToNumber--;
        }

        if (a_ReferenceToNumber >= max)
        {
            a_ReferenceToNumber = 0;
        }
        else if (a_ReferenceToNumber < 0)
        {
            if (max == 0)
            {
                a_ReferenceToNumber = 0;
            }
            else
            {
                a_ReferenceToNumber = max - 1;
            }
        }

        return a_ReferenceToNumber;
    }

    void SetMenuStateToReserve()
    {
        if (IsReserveHealthBarsSpawned == false)
        {
            for (int i = 0; i < m_PartyManager.m_ReservePartymembers.Count; i++)
            {
                m_ReserveHealthBar.Add(Instantiate<Script_HealthBar>(m_HealthbarReference, gameObject.transform));
                m_ReserveHealthBar[i].gameObject.transform.localPosition = new Vector3(290, -80 + i * 80, 0);
                m_ReserveHealthBar[i].Partymember = m_PartyManager.m_ReservePartymembers[i];
            }
        }
        IsReserveHealthBarsSpawned = true;
        m_MenuState = MenuState.SelectingReserve;
    }


    public void SwapReserveToParty()
    {
        m_PartyManager.ReserveToParty(m_CurrentParty, m_ReservePosition);
    }



}