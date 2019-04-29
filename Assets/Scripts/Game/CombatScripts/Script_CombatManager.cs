using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;


public class Script_CombatManager : MonoBehaviour
{

    public Script_PartyManager PartyManager;
    public Script_EncounterManager EncounterManager;
    public Script_GameManager GameManager;

    public Script_Grid m_Grid;

    public GameObject Canvas_CommandBoard;
    public GameObject Canvas_SkillMenu;
    public GameObject Canvas_TurnMenu;
    public Script_EndOfCombatMenu Canvas_CombatEndMenu;
    public GameObject Image_Notification;


    //For the skills

    public Script_Creatures CurrentTurnHolder;

    public Text Text_Notification;
    public Text Text_SkillDescription;

    public int AmountofTurns;
    public int EnemySelection;
    int m_EnemyChosen;

    int CurrentTurnHolderNumber;
    int CurrentTurnHolderSkills;

    // True is BloodArt false is Normal SKills
    bool WhatTypeOfSkillsUsed;
    bool HasStatusAppeared;
    bool WhichSidesTurnIsIt;
    bool Attackisfinished;
    bool CombatHasStarted;
    bool EnemyIsChosen;
    bool m_CurrentTurnHolderbuttonsHaveSpawned;
    bool m_AttackButton;

    bool m_IsDomainEnroaching;

    public Script_CombatCameraController m_BattleCamera;
    public Script_GrassController m_GrassController;
    public Script_ButtonSkillWrapper m_ButtonReference;

    public Script_ButtonEnemyWrapper m_ButtonEnemyReference;
    public Script_TurnIndicatorWrapper m_ImageReference;


    public List<Text> CurentTurnHolderSkillText;
    public List<Button> m_BasicMenuButtons;
    public List<Script_ButtonSkillWrapper> m_CurrentSkillMenuButtonsMenu;
    public List<Script_ButtonEnemyWrapper> m_CurrentEnemyMenuButtons;

    public List<Script_TurnIndicatorWrapper> m_TurnIdenticator;

    public List<Script_Creatures> DeadAllys;
    public List<Script_Creatures> TurnOrderAlly;
    public List<Script_Creatures> TurnOrderEnemy;
    public List<Script_Creatures> CurrentTurnOrderSide;


    public enum BattleStates
    {
        NoTurn,
        Spawn,
        EnemyTurn,
        EnemyAttacking,
        EnemyDomain,
        AllyTurn,
        AllySkillSelecting,
        AllyAttack,
        EndOfTurnSet,
        EndOfCombat


    }

    public BattleStates m_BattleStates;

    void Start()
    {
        
        EnemyIsChosen = false;

        GameManager = Script_GameManager.Instance;
        EncounterManager = Script_GameManager.Instance.EncounterManager;
        PartyManager = Script_GameManager.Instance.PartyManager;

       // m_Grid = Script_GameManager.Instance.m_Grid;
        Image_Notification.SetActive(false);
        //Canvas_CommandBoard.SetActive(false);
        Canvas_SkillMenu.SetActive(false);
        Canvas_TurnMenu.SetActive(false);


    }

    public void CombatStart()
    {
        if (CombatHasStarted == false)
        {

            m_CurrentTurnHolderbuttonsHaveSpawned = false;
            //Setting up the players
            m_Grid.StartGridCreation();

            AddCreatureToCombat(PartyManager.m_CurrentParty[0], new Vector2Int(2, 5), TurnOrderAlly);
           

            //Setting up the Enemy

            AddCreatureToCombat(EncounterManager.EnemySlot1, new Vector2Int(6, 5), TurnOrderEnemy);
            AddCreatureToCombat(EncounterManager.EnemySlot2, new Vector2Int(6, 9), TurnOrderEnemy);



            EnemyIsChosen = false;
            CombatHasStarted = true;
            HasStatusAppeared = false;
            Canvas_TurnMenu.SetActive(true);
            AmountofTurns = TurnOrderAlly.Count;
            for (int i = 0; i < AmountofTurns; i++)
            {
                m_TurnIdenticator.Add(Instantiate<Script_TurnIndicatorWrapper>(m_ImageReference));
                m_TurnIdenticator[i].gameObject.transform.localPosition = new Vector3(350 - i * 25, 170, 0);
                m_TurnIdenticator[i].gameObject.transform.SetParent(Canvas_TurnMenu.transform, false);
            }
            
            m_BattleStates = BattleStates.AllyTurn;

            CurrentTurnOrderSide = TurnOrderAlly;
            //Canvas_CommandBoard.SetActive(false);
            Canvas_CombatEndMenu.Reset();
            WhichSidesTurnIsIt = false;
            CurrentTurnHolderNumber = 0;

        }

    }

    public void AddCreatureToCombat(Script_Creatures aCreature, Vector2Int aPosition, List<Script_Creatures> aList)
    {
        aList.Add(aCreature);

        aList[aList.Count - 1].ModelInGame = Instantiate<GameObject>(aList[aList.Count - 1].Model);
        aList[aList.Count - 1].SetSpawnPosition(m_Grid.m_GridPathArray[aPosition.x, aPosition.y].gameObject.transform.position);
        aList[aList.Count - 1].ModelInGame.transform.position = m_Grid.m_GridPathArray[aPosition.x, aPosition.y].gameObject.transform.position;
        aList[aList.Count - 1].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, -90, 0.0f);
        aList[aList.Count - 1].m_CreatureAi = aList[0].ModelInGame.GetComponent<Script_AiController>();
        aList[aList.Count - 1].m_CreatureAi.m_Position =
            m_Grid.m_GridPathArray[aPosition.x, aPosition.y].m_PositionInGrid;

        aList[aList.Count - 1].m_CreatureAi.m_Grid = m_Grid;

        aList[aList.Count - 1].m_CreatureAi.m_Creature = aList[aList.Count - 1];

        m_Grid.m_GridPathArray[aPosition.x, aPosition.y].GetComponent<Script_CombatNode>().m_CreatureOnGridPoint = aList[aList.Count - 1];


        aList[aList.Count - 1].ModelInGame.transform.localScale = new Vector3(0.02448244f, 0.02448244f, 0.02448244f);

    }




    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            CombatStart();
        }


        switch (m_BattleStates)
        {
            case BattleStates.Spawn:
                
                    m_BattleStates = BattleStates.AllyTurn;


                
                break;

            case BattleStates.AllyTurn:
                CurrentTurnHolder = CurrentTurnOrderSide[CurrentTurnHolderNumber];
                Attackisfinished = false;

                    //Canvas_CommandBoard.SetActive(true);
                   // m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.Default);
                

                break;

            case BattleStates.AllySkillSelecting:
                if (m_AttackButton == false)
                {
                    PlayerSelecting();
                }
                else
                {

                }
                break;

            case BattleStates.AllyAttack:
                if (m_AttackButton == false)
                {
                    if (WhatTypeOfSkillsUsed == false)
                    {
                        PlayerTurnSkill();
                    }
                    else if (WhatTypeOfSkillsUsed == true)
                    {
                        PlayerTurnBloodArt();
                    }
                }
                else
                {
                    SinglePersonAttack();
                }
           
                break;

            case BattleStates.EnemyAttacking:
                if (m_BattleCamera.m_cameraState == Script_CombatCameraController.CameraState.Nothing)
                {

                   // CombatTurnEndCombatManagerEnemy();

                }
                break;

            case BattleStates.EnemyDomain:
                if (m_IsDomainEnroaching == true)
                {
                    m_GrassController.IsEncoraching();
                   

                    //m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.EnemyAttacking);
                    ParticleSystem m_Skillparticleeffect = Instantiate<ParticleSystem>(CurrentTurnHolder.m_Domain.GetSkillParticleEffect());
                    m_Skillparticleeffect.transform.localPosition = CurrentTurnHolder.ModelInGame.transform.position;
                    m_IsDomainEnroaching = false;
                }
                if (m_BattleCamera.m_cameraState == Script_CombatCameraController.CameraState.Nothing)
                {
                    m_BattleStates = BattleStates.EndOfTurnSet;
                    m_TurnIdenticator[0].DestroyThisObject();
                    m_TurnIdenticator.RemoveAt(0);
                }
                break;

            case BattleStates.EnemyTurn:
                CurrentTurnHolder = CurrentTurnOrderSide[CurrentTurnHolderNumber];
                EnemyTurn();
                Attackisfinished = false;

                break;

            

            case BattleStates.EndOfCombat:

                Canvas_CombatEndMenu.gameObject.SetActive(true);
                Canvas_CombatEndMenu.TurnScoreOn();
                Canvas_CombatEndMenu.GetScore();
                if (Input.anyKey)
                {
                    //CombatEnd();
                }
                break;

        }

    
        if (CombatHasStarted == true)
        {
            if (CurrentTurnHolder != null)
            {
                if (CurrentTurnHolder.GetCharactertype() == Script_Creatures.Charactertype.Ally)
                {

                    

                }
            }
        }

        for (int i = 0; i < TurnOrderEnemy.Count; i++)
        {

            TurnOrderEnemy[i].SetObjectToRotateAround(CurrentTurnHolder);



        }

       
        //Ending the Current encounter
        if (CombatHasStarted == true)
        {
            if (TurnOrderEnemy.Count == 0)
            {
                if (m_BattleCamera.m_cameraState == Script_CombatCameraController.CameraState.Nothing)
                {
                    m_BattleStates = BattleStates.EndOfCombat;
                }
            }
        }

        RemoveDeadFromList();

        if (AmountofTurns == 0)
        {
            if (CombatHasStarted == true)
            {
                AmountofTurns = 0;

                if (Attackisfinished == true)
                {
                    if (m_BattleStates != BattleStates.EnemyDomain)
                    {
                        for (int i = 0; i < CurrentTurnOrderSide.Count; i++)
                        {
                            if (CurrentTurnOrderSide[i].m_DomainStages == Script_Creatures.DomainStages.Encroaching)
                            {

                                
                                m_BattleStates = BattleStates.EnemyDomain;
                                m_IsDomainEnroaching = true;
                                Attackisfinished = false;
                            }
                            else
                            {
                               
                                m_BattleStates = BattleStates.EndOfTurnSet;
                            }
                        }
                    }
                }
                if (m_BattleStates == BattleStates.EndOfTurnSet)
                {
                    WhichSidesTurnIsIt = !WhichSidesTurnIsIt;

                    if (WhichSidesTurnIsIt == false)
                    {

                        //Canvas_CommandBoard.SetActive(true);

                        m_BattleStates = BattleStates.AllyTurn;
                        CurrentTurnOrderSide = TurnOrderAlly;
                    }

                    if (WhichSidesTurnIsIt == true)
                    {
                        //Canvas_CommandBoard.SetActive(false);
                        m_BattleStates = BattleStates.EnemyTurn;
                        CurrentTurnOrderSide = TurnOrderEnemy;
                    }


                    CurrentTurnHolderNumber = 0;
                    RemoveDeadFromList();

                    for (int i = 0; i < CurrentTurnOrderSide.Count; i++)
                    {
                        AmountofTurns += CurrentTurnOrderSide[i].AmountOfTurns;
                    }

                    if (m_TurnIdenticator.Count == 0)
                    {

                        for (int i = 0; i < AmountofTurns; i++)
                        {
                            

                            m_TurnIdenticator.Add(Instantiate<Script_TurnIndicatorWrapper>(m_ImageReference));
                            m_TurnIdenticator[i].gameObject.transform.localPosition = new Vector3(350 - i * 25, 170, 0);
                            m_TurnIdenticator[i].gameObject.transform.SetParent(Canvas_TurnMenu.transform, false);

                            if (CurrentTurnOrderSide[0].GetCharactertype() == Script_Creatures.Charactertype.Ally)
                            {
                                m_TurnIdenticator[i].SetGroup(true);
                            }
                            if (CurrentTurnOrderSide[0].GetCharactertype() == Script_Creatures.Charactertype.Enemy)
                            {
                                m_TurnIdenticator[i].SetGroup(false);
                            }
                        }
                        for (int i = 0; i < AmountofTurns; i++)
                        {
                            if (CurrentTurnOrderSide[i].m_DomainStages == Script_Creatures.DomainStages.Encroaching)
                            {
                                m_TurnIdenticator.Add(Instantiate<Script_TurnIndicatorWrapper>(m_ImageReference));
                                m_TurnIdenticator[AmountofTurns].SetGroup(false);
                                m_TurnIdenticator[AmountofTurns].gameObject.transform.localPosition = new Vector3(350 - AmountofTurns * 25, 170, 0);
                                m_TurnIdenticator[AmountofTurns].gameObject.transform.SetParent(Canvas_TurnMenu.transform, false);
                                
                            }
                        }
                    }
         
                }
            }
        }
        if (CurrentTurnOrderSide.Count == CurrentTurnHolderNumber)
        {
            CurrentTurnHolderNumber = 0;
        }

        for (int i = 0; i < TurnOrderEnemy.Count; i++)
        {
            TurnOrderEnemy[i].Update();
        }

        SettingSkillText();

    }

    public void SetEnemyChosen(int A_newEnemyIsChosen)
    {
        m_EnemyChosen = A_newEnemyIsChosen;
    }
    public void SetBattleStateToSelect()
    {
        m_BattleStates = BattleStates.AllySkillSelecting;
    }

    public void SetBattleStateToAllyAttack()
    {
        m_BattleStates = BattleStates.AllyAttack;
    }

    void SettingSkillText()
    {
        if (CurrentTurnHolder != null)
        {
            if (m_BattleStates == BattleStates.AllyTurn || m_BattleStates == BattleStates.AllySkillSelecting)
            {
                if (Input.GetKeyDown("escape"))
                {
                    if (m_CurrentSkillMenuButtonsMenu.Count != 0)
                    {
                        for (int i = m_CurrentSkillMenuButtonsMenu.Count; i > 0; i--)
                        {
                            Destroy(m_CurrentSkillMenuButtonsMenu[0]);
                            m_CurrentSkillMenuButtonsMenu[0].ToDestroy();
                            m_CurrentSkillMenuButtonsMenu.RemoveAt(0);
                        }
                    }
                    if (m_CurrentEnemyMenuButtons.Count != 0)
                    {
                        for (int i = m_CurrentEnemyMenuButtons.Count; i > 0; i--)
                        {
                            Destroy(m_CurrentEnemyMenuButtons[0]);
                            m_CurrentEnemyMenuButtons[0].ToDestroy();
                            m_CurrentEnemyMenuButtons.RemoveAt(0);
                        }
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        m_BasicMenuButtons[i].interactable = true;
                    }
                    m_CurrentTurnHolderbuttonsHaveSpawned = false;
                    HasStatusAppeared = false;
                    m_AttackButton = false;
                    m_BattleStates = BattleStates.AllyTurn;
                }


            }
        }
    }
    void RemoveDeadFromList()
    {
        if (TurnOrderAlly != null)
        {
            for (int i = 0; i < TurnOrderAlly.Count; i++)
            {
                if (TurnOrderAlly[i].CurrentHealth <= 0)
                {
                    Canvas_CombatEndMenu.AddToDeaths(1);
                    DeadAllys.Add(TurnOrderAlly[i]);
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
                    TurnOrderEnemy[i].Death();
                    TurnOrderEnemy.RemoveAt(i);
                }

            }
        }

        if (AmountofTurns <= 0)
        {
            AmountofTurns = 0;
        }
    }

    public void EnemyTurn()
    {
        RemoveDeadFromList();
        int EnemySkillChosen = CurrentTurnHolder.EnemyAi();

        if (CurrentTurnHolder.m_creaturesAilment != Script_Creatures.CreaturesAilment.Sleep)
        {
            if (Attackisfinished == false)
            {
                Image_Notification.SetActive(true);
                Text_Notification.text = CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillName();

                if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillType() == Script_Skills.SkillType.Extra)
                {
                    
                }

                if (Input.GetKeyDown(KeyCode.F1))
                {
                    Application.Quit();
                }
                if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillType() == Script_Skills.SkillType.Buff)
                {
                    //Checking the range the skills has single target or fulltarget
                    if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                    {

                       

                    }
                }
                if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillType() == Script_Skills.SkillType.Attack)
                {
                    if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                    {

                       



                    }
                }

                if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillType() == Script_Skills.SkillType.Attack)
                {
                    if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillRange() == Script_Skills.SkillRange.SingleTarget)
                    {

                       
                    }
                }
                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Heal)
                {
                    //Checking the range the skills has single target or fulltarget
                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                    {
                   
                    }
                }
            }
        }
        else
        {
            AmountofTurns--;

            CurrentTurnHolder.EndTurn();

            if (AmountofTurns != 0)
            {
                CurrentTurnHolderNumber++;

            }

        }
        
    }


    public void PlayerTurnSkill()
    {
        RemoveDeadFromList();
        if (CurrentTurnHolder.m_creaturesAilment != Script_Creatures.CreaturesAilment.Sleep)
        {
            //Extra
            if (Attackisfinished == false)
            {
                //Canvas_CommandBoard.gameObject.SetActive(false);

                //Attack single target

                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Attack)
                    {
                        //Checking the range the skills has single target or fulltarget
                        if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.SingleTarget)
                        {
                         
                        }
                    }


                    //Full Target Attack


                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Attack)
                    {
                        //Checking the range the skills has single target or fulltarget
                        if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                        {

                        }
                    }

                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Buff)
                    {
                          //Checking the range the skills has single target or fulltarget
                          if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                          { 

                          }
                    }

                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Debuff)
                {
                    //Checking the range the skills has single target or fulltarget
                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                    {
                      

                    }
                }

                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Aliment)
                    {
                        //Checking the range the skills has single target or fulltarget
                        if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                        {
                        

                        }
                    }

                    if (CurrentTurnHolder.CurrentMana > CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse())
                    {

                        if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Heal)
                        {
                        //Checking the range the skills has single target or fulltarget
                        if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                        {
                        }
                        }



                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Resurrect)
                    {
                        //Checking the range the skills has single target or fulltarget
                        if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.SingleTarget)
                        {

                            
                        }
                    }
                }
            }
        }
        else
        {
            
        }

    }

    public void PlayerTurnBloodArt()
    {
        if (Attackisfinished == false)
        {
            if (CurrentTurnHolder.m_BloodArts[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Blood)
            {
                //Checking the range the skills has single target or fulltarget
                if (CurrentTurnHolder.m_BloodArts[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.SelfTargeted)
                {
                    CurrentTurnHolder.IncrementMana(CurrentTurnHolder.MaxMana / CurrentTurnHolder.m_BloodArts[CurrentTurnHolderSkills].GetCostToUse());
                    StartCoroutine(CurrentTurnHolder.DecrementHealth(CurrentTurnHolder.MaxHealth / 4, CurrentTurnHolder.m_BloodArts[CurrentTurnHolderSkills].GetElementalType(), 0.01f, 0.2f, 0.5f));
                    Attackisfinished = true;
                    
                }
            }
        }
    }

    public void PlayerSelecting()
    {

        
        if (HasStatusAppeared != true)
        {

            HasStatusAppeared = true;
            if (WhatTypeOfSkillsUsed == true)
            {
                

                if (CurrentTurnHolder.m_BloodArts[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Blood)
                {
                    //Checking the range the skills has single target or fulltarget
                    if (CurrentTurnHolder.m_BloodArts[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.SelfTargeted)
                    {

                        m_CurrentEnemyMenuButtons.Add(Instantiate<Script_ButtonEnemyWrapper>(m_ButtonEnemyReference, gameObject.transform));
                        m_CurrentEnemyMenuButtons[0].gameObject.transform.localPosition = new Vector3(300, -60 + 0 * 65, 0);
                        m_CurrentEnemyMenuButtons[0].SetupButton(CurrentTurnHolder, 0, this);
                        m_CurrentEnemyMenuButtons[0].gameObject.transform.SetParent(Canvas_CommandBoard.transform, false);
                    }

                }
            }
            else if (WhatTypeOfSkillsUsed == false)
            {
                //Attack single target

                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Attack)
                {
                    //Checking the range the skills has single target or fulltarget
                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.SingleTarget)
                    {
                        for (int i = 0; i < TurnOrderEnemy.Count; i++)
                        {
                            m_CurrentEnemyMenuButtons.Add(Instantiate<Script_ButtonEnemyWrapper>(m_ButtonEnemyReference, gameObject.transform));
                            m_CurrentEnemyMenuButtons[i].gameObject.transform.localPosition = new Vector3(300, -60 + i * 65, 0);
                            m_CurrentEnemyMenuButtons[i].SetupButton(TurnOrderEnemy[i], i, this);
                            m_CurrentEnemyMenuButtons[i].gameObject.transform.SetParent(Canvas_CommandBoard.transform, false);

                        }

                    }
                }


                //Full Target Attack


                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Attack)
                {
                    //Checking the range the skills has single target or fulltarget
                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                    {
                        for (int i = 0; i < TurnOrderEnemy.Count; i++)
                        {
                            m_CurrentEnemyMenuButtons.Add(Instantiate<Script_ButtonEnemyWrapper>(m_ButtonEnemyReference, gameObject.transform));
                            m_CurrentEnemyMenuButtons[i].gameObject.transform.localPosition = new Vector3(300, -60 + i * 65, 0);
                            m_CurrentEnemyMenuButtons[i].SetupButton(TurnOrderEnemy[i], i, this);
                            m_CurrentEnemyMenuButtons[i].gameObject.transform.SetParent(Canvas_CommandBoard.transform, false);

                        }
                        

                    }
                }

                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Buff)
                {
                    //Checking the range the skills has single target or fulltarget
                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                    {
                        for (int i = 0; i < TurnOrderAlly.Count; i++)
                        {
                            m_CurrentEnemyMenuButtons.Add(Instantiate<Script_ButtonEnemyWrapper>(m_ButtonEnemyReference, gameObject.transform));
                            m_CurrentEnemyMenuButtons[i].gameObject.transform.localPosition = new Vector3(300, -60 + i * 65, 0);
                            m_CurrentEnemyMenuButtons[i].SetupButton(TurnOrderAlly[i], i, this);
                            m_CurrentEnemyMenuButtons[i].gameObject.transform.SetParent(Canvas_CommandBoard.transform, false);

                        }
                    }
                }

                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Debuff)
                {
                    //Checking the range the skills has single target or fulltarget
                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                    {
                        for (int i = 0; i < TurnOrderEnemy.Count; i++)
                        {
                            m_CurrentEnemyMenuButtons.Add(Instantiate<Script_ButtonEnemyWrapper>(m_ButtonEnemyReference, gameObject.transform));
                            m_CurrentEnemyMenuButtons[i].gameObject.transform.localPosition = new Vector3(300, -60 + i * 65, 0);
                            m_CurrentEnemyMenuButtons[i].SetupButton(TurnOrderEnemy[i], i, this);
                            m_CurrentEnemyMenuButtons[i].gameObject.transform.SetParent(Canvas_CommandBoard.transform, false);

                        }
                    }
                }

                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Aliment)
                {
                    //Checking the range the skills has single target or fulltarget
                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                    {


                    }
                }

                if (CurrentTurnHolder.CurrentMana > CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse())
                {

                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Heal)
                    {
                        //Checking the range the skills has single target or fulltarget
                        if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                        {
                            for (int i = 0; i < TurnOrderAlly.Count; i++)
                            {
                                m_CurrentEnemyMenuButtons.Add(Instantiate<Script_ButtonEnemyWrapper>(m_ButtonEnemyReference, gameObject.transform));
                                m_CurrentEnemyMenuButtons[i].gameObject.transform.localPosition = new Vector3(300, -60 + i * 65, 0);
                                m_CurrentEnemyMenuButtons[i].SetupButton(TurnOrderAlly[i], i, this);
                                m_CurrentEnemyMenuButtons[i].gameObject.transform.SetParent(Canvas_CommandBoard.transform, false);

                            }
                        }
                    }
                }


                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Resurrect)
                {
                    //Checking the range the skills has single target or fulltarget
                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.SingleTarget)
                    {
                        for (int i = 0; i < DeadAllys.Count; i++)
                        {
                            m_CurrentEnemyMenuButtons.Add(Instantiate<Script_ButtonEnemyWrapper>(m_ButtonEnemyReference, gameObject.transform));
                            m_CurrentEnemyMenuButtons[i].gameObject.transform.localPosition = new Vector3(300, -60 + i * 65, 0);
                            m_CurrentEnemyMenuButtons[i].SetupButton(DeadAllys[i], i, this);
                            m_CurrentEnemyMenuButtons[i].gameObject.transform.SetParent(Canvas_CommandBoard.transform, false);

                        }

                    }

                }
                for (int i = 0; i < CurrentTurnHolder.m_Skills.Count; i++)
                {
                    m_CurrentSkillMenuButtonsMenu[i].SetAsNotInteractable();
                }



            }
        }
    }

    public void CurrentTurnHolderSkill1()
    {
        for (int i = 0; i < TurnOrderEnemy.Count; i++)
        {
            m_CurrentEnemyMenuButtons.Add(Instantiate<Script_ButtonEnemyWrapper>(m_ButtonEnemyReference, gameObject.transform));
            m_CurrentEnemyMenuButtons[i].gameObject.transform.localPosition = new Vector3(300, -60 + i * 65, 0);
            m_CurrentEnemyMenuButtons[i].SetupButton(TurnOrderEnemy[i], i, this);
            m_CurrentEnemyMenuButtons[i].gameObject.transform.SetParent(Canvas_CommandBoard.transform, false);

        }
        for (int i = 0; i < 4; i++)
        {
            m_BasicMenuButtons[i].interactable = false;
        }
        m_AttackButton = true;
        m_BattleStates = BattleStates.AllySkillSelecting;

    }
    public void CurrentTurnHolderSkill2()
    {

        if (m_CurrentTurnHolderbuttonsHaveSpawned == false)
        {
            WhatTypeOfSkillsUsed = false;
            for (int i = 0; i < CurrentTurnHolder.m_Skills.Count; i++)
            {
                m_CurrentSkillMenuButtonsMenu.Add(Instantiate<Script_ButtonSkillWrapper>(m_ButtonReference, gameObject.transform));
                m_CurrentSkillMenuButtonsMenu[i].gameObject.transform.position = new Vector3(-1540, 115 + i * 31, 0);
                if (CurrentTurnHolder.m_Skills[i].GetSkillType() == Script_Skills.SkillType.Resurrect)
                {
                    m_CurrentSkillMenuButtonsMenu[i].SetupButton(CurrentTurnHolder, CurrentTurnHolder.m_Skills[i], i, this, DeadAllys);
                }
                else 
                {
                    m_CurrentSkillMenuButtonsMenu[i].SetupButton(CurrentTurnHolder, CurrentTurnHolder.m_Skills[i], i, this, TurnOrderEnemy);
                }
                

                m_CurrentSkillMenuButtonsMenu[i].gameObject.transform.SetParent(Canvas_CommandBoard.transform, false);
            }
            
            for (int i = 0; i < 4; i++)
            {

                m_BasicMenuButtons[i].interactable = false;
            }
            m_CurrentTurnHolderbuttonsHaveSpawned = true;
        }
    }
    public void CurrentTurnHolderSkill3()
    {
        if (m_CurrentTurnHolderbuttonsHaveSpawned == false)
        {
            WhatTypeOfSkillsUsed = true;
            for (int i = 0; i < CurrentTurnHolder.m_BloodArts.Count; i++)
            {
                m_CurrentSkillMenuButtonsMenu.Add(Instantiate<Script_ButtonSkillWrapper>(m_ButtonReference, gameObject.transform));
                m_CurrentSkillMenuButtonsMenu[i].gameObject.transform.position = new Vector3(-1540, 85 + i * 31, 0);
                m_CurrentSkillMenuButtonsMenu[i].SetupButton(CurrentTurnHolder, CurrentTurnHolder.m_BloodArts[i], i, this, TurnOrderAlly);
                m_CurrentSkillMenuButtonsMenu[i].gameObject.transform.SetParent(Canvas_CommandBoard.transform, false);
            }

            for (int i = 0; i < 4; i++)
            {

                m_BasicMenuButtons[i].interactable = false;
            }
            m_CurrentTurnHolderbuttonsHaveSpawned = true;
        }

    }
   

    public void SetTurnHolderSkills( int a_SkillNumber)
    {
        CurrentTurnHolderSkills = a_SkillNumber;
    }

    public void SetDescriptionText(string a_SkillDescription)
    {
        Text_SkillDescription.text = a_SkillDescription;
    }

    public bool SwitchTurnSides()
    {
        WhichSidesTurnIsIt = !WhichSidesTurnIsIt;

        return WhichSidesTurnIsIt;

    }

    void SinglePersonAttack()
    {
        if (Attackisfinished == false)
        {
            int DamageToEnemys = 0;

            //Checking what damageType the skill will use
            if (CurrentTurnHolder.m_Attack.GetDamageType() == Script_Skills.DamageType.Strength)
            {
                DamageToEnemys = CurrentTurnHolder.m_Attack.UseSkill(CurrentTurnHolder.GetAllStrength());
            }
            else if (CurrentTurnHolder.m_Attack.GetDamageType() == Script_Skills.DamageType.Magic)
            {
                DamageToEnemys = CurrentTurnHolder.m_Attack.UseSkill(CurrentTurnHolder.GetAllMagic());
            }

            Canvas_CombatEndMenu.AddToDamageDealt(DamageToEnemys);
            //Setting the Notfication to the skillname
            Image_Notification.SetActive(true);
            Text_Notification.text = CurrentTurnHolder.m_Attack.GetSkillName();
            //Canvas_CommandBoard.gameObject.SetActive(false);
            //  m_BattleStates = BattleStates.AllySelecting;

            CurrentTurnHolder.IncrementMana(5);
            StartCoroutine(TurnOrderEnemy[m_EnemyChosen].DecrementHealth(DamageToEnemys, CurrentTurnHolder.m_Attack.GetElementalType(), 0.01f, 0.2f, 0.5f));


            CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Attack.GetCostToUse());

            Attackisfinished = true;
            
        }
    }


    

}
