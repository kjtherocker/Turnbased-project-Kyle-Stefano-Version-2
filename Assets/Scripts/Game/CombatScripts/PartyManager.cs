 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public List<Creatures> m_CurrentParty;
    public List<Creatures> m_ReservePartymembers;


    // Use this for initialization
    void Start()
    {
        m_CurrentParty.Add(gameObject.AddComponent<Sigma>());
        m_CurrentParty.Add(gameObject.AddComponent<Fide>());
        m_CurrentParty.Add(gameObject.AddComponent<Cavia>());
        m_CurrentParty.Add(gameObject.AddComponent<Vella>());
    }

    // Update is called once per frame
    void Update()
    {
      

    }

    public void CombatEnd()
    {
        for (int i = 0; i < m_CurrentParty.Count; i++)
        {
            m_CurrentParty[i].CurrentHealth = m_CurrentParty[i].MaxHealth;
            m_CurrentParty[i].CurrentMana = m_CurrentParty[i].MaxMana;
        }
    }

    public void ReserveToParty(int CurrentPartyPosition, int CurrentReservePosition)
    {
        Creatures TransferBuffer = m_CurrentParty[CurrentPartyPosition];
        m_CurrentParty[CurrentPartyPosition] = m_ReservePartymembers[CurrentReservePosition];
        m_ReservePartymembers[CurrentReservePosition] = TransferBuffer;

    }
}
