using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Script_CombatManager : MonoBehaviour
{

    public Script_PartyManager PartyManager;

    public Script_EncounterManager EncounterManager;

    public Script_GameManager GameManager;



    public GameObject SpawnPosition1;
    public GameObject SpawnPosition2;
    public GameObject SpawnPosition3;
    public GameObject SpawnPosition4;

    public GameObject SpawnEnemyPosition1;
    public GameObject SpawnEnemyPosition2;
    public GameObject SpawnEnemyPosition3;
    public GameObject SpawnEnemyPosition4;

    GameObject PlayerModel1;
    GameObject PlayerModel2;
    GameObject PlayerModel3;
    GameObject PlayerModel4;

    GameObject EnemyModel1;
    GameObject EnemyModel2;
    GameObject EnemyModel3;
    GameObject EnemyModel4;

    Script_Creatures CurrentTurnHolder;


    public List<Script_Creatures> TurnOrderAlly;
    public List<Script_Creatures> TurnOrderEnemy;


    enum BattleStates
    {
        NoTurn,
        EnemyTurn,
        AllyTurn

    }

    BattleStates m_BattleStates;

    void Start()
    {
        CombatStart();


    }

    void CombatStart()
    {
        m_BattleStates = BattleStates.AllyTurn;


        //Setting up the Enemy
        if (EncounterManager.EnemySlot1 != null)
        {
            TurnOrderEnemy.Add(EncounterManager.EnemySlot1);

            EnemyModel1 = TurnOrderEnemy[0].Model;
            Instantiate<GameObject>(EnemyModel1, SpawnEnemyPosition1.transform);
        

        }

        if (EncounterManager.EnemySlot2 != null)
        {
            TurnOrderEnemy.Add(EncounterManager.EnemySlot2);

            EnemyModel2 = TurnOrderEnemy[1].Model;
            Instantiate<GameObject>(EnemyModel2, SpawnEnemyPosition2.transform);


        }

        if (EncounterManager.EnemySlot3 != null)
        {
            TurnOrderEnemy.Add(EncounterManager.EnemySlot3);

            EnemyModel3 = TurnOrderEnemy[2].Model;
            Instantiate<GameObject>(EnemyModel3, SpawnEnemyPosition3.transform);


        }

        if (EncounterManager.EnemySlot4 != null)
        {
            TurnOrderEnemy.Add(EncounterManager.EnemySlot4);

            EnemyModel4 = TurnOrderEnemy[3].Model;
            Instantiate<GameObject>(EnemyModel4, SpawnEnemyPosition4.transform);


        }


        //Setting up the players

        if (PartyManager.PartyMemberSlot1 != null)
        {
            TurnOrderAlly.Add(PartyManager.PartyMemberSlot1);

            PlayerModel1 = PartyManager.PartyMemberSlot1.Model;
            Instantiate<GameObject>(PlayerModel1, SpawnPosition1.transform);
           // PlayerModel1.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);

        }

        if (PartyManager.PartyMemberSlot2 != null)
        {
            TurnOrderAlly.Add(PartyManager.PartyMemberSlot2);

            PlayerModel2 = PartyManager.PartyMemberSlot2.Model;
            Instantiate<GameObject>(PlayerModel2, SpawnPosition2.transform);
            //PlayerModel2.transform.rotation = Quaternion.Euler(0.0f, 260.0f, 0.0f);
        }

        if (PartyManager.PartyMemberSlot3 != null)
        {
            TurnOrderAlly.Add(PartyManager.PartyMemberSlot3);

            PlayerModel3 = PartyManager.PartyMemberSlot3.Model;
            Instantiate<GameObject>(PlayerModel3, SpawnPosition3.transform);
           // PlayerModel3.transform.rotation = Quaternion.Euler(0.0f, 260.0f, 0.0f);

        }

        if (PartyManager.PartyMemberSlot4 != null)
        {
            TurnOrderAlly.Add(PartyManager.PartyMemberSlot4);

            PlayerModel4 = PartyManager.PartyMemberSlot4.Model;
            Instantiate<GameObject>(PlayerModel4, SpawnPosition4.transform);
            //PlayerModel4.transform.rotation = Quaternion.Euler(0.0f, 260.0f, 0.0f);
        }

    }

    void Update()
    {
        if (m_BattleStates == BattleStates.AllyTurn)
        {

            CurrentTurnHolder = TurnOrderAlly[0];
        }


    }

    public void CombatEnd()
    {
        PlayerModel1 = null;
        PlayerModel2 = null;
        PlayerModel3 = null;
        PlayerModel4 = null;
       
        GameManager.SwitchToOverworld();
    }


}
