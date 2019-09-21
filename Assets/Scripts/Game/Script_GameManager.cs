using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Script_GameManager : Singleton<Script_GameManager>
{
    public GameObject Overworld_Objects;
    public GameObject Combat_Objects;
    public Script_PartyManager m_PartyManager;
    public Script_PartyManager PartyManager { get { return m_PartyManager; } }
    public Script_EncounterManager m_EncounterManager;
    public Script_EncounterManager EncounterManager { get { return m_EncounterManager; } }
    public Script_CombatManager m_CombatManager;
    public Script_CombatManager CombatManager { get { return m_CombatManager; } }
    public Script_CombatCameraController m_BattleCamera;
    public Script_CombatCameraController BattleCamera { get { return m_BattleCamera; } }

    public Script_EditorCamera m_EditorCamera;
    public Script_EditorCamera EditorCamera { get { return m_EditorCamera; } }


    public UiManager m_UiManager;
    public UiManager UiManager { get { return m_UiManager; } }

    public Script_InputManager m_InputManager;
    public Script_InputManager InputManager { get { return m_InputManager; } }

    public Script_PropList m_PropList;
    public Script_PropList PropList { get{ return m_PropList; } }

    public Script_Grid m_Grid;
    public Script_Grid Grid { get { return m_Grid; } }

    public Script_SkillList m_SkillList;
    public Script_SkillList SkillList { get { return m_SkillList; } set {  m_SkillList = value; } }

    public Script_NodeFormations m_NodeFormation;
    public Script_NodeFormations NodeFormation { get { return m_NodeFormation; } }



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
        m_PartyManager = GameObject.Find("PartyManager").GetComponent<Script_PartyManager>();
        m_CombatManager = GameObject.Find("CombatManager").GetComponent<Script_CombatManager>();
        m_EncounterManager = GameObject.Find("EncounterManager").GetComponent<Script_EncounterManager>();
        m_NodeFormation = GameObject.Find("NodeFormations").GetComponent<Script_NodeFormations>();
        //m_BattleCamera = GameObject.Find("BattleCamera").GetComponent<Script_CombatCameraController>();
        Physics.autoSimulation = false;

    }
	


    public void SwitchToOverworld()
    {
        m_GameStates = GameStates.Overworld;
        Overworld_Objects.SetActive(true);
        Combat_Objects.SetActive(false);
    }

    public void SwitchToBattle()
    {
        m_GameStates = GameStates.Combat;
        m_CombatManager.CombatStart();
        Overworld_Objects.SetActive(false);
        Combat_Objects.SetActive(true);
    }
}
