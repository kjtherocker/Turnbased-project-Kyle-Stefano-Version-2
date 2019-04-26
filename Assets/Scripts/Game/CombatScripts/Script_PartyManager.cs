 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PartyManager : MonoBehaviour
{
    public List<Script_Creatures> m_CurrentParty;
    public List<Script_Creatures> m_ReservePartymembers;


    // Use this for initialization
    void Start()
    {
        m_CurrentParty.Add(gameObject.AddComponent<Script_MainCharacter>());
        m_CurrentParty.Add(gameObject.AddComponent<Script_MainCharacter>());
        m_CurrentParty.Add(gameObject.AddComponent<Script_MainCharacter>());
        m_CurrentParty.Add(gameObject.AddComponent<Script_MainCharacter>());
    }

    // Update is called once per frame
    void Update()
    {
      

    }

    public void CombatEnd()
    {
        for (int i = 0; i < m_CurrentParty.Count; i++)
        {
            m_CurrentParty[i].SetHealth(m_CurrentParty[i].MaxHealth);
            m_CurrentParty[i].SetMana(100);
        }
    }

    public void ReserveToParty(int CurrentPartyPosition, int CurrentReservePosition)
    {
        Script_Creatures TransferBuffer = m_CurrentParty[CurrentPartyPosition];
        m_CurrentParty[CurrentPartyPosition] = m_ReservePartymembers[CurrentReservePosition];
        m_ReservePartymembers[CurrentReservePosition] = TransferBuffer;

    }
}
