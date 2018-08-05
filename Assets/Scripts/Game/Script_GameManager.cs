using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_GameManager : MonoBehaviour
{
    public GameObject Overworld_Objects;
    public GameObject Combat_Objects;

    public enum GameStates
    {
        Overworld,
        Combat

    }

    public GameStates m_GameStates;

	// Use this for initialization
	void Start ()
    {
        m_GameStates = GameStates.Overworld;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_GameStates == GameStates.Overworld)
        {
            
            Overworld_Objects.SetActive(true);
            Combat_Objects.SetActive(false);
        }
        if (m_GameStates == GameStates.Combat)
        {
            Overworld_Objects.SetActive(false);
            Combat_Objects.SetActive(true);
        }

    }

    public void SwitchToOverworld()
    {
        m_GameStates = GameStates.Overworld;
    }

    public void SwitchToBattle()
    {
        m_GameStates = GameStates.Combat;
    }
}
