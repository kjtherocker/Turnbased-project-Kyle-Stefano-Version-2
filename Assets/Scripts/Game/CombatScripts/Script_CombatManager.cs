using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


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

    //For the skills
    public List<Text> CurentTurnHolderSkillText;
    public List<Button> CurentTurnHolderSkillButton;

    public GameObject Canvas_CommandBoard;

    public GameObject Image_Notification;

    public Text Text_Notification;


    public Script_Creatures CurrentTurnHolder;

    public int AmountofTurns;
    public int EnemySelection;

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
        AllySelecting,
      

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

                TurnOrderAlly[0].ModelInGame = Instantiate<GameObject>(TurnOrderAlly[0].Model, SpawnPosition1.transform);

                TurnOrderAlly[0].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, 260.0f, 0.0f);

            }

            if (PartyManager.PartyMemberSlot2 != null)
            {
                TurnOrderAlly.Add(PartyManager.PartyMemberSlot2);

                TurnOrderAlly[1].ModelInGame = Instantiate<GameObject>(TurnOrderAlly[1].Model, SpawnPosition2.transform);

                TurnOrderAlly[1].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, 260.0f, 0.0f);
            }

            if (PartyManager.PartyMemberSlot3 != null)
            {
                TurnOrderAlly.Add(PartyManager.PartyMemberSlot3);

                TurnOrderAlly[2].ModelInGame = Instantiate<GameObject>(TurnOrderAlly[2].Model, SpawnPosition3.transform);

                TurnOrderAlly[2].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, 260.0f, 0.0f);

            }

            if (PartyManager.PartyMemberSlot4 != null)
            {
                TurnOrderAlly.Add(PartyManager.PartyMemberSlot4);

                TurnOrderAlly[3].ModelInGame = Instantiate<GameObject>(TurnOrderAlly[3].Model, SpawnPosition4.transform);

                TurnOrderAlly[3].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, 260.0f, 0.0f);
            }

            //Setting up the Enemy

            

            if (EncounterManager.EnemySlot1 != null)
            {
                TurnOrderEnemy.Add(EncounterManager.EnemySlot1);

                TurnOrderEnemy[0].ModelInGame = Instantiate<GameObject>(TurnOrderEnemy[0].Model, SpawnEnemyPosition1.transform);

                //TurnOrderEnemy[0].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);

                // EnemyModel1.transform.rotation = Quaternion.Euler(0.0f, 130.0f, 0.0f);
            }

            if (EncounterManager.EnemySlot2 != null)
            {
                TurnOrderEnemy.Add(EncounterManager.EnemySlot2);

                TurnOrderEnemy[1].ModelInGame = Instantiate<GameObject>(TurnOrderEnemy[1].Model, SpawnEnemyPosition2.transform);

                TurnOrderEnemy[1].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            }

            if (EncounterManager.EnemySlot3 != null)
            {
                TurnOrderEnemy.Add(EncounterManager.EnemySlot3);

               
                TurnOrderEnemy[2].ModelInGame = Instantiate<GameObject>(TurnOrderEnemy[2].Model, SpawnEnemyPosition3.transform);

                TurnOrderEnemy[2].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);

            }

            if (EncounterManager.EnemySlot4 != null)
            {
                TurnOrderEnemy.Add(EncounterManager.EnemySlot4);

                TurnOrderEnemy[3].ModelInGame = Instantiate<GameObject>(TurnOrderEnemy[3].Model, SpawnEnemyPosition4.transform);

                TurnOrderEnemy[3].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            }



            AmountofTurns = TurnOrderAlly.Count;
            m_BattleStates = BattleStates.AllyTurn;
            Canvas_CommandBoard.SetActive(true);
            CurrentTurnOrderSide = TurnOrderAlly;
            CombatHasStarted = true;

        }
    }




    void Update()
    {


        


        //Ending the Current encounter
        if (CombatHasStarted == true)
        {
            if (TurnOrderEnemy.Count == 0)
            {
                
                CombatEnd();
              
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

        if (GameManager.m_GameStates == Script_GameManager.GameStates.Combat)
        {
            if (CombatHasStarted == false)
            {
                
                CombatStart();
                
            }
        }

        if (AmountofTurns == 0)
        {
            // SwitchTurnSides();
            WhichSidesTurnIsIt = !WhichSidesTurnIsIt;

            if (WhichSidesTurnIsIt == false)
            {
                Canvas_CommandBoard.SetActive(true);
                m_BattleStates = BattleStates.AllyTurn;
                CurrentTurnOrderSide = TurnOrderAlly;
            }

            if (WhichSidesTurnIsIt == true)
            {
                Canvas_CommandBoard.SetActive(false);
                m_BattleStates = BattleStates.EnemyTurn;
                CurrentTurnOrderSide = TurnOrderEnemy;
            }

            

            

            CurrentTurnHolderNumber = 0;
            AmountofTurns = CurrentTurnOrderSide.Count;
        }


        if (m_BattleStates == BattleStates.EnemyTurn)
        {

            CurrentTurnHolder = CurrentTurnOrderSide[CurrentTurnHolderNumber];
            EnemyTurn();

        }



        if (m_BattleStates == BattleStates.AllyTurn)
        {
            
            CurrentTurnHolder = CurrentTurnOrderSide[CurrentTurnHolderNumber];
            Attackisfinished = false;
            //PlayersTurn();
        }

        if (EnemySelection < 0)
        {
            EnemySelection = TurnOrderEnemy.Count - 1;
        }

        if (EnemySelection > TurnOrderEnemy.Count - 1)
        {
            EnemySelection = 0;
        }

        if (TurnOrderEnemy.Count == 0)
        {
            EnemySelection = 0;
        }

       //     if (m_BattleStates == BattleStates.AllySelecting)
      //  {
            if (Input.GetKeyDown("left"))
            {
                EnemySelection--;
            }
            if (Input.GetKeyDown("right"))
            {
                EnemySelection++;
            }

      //  }

            for (int i = 0; i < TurnOrderEnemy.Count; i++)
        {
            TurnOrderEnemy[i].Update();
        }

        if (CurrentTurnHolder.charactertype == Script_Creatures.Charactertype.Ally)
        {
            if (CurrentTurnHolder.m_Skills[0] != null)
            {
                CurentTurnHolderSkillText[0].text = CurrentTurnHolder.m_Skills[0].GetSkillName();
            }
            else
            {
                CurentTurnHolderSkillButton[0].enabled = false;
            }
            if (CurrentTurnHolder.m_Skills[1] != null)
            {
                CurentTurnHolderSkillText[1].text = CurrentTurnHolder.m_Skills[1].GetSkillName();
            }
            else
            {
                CurentTurnHolderSkillButton[1].enabled = false;
            }
            if (CurrentTurnHolder.m_Skills[2] != null)
            {
                CurentTurnHolderSkillText[2].text = CurrentTurnHolder.m_Skills[2].GetSkillName();
            }
            else
            {

                CurentTurnHolderSkillButton[2].enabled = false;
            }
            if (CurrentTurnHolder.m_Skills[3] != null)
            {
                CurentTurnHolderSkillText[3].text = CurrentTurnHolder.m_Skills[3].GetSkillName();
            }
            else
            {
                CurentTurnHolderSkillButton[3].enabled = false;
            }

        }
        CurentTurnHolderSkillButton[0].onClick.AddListener(CurrentTurnHolderSkill1);
       // if(CurentTurnHolderSkillButton[0].OnPointerEnter()  //.AddListener(CurrentTurnHolderSkill1);
        CurentTurnHolderSkillButton[1].onClick.AddListener(CurrentTurnHolderSkill2);
        CurentTurnHolderSkillButton[2].onClick.AddListener(CurrentTurnHolderSkill3);
        CurentTurnHolderSkillButton[3].onClick.AddListener(CurrentTurnHolderSkill4);

    }

    public void EnemyTurn()
    {


        if (CurrentTurnHolder.m_Skills[0].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
        {
            if (Input.GetKeyDown("space"))
            {
                int DamageToAllys = 0;


                //Checking what damageType the skill will use
                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetDamageType() == Script_Skills.DamageType.Strength)
                {
                    DamageToAllys = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].UseSkill(CurrentTurnHolder.GetAllStrength());
                }
                else if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetDamageType() == Script_Skills.DamageType.Magic)
                {
                    DamageToAllys = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].UseSkill(CurrentTurnHolder.GetAllMagic());
                }


                Image_Notification.SetActive(true);

                Text_Notification.text = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();


                for (int i = 0; i < TurnOrderAlly.Count; i++)
                {
                    TurnOrderAlly[i].IncrementMana(5);
                    TurnOrderAlly[i].DecrementHealth(DamageToAllys, CurrentTurnHolder.m_Skills[0].GetElementalType());
                }

                AmountofTurns--;

                CurrentTurnHolder.EndTurn();

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


                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Extra)
                {
                    string SkillName = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();

                    if (SkillName.Equals("Pass Turn"))
                    {
                        AmountofTurns--;
                        Attackisfinished = true;
                        CurrentTurnHolder.EndTurn();
                        if (AmountofTurns != 0)
                        {
                            CurrentTurnHolderNumber++;

                        }
                        
                    }


                }

                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Attack)
                {
                    //Checking the range the skills has single target or fulltarget
                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.SingleTarget)
                    {
                        int DamageToEnemys = 0;

                        //Checking what damageType the skill will use
                        if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetDamageType() == Script_Skills.DamageType.Strength)
                        {
                            DamageToEnemys = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].UseSkill(CurrentTurnHolder.GetAllStrength());
                        }
                        else if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetDamageType() == Script_Skills.DamageType.Magic)
                        {
                            DamageToEnemys = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].UseSkill(CurrentTurnHolder.GetAllMagic());
                        }

                        //Setting the Notfication to the skillname
                        Image_Notification.SetActive(true);
                        Text_Notification.text = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();

                      //  m_BattleStates = BattleStates.AllySelecting;
                        
                            CurrentTurnHolder.IncrementMana(5);
                            TurnOrderEnemy[EnemySelection].DecrementHealth(DamageToEnemys, CurrentTurnHolder.m_Skills[0].GetElementalType());
                        

                        CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());

                        AmountofTurns--;
                        Attackisfinished = true;
                        CurrentTurnHolder.EndTurn();
                        if (AmountofTurns != 0)
                        {
                            CurrentTurnHolderNumber++;

                        }


                    }
                }

                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Attack)
                {
                    //Checking the range the skills has single target or fulltarget
                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                    {
                        int DamageToEnemys = 0;

                        //Checking what damageType the skill will use
                        if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetDamageType() == Script_Skills.DamageType.Strength)
                        {
                            DamageToEnemys = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].UseSkill(CurrentTurnHolder.GetAllStrength());
                        }
                        else if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetDamageType() == Script_Skills.DamageType.Magic)
                        {
                            DamageToEnemys = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].UseSkill(CurrentTurnHolder.GetAllMagic());
                        }

                        //Setting the Notfication to the skillname
                        Image_Notification.SetActive(true);
                        Text_Notification.text = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();

                        for (int i = 0; i < TurnOrderEnemy.Count; i++)
                        {
                            CurrentTurnHolder.IncrementMana(5);
                            TurnOrderEnemy[i].DecrementHealth(DamageToEnemys, CurrentTurnHolder.m_Skills[0].GetElementalType());
                        }

                        CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());

                        AmountofTurns--;
                        Attackisfinished = true;
                        CurrentTurnHolder.EndTurn();
                        if (AmountofTurns != 0)
                        {
                            CurrentTurnHolderNumber++;

                        }


                    }
                }

                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Buff)
                {
                    //Checking the range the skills has single target or fulltarget
                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                    {
                        int BuffToAllys = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].UseSkill(CurrentTurnHolder.GetAllMagic());

                        //Setting the Notfication to the skillname
                        Image_Notification.SetActive(true);
                        Text_Notification.text = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();

                        CurrentTurnHolder.EndTurn();

                        for (int i = 0; i < TurnOrderAlly.Count; i++)
                        {
                            CurrentTurnHolder.IncrementMana(5);
                            TurnOrderAlly[i].AddBuff(BuffToAllys);
                        }

                        CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());

                        AmountofTurns--;
                        Attackisfinished = true;
                        
                        if (AmountofTurns != 0)
                        {
                            CurrentTurnHolderNumber++;

                        }
                    }
                }

                if (CurrentTurnHolder.CurrentMana > CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse())
                {

                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Heal)
                    {
                        //Checking the range the skills has single target or fulltarget
                        if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                        {
                            int DamageToEnemys = 0;

                            //Checking what damageType the skill will use
                            if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetDamageType() == Script_Skills.DamageType.Strength)
                            {
                                DamageToEnemys = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].UseSkill(CurrentTurnHolder.GetAllStrength());
                            }
                            else if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetDamageType() == Script_Skills.DamageType.Magic)
                            {
                                DamageToEnemys = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].UseSkill(CurrentTurnHolder.GetAllMagic());
                            }

                            //Setting the Notfication to the skillname
                            Image_Notification.SetActive(true);
                            Text_Notification.text = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();

                            for (int i = 0; i < TurnOrderAlly.Count; i++)
                            {
                                CurrentTurnHolder.IncrementMana(5);
                                TurnOrderAlly[i].IncrementHealth(DamageToEnemys);
                            }

                            CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());

                            AmountofTurns--;
                            Attackisfinished = true;
                            CurrentTurnHolder.EndTurn();
                            if (AmountofTurns != 0)
                            {
                                CurrentTurnHolderNumber++;

                            }


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
    public void CurrentTurnHolderSkill4()
    {
        CurrentTurnHolderSkills = 3;
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

        for (int i = TurnOrderAlly.Count; i > 0 ; i--)
        {
            Destroy(TurnOrderAlly[0].ModelInGame);
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
