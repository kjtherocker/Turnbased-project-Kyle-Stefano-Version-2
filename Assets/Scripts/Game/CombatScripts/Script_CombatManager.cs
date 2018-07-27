using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



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

    public GameObject PlayerModel1;
    public GameObject PlayerModel2;
    public GameObject PlayerModel3;
    public GameObject PlayerModel4;

    public GameObject EnemyModel1;
    public GameObject EnemyModel2;
    public GameObject EnemyModel3;
    public GameObject EnemyModel4;


    

    public Text CurrentTurnHolderAttackText;
    public Text CurrentTurnHolderSkill1Text;
    public Text CurrentTurnHolderSkill2Text;
    public Text CurrentTurnHolderSkill3Text;

    public Button CurrentTurnHolderAttackButton;
    public Button CurrentTurnHolderSkill1Button;
    public Button CurrentTurnHolderSkill2Button;
    public Button CurrentTurnHolderSkill3Button;

    public GameObject Canvas_CommandBoard;

    public GameObject Image_Notification;

    public Text Text_Notification;


    public Script_Creatures CurrentTurnHolder;

    public int AmountofTurns;
    bool WhichSidesTurnIsIt;

    bool Attackisfinished;

    int CurrentTurnHolderNumber;
    int CurrentTurnHolderSkills;

    bool CombatHasStarted;

    public List<Script_Creatures> TurnOrderAlly;
    public List<Script_Creatures> TurnOrderEnemy;

    public List<Script_Creatures> CurrentTurnOrderSide;


    enum BattleStates
    {
        NoTurn,
        EnemyTurn,
        AllyTurn,
      

    }

    BattleStates m_BattleStates;

    void Start()
    {
  
    }

    public void CombatStart()
    {
        if (CombatHasStarted == false)
        {
      


            m_BattleStates = BattleStates.AllyTurn;
            
            WhichSidesTurnIsIt = false;
            CurrentTurnHolderNumber = 0;

            //Setting up the players

            if (PartyManager.PartyMemberSlot1 != null)
            {
                TurnOrderAlly.Add(PartyManager.PartyMemberSlot1);

                PlayerModel1 = TurnOrderAlly[0].Model;
                Instantiate<GameObject>(PlayerModel1, SpawnPosition1.transform);
                 PlayerModel1.transform.rotation = Quaternion.Euler(0.0f, 260.0f, 0.0f);

            }

            if (PartyManager.PartyMemberSlot2 != null)
            {
                TurnOrderAlly.Add(PartyManager.PartyMemberSlot2);

                PlayerModel2 = TurnOrderAlly[1].Model;
                Instantiate<GameObject>(PlayerModel2, SpawnPosition2.transform);
                PlayerModel2.transform.rotation = Quaternion.Euler(0.0f, 260.0f, 0.0f);
            }

            if (PartyManager.PartyMemberSlot3 != null)
            {
                TurnOrderAlly.Add(PartyManager.PartyMemberSlot3);

                PlayerModel3 = TurnOrderAlly[2].Model;
                Instantiate<GameObject>(PlayerModel3, SpawnPosition3.transform);
                PlayerModel1.transform.rotation = Quaternion.Euler(0.0f, 260.0f, 0.0f);

            }

            if (PartyManager.PartyMemberSlot4 != null)
            {
                TurnOrderAlly.Add(PartyManager.PartyMemberSlot4);

                PlayerModel4 = TurnOrderAlly[3].Model;
                Instantiate<GameObject>(PlayerModel4, SpawnPosition4.transform);
                PlayerModel4.transform.rotation = Quaternion.Euler(0.0f, 260.0f, 0.0f);
            }

            //Setting up the Enemy

            

            if (EncounterManager.EnemySlot1 != null)
            {
                TurnOrderEnemy.Add(EncounterManager.EnemySlot1);

                EnemyModel1 = TurnOrderEnemy[0].Model;
                Instantiate<GameObject>(EnemyModel1, SpawnEnemyPosition1.transform);

               // EnemyModel1.transform.rotation = Quaternion.Euler(0.0f, 130.0f, 0.0f);
            }

            if (EncounterManager.EnemySlot2 != null)
            {
                TurnOrderEnemy.Add(EncounterManager.EnemySlot2);

                EnemyModel2 = TurnOrderEnemy[1].Model;
                Instantiate<GameObject>(EnemyModel2, SpawnEnemyPosition2.transform);

                EnemyModel2.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            }

            if (EncounterManager.EnemySlot3 != null)
            {
                TurnOrderEnemy.Add(EncounterManager.EnemySlot3);

                EnemyModel3 = TurnOrderEnemy[2].Model;
                Instantiate<GameObject>(EnemyModel3, SpawnEnemyPosition3.transform);
                EnemyModel3.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);

            }

            if (EncounterManager.EnemySlot4 != null)
            {
                TurnOrderEnemy.Add(EncounterManager.EnemySlot4);

                EnemyModel4 = TurnOrderEnemy[3].Model;
                Instantiate<GameObject>(EnemyModel4, SpawnEnemyPosition4.transform);

                EnemyModel4.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            }



            AmountofTurns = TurnOrderAlly.Count;
            CombatHasStarted = true;

        }
    }




    void Update()
    {


        if (TurnOrderEnemy != null)
        {
            for (int i = 0; i < TurnOrderEnemy.Count; i++)
            {
                if (TurnOrderEnemy[i].CurrentHealth <= 0)
                {
                    TurnOrderEnemy.RemoveAt(i);
                }

            }
        }

        if (TurnOrderAlly != null)
        {
            for (int i = 0; i < TurnOrderAlly.Count; i++)
            {
                if (TurnOrderAlly[i].CurrentHealth <= 0)
                {
                    TurnOrderAlly.RemoveAt(i);
                }

            }
        }


        //Ending the Current encounter
        if (CombatHasStarted == true)
        {
            if (TurnOrderEnemy.Count == 0)
            {
                
                CombatEnd();
              
            }
        }





        if (GameManager.m_GameStates == Script_GameManager.GameStates.Combat)
        {
            if (CombatHasStarted == false)
            {
                
                CombatStart();
                
            }
        }

        if (AmountofTurns == 0)
        {
            SwitchTurnSides();
            CurrentTurnHolderNumber = 0;
            AmountofTurns = CurrentTurnOrderSide.Count;
        }
        if (WhichSidesTurnIsIt == false)
        {
            Canvas_CommandBoard.SetActive(true);
            m_BattleStates = BattleStates.AllyTurn;
        }

        if (WhichSidesTurnIsIt == true)
        {
            Canvas_CommandBoard.SetActive(false);
            m_BattleStates = BattleStates.EnemyTurn;
        }

        if (m_BattleStates == BattleStates.EnemyTurn)
        {
            CurrentTurnOrderSide = TurnOrderEnemy;
            CurrentTurnHolder = CurrentTurnOrderSide[CurrentTurnHolderNumber];
            EnemyTurn();
            
        }

        if (m_BattleStates == BattleStates.AllyTurn)
        {
           CurrentTurnOrderSide = TurnOrderAlly;
           CurrentTurnHolder = CurrentTurnOrderSide[CurrentTurnHolderNumber];
            Attackisfinished = false;
            //PlayersTurn();
        }

        for (int i = 0; i < TurnOrderEnemy.Count; i++)
        {
            TurnOrderEnemy[i].Update();
        }

        if (CurrentTurnHolder.charactertype == Script_Creatures.Charactertype.Ally)
        {
            if (CurrentTurnHolder.m_Skills[0] != null)
            {
                CurrentTurnHolderSkill1Text.text = CurrentTurnHolder.m_Skills[0].GetSkillName();
            }
            if (CurrentTurnHolder.m_Skills[1] != null)
            {
                CurrentTurnHolderSkill2Text.text = CurrentTurnHolder.m_Skills[1].GetSkillName();
            }
            if (CurrentTurnHolder.m_Skills[2] != null)
            {
                CurrentTurnHolderSkill3Text.text = CurrentTurnHolder.m_Skills[2].GetSkillName();
            }


        }
        CurrentTurnHolderSkill1Button.onClick.AddListener(CurrentTurnHolderSkill1);
        CurrentTurnHolderSkill2Button.onClick.AddListener(CurrentTurnHolderSkill2);
        CurrentTurnHolderSkill3Button.onClick.AddListener(CurrentTurnHolderSkill3);

    }

    public void EnemyTurn()
    {


        if (CurrentTurnHolder.m_Skills[0].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
        {
            if (Input.GetKeyDown("space"))
            {
                int DamageToEnemys = CurrentTurnHolder.m_Skills[0].UseSkill(CurrentTurnHolder.Magic);


                Image_Notification.SetActive(true);

                Text_Notification.text = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();


                for (int i = 0; i < TurnOrderAlly.Count; i++)
                {
                    TurnOrderAlly[i].IncrementMana(5);
                    TurnOrderAlly[i].DecrementHealth(DamageToEnemys);
                }

                AmountofTurns--;

                if (AmountofTurns != 0)
                {
                    CurrentTurnHolderNumber++;

                }
            }
        }

            if (CurrentTurnHolder.m_Skills[0].GetSkillRange() == Script_Skills.SkillRange.SingleTarget)
            {

                if (Input.anyKey)
                {
                    CurrentTurnHolder.m_Skills[0].UseSkill(CurrentTurnHolder.Strength);
                    CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[0].GetCostToUse());
                    AmountofTurns--;
                }

            }
     }


    public void PlayersTurn()
    {
        

        if (Attackisfinished == false)
        {
            if (CurrentTurnHolder.CurrentMana > CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse())
            {

                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                {

                    int DamageToEnemys = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].UseSkill(CurrentTurnHolder.Magic);

                    Image_Notification.SetActive(true);

                    Text_Notification.text = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();


                    for (int i = 0; i < TurnOrderEnemy.Count; i++)
                    {
                        CurrentTurnHolder.IncrementMana(5);
                        TurnOrderEnemy[i].DecrementHealth(DamageToEnemys);
                    }




                    CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());

                    AmountofTurns--;
                    Attackisfinished = true;

                    if (AmountofTurns != 0)
                    {
                        CurrentTurnHolderNumber++;

                    }

                    
                }

                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.SingleTarget)
                {

                    if (Input.anyKey)
                    {
                        CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].UseSkill(CurrentTurnHolder.Strength);
                        CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());
                        AmountofTurns--;
                    }

                }
            }
        }
    }

    public void CurrentTurnHolderSkill1()
    {
        CurrentTurnHolderSkills = 0;
        PlayersTurn();
    }
    public void CurrentTurnHolderSkill2()
    {
        CurrentTurnHolderSkills = 1;
        PlayersTurn();
    }
    public void CurrentTurnHolderSkill3()
    {
        CurrentTurnHolderSkills = 2;
        PlayersTurn();
    }


    public bool SwitchTurnSides()
    {
        WhichSidesTurnIsIt =! WhichSidesTurnIsIt;

        return WhichSidesTurnIsIt;

    }

    public void CombatEnd()
    {
        //EnemyModel1 = null;
        //EnemyModel2 = null;
       // EnemyModel3 = null;
        //EnemyModel4 = null;

        for (int i = TurnOrderAlly.Count; i > 0 ; i++)
        {
            TurnOrderAlly.RemoveAt(0);
        }
        //TurnOrderAlly = null;
        CombatHasStarted = false;
        CurrentTurnHolderNumber = 0;
        AmountofTurns = 0;
        EncounterManager.ResetEncounterManager();
        GameManager.SwitchToOverworld();

    }


}
