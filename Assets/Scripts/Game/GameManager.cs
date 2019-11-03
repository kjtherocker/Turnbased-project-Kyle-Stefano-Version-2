using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public GameObject Overworld_Objects;
    public GameObject Combat_Objects;
    public PartyManager m_PartyManager;
    public PartyManager PartyManager { get { return m_PartyManager; } }
    public EncounterManager m_EncounterManager;
    public EncounterManager EncounterManager { get { return m_EncounterManager; } }
    public CombatManager m_CombatManager;
    public CombatManager CombatManager { get { return m_CombatManager; } }
    public CombatCameraController m_BattleCamera;
    public CombatCameraController BattleCamera { get { return m_BattleCamera; } }

    public EditorCamera m_EditorCamera;
    public EditorCamera EditorCamera { get { return m_EditorCamera; } }


    public UiManager m_UiManager;
    public UiManager UiManager { get { return m_UiManager; } }

    public InputManager m_InputManager;
    public InputManager InputManager { get { return m_InputManager; } }

    public PropList m_PropList;
    public PropList PropList { get{ return m_PropList; } }

    public Grid m_Grid;
    public Grid Grid { get { return m_Grid; } }

    public SkillList m_SkillList;
    public SkillList SkillList { get { return m_SkillList; } set {  m_SkillList = value; } }

    public NodeFormations m_NodeFormation;
    public NodeFormations NodeFormation { get { return m_NodeFormation; } }



    public enum GameStates
    {
        Overworld,
        Combat

    }

    public GameStates m_GameStates;

    // Use this for initialization


    private void Awake()
    {

     



        m_GameStates = GameStates.Overworld;
        m_PartyManager = GameObject.Find("PartyManager").GetComponent<PartyManager>();
        m_CombatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
        m_EncounterManager = GameObject.Find("EncounterManager").GetComponent<EncounterManager>();
        m_NodeFormation = GameObject.Find("NodeFormations").GetComponent<NodeFormations>();
        //m_BattleCamera = GameObject.Find("BattleCamera").GetComponent<CombatCameraController>();
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
