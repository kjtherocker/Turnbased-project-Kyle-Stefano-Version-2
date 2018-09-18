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


    public GameObject SpawnPosition1;
    public GameObject SpawnPosition2;
    public GameObject SpawnPosition3;
    public GameObject SpawnPosition4;

    public GameObject SpawnEnemyPosition1;
    public GameObject SpawnEnemyPosition2;
    public GameObject SpawnEnemyPosition3;
    public GameObject SpawnEnemyPosition4;
    public GameObject SpawnBossEnemyPosition;

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


    }

    public void CombatStart()
    {
        if (CombatHasStarted == false)
        {

            m_CurrentTurnHolderbuttonsHaveSpawned = false;
            //Setting up the players

            if (PartyManager.m_CurrentParty[0] != null)
            {
                TurnOrderAlly.Add(PartyManager.m_CurrentParty[0]);

                TurnOrderAlly[0].ModelInGame = Instantiate<GameObject>(TurnOrderAlly[0].Model, SpawnPosition1.transform);
                TurnOrderAlly[0].SetSpawnPosition(SpawnPosition1.transform.position);
                TurnOrderAlly[0].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, -90, 0.0f);

            }

            if (PartyManager.m_CurrentParty[1] != null)
            {
                TurnOrderAlly.Add(PartyManager.m_CurrentParty[1]);

                TurnOrderAlly[1].ModelInGame = Instantiate<GameObject>(TurnOrderAlly[1].Model, SpawnPosition2.transform);
                TurnOrderAlly[1].SetSpawnPosition(SpawnPosition2.transform.position);

                TurnOrderAlly[1].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, -90, 0.0f);
            }

            if (PartyManager.m_CurrentParty[2] != null)
            {
                TurnOrderAlly.Add(PartyManager.m_CurrentParty[2]);

                TurnOrderAlly[2].ModelInGame = Instantiate<GameObject>(TurnOrderAlly[2].Model, SpawnPosition3.transform);
                TurnOrderAlly[2].SetSpawnPosition(SpawnPosition3.transform.position);

                TurnOrderAlly[2].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, -90, 0.0f);

            }

            if (PartyManager.m_CurrentParty[3] != null)
            {
                TurnOrderAlly.Add(PartyManager.m_CurrentParty[3]);

                TurnOrderAlly[3].ModelInGame = Instantiate<GameObject>(TurnOrderAlly[3].Model, SpawnPosition4.transform);
                TurnOrderAlly[3].SetSpawnPosition(SpawnPosition4.transform.position);

                TurnOrderAlly[3].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, -90, 0.0f);
            }

            //Setting up the Enemy



            if (EncounterManager.EnemySlot1 != null)
            {
                TurnOrderEnemy.Add(EncounterManager.EnemySlot1);

                TurnOrderEnemy[0].ModelInGame = Instantiate<GameObject>(TurnOrderEnemy[0].Model, SpawnEnemyPosition1.transform);
                TurnOrderEnemy[0].SetSpawnPosition(Vector3.zero);

            }

            if (EncounterManager.EnemySlot2 != null)
            {
                TurnOrderEnemy.Add(EncounterManager.EnemySlot2);

                TurnOrderEnemy[1].ModelInGame = Instantiate<GameObject>(TurnOrderEnemy[1].Model, SpawnEnemyPosition2.transform);
                TurnOrderEnemy[1].SetSpawnPosition(Vector3.zero);

            }

            if (EncounterManager.EnemySlot3 != null)
            {
                TurnOrderEnemy.Add(EncounterManager.EnemySlot3);


                TurnOrderEnemy[2].ModelInGame = Instantiate<GameObject>(TurnOrderEnemy[2].Model, SpawnEnemyPosition3.transform);
                TurnOrderEnemy[2].SetSpawnPosition(Vector3.zero);


            }

            if (EncounterManager.EnemySlot4 != null)
            {
                TurnOrderEnemy.Add(EncounterManager.EnemySlot4);

                TurnOrderEnemy[3].ModelInGame = Instantiate<GameObject>(TurnOrderEnemy[3].Model, SpawnEnemyPosition4.transform);
                TurnOrderEnemy[3].SetSpawnPosition(Vector3.zero);

            }

            if (EncounterManager.EnemySlotBoss != null)
            {
                TurnOrderEnemy.Add(EncounterManager.EnemySlotBoss);

                TurnOrderEnemy[0].ModelInGame = Instantiate<GameObject>(TurnOrderEnemy[0].Model, SpawnBossEnemyPosition.transform);
                TurnOrderEnemy[0].SetSpawnPosition(Vector3.zero);
                m_GrassController.SetRedEyesReference(TurnOrderEnemy[0]);

            }
            EnemyIsChosen = false;
            CombatHasStarted = true;
            HasStatusAppeared = false;

            AmountofTurns = TurnOrderAlly.Count;
            for (int i = 0; i < AmountofTurns; i++)
            {
                m_TurnIdenticator.Add(Instantiate<Script_TurnIndicatorWrapper>(m_ImageReference));
                m_TurnIdenticator[i].gameObject.transform.localPosition = new Vector3(350 - i * 25, 170, 0);
                m_TurnIdenticator[i].gameObject.transform.SetParent(Canvas_TurnMenu.transform, false);
            }
            m_BattleStates = BattleStates.AllyTurn;
            m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.Spawn);
            CurrentTurnOrderSide = TurnOrderAlly;
            Canvas_CommandBoard.SetActive(false);
            Canvas_CombatEndMenu.Reset();
            WhichSidesTurnIsIt = false;
            CurrentTurnHolderNumber = 0;

        }

    }




    void Update()
    {

        if (m_BattleStates == BattleStates.Spawn)
        {


            if (m_BattleCamera.GetCameraState() != Script_CombatCameraController.CameraState.Spawn)
            {
                m_BattleStates = BattleStates.AllyTurn;


            }
        }

        if (CombatHasStarted == true)
        {
            if (CurrentTurnHolder != null)
            {
                if (CurrentTurnHolder.GetCharactertype() == Script_Creatures.Charactertype.Ally)
                {

                    m_BattleCamera.SetCharacterReference(CurrentTurnHolder);

                }
            }
        }

        for (int i = 0; i < TurnOrderEnemy.Count; i++)
        {

            TurnOrderEnemy[i].SetObjectToRotateAround(CurrentTurnHolder);



        }

        if (m_BattleStates == BattleStates.EndOfCombat)
        {
            Canvas_CombatEndMenu.TurnScoreOn();
            Canvas_CombatEndMenu.GetScore();
            if (Input.anyKey)
            {
                CombatEnd();
            }
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


        if (GameManager.m_GameStates == Script_GameManager.GameStates.Combat)
        {
            if (CombatHasStarted == false)
            {
                CombatStart();
            }
        }

        if (m_BattleStates == BattleStates.EnemyAttacking)
        {
            if (m_BattleCamera.m_cameraState == Script_CombatCameraController.CameraState.Nothing)
            {

                    CombatTurnEndCombatManagerEnemy();

            }
        }

        if (m_BattleStates == BattleStates.EnemyDomain)
        {
            if (m_IsDomainEnroaching == true)
            {
                m_GrassController.IsEncoraching();
                m_BattleCamera.SetCharacterReference(CurrentTurnHolder);

                m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.EnemyAttacking);
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
        }

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
                                m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.Nothing);
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
                                //if (CurrentTurnOrderSide[AmountofTurns].GetCharactertype() == Script_Creatures.Charactertype.Ally)
                                //{
                                //    m_TurnIdenticator[AmountofTurns].SetGroup(true);
                                //}
                                //if (CurrentTurnOrderSide[AmountofTurns].GetCharactertype() == Script_Creatures.Charactertype.Enemy)
                                //{
                                //    m_TurnIdenticator[AmountofTurns].SetGroup(false);
                                //}
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


        if (m_BattleStates == BattleStates.EnemyTurn)
        {

            CurrentTurnHolder = CurrentTurnOrderSide[CurrentTurnHolderNumber];
            EnemyTurn();
            Attackisfinished = false;

        }



        if (m_BattleStates == BattleStates.AllyTurn)
        {

            CurrentTurnHolder = CurrentTurnOrderSide[CurrentTurnHolderNumber];
            Attackisfinished = false;

            if (m_BattleCamera.GetCameraState() != Script_CombatCameraController.CameraState.Spawn)
            {
                Canvas_CommandBoard.SetActive(true);
                m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.Default);
            }

        }

        if (m_BattleStates == BattleStates.AllySkillSelecting)
        {
            if (m_AttackButton == false)
            {
                PlayerSelecting();
            }
            else
            {

            }
        }
        if (m_BattleStates == BattleStates.AllyAttack)
        {
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
            if (m_BattleCamera.GetCameraState() == Script_CombatCameraController.CameraState.Nothing)
            {
                CombatTurnEndCombatManager();
            }


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
                    m_GrassController.IsEncoraching();
                    m_BattleCamera.SetCharacterReference(CurrentTurnHolder);
               
                    ParticleSystem m_Skillparticleeffect = Instantiate<ParticleSystem>(CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillParticleEffect());
                    m_Skillparticleeffect.transform.localPosition = CurrentTurnHolder.ModelInGame.transform.position;
                    m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.EnemyAttacking);
                    m_BattleStates = BattleStates.EnemyAttacking;
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

                        int BuffToAllys = CurrentTurnHolder.m_Skills[EnemySkillChosen].UseSkill(CurrentTurnHolder.GetAllMagic());

                        //Setting the Notfication to the skillname
                        

                        CurrentTurnHolder.EndTurn();

                        for (int i = 0; i < TurnOrderEnemy.Count; i++)
                        {
                            CurrentTurnHolder.IncrementMana(5);
                            StartCoroutine(TurnOrderEnemy[i].AddBuff(BuffToAllys));
                        }

                        CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[EnemySkillChosen].GetCostToUse());


                        m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.EnemyBuff);
                        m_BattleStates = BattleStates.EnemyAttacking;

                    }
                }
                if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillType() == Script_Skills.SkillType.Attack)
                {
                    if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                    {

                        int DamageToAllys = 0;



                        //Checking what damageType the skill will use
                        if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetDamageType() == Script_Skills.DamageType.Strength)
                        {
                            DamageToAllys = CurrentTurnHolder.m_Skills[EnemySkillChosen].UseSkill(CurrentTurnHolder.GetAllStrength());
                        }
                        else if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetDamageType() == Script_Skills.DamageType.Magic)
                        {
                            DamageToAllys = CurrentTurnHolder.m_Skills[EnemySkillChosen].UseSkill(CurrentTurnHolder.GetAllMagic());
                        }


                        // Image_Notification.SetActive(true);

                        Canvas_CombatEndMenu.AddToDamageTaken(DamageToAllys);

                        m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.EnemyAttacking);
                        for (int i = 0; i < TurnOrderAlly.Count; i++)
                        {
                            ParticleSystem m_Skillparticleeffect = Instantiate<ParticleSystem>(CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillParticleEffect());
                            TurnOrderAlly[i].IncrementMana(5);
                            m_Skillparticleeffect.transform.localPosition = TurnOrderAlly[i].ModelInGame.transform.position;

                            StartCoroutine(TurnOrderAlly[i].DecrementHealth(DamageToAllys, CurrentTurnHolder.m_Skills[EnemySkillChosen].GetElementalType(), 0.2f, 2.0f, 1.0f));
                        }


                        m_BattleStates = BattleStates.EnemyAttacking;





                    }
                }

                if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillType() == Script_Skills.SkillType.Attack)
                {
                    if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillRange() == Script_Skills.SkillRange.SingleTarget)
                    {

                        int DamageToAllys = 0;

                        if (EnemyIsChosen == false)
                        {
                            m_EnemyChosen = Random.Range(0, TurnOrderAlly.Count);
                            EnemyIsChosen = true;
                        }
                       
                        //Checking what damageType the skill will use
                        if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetDamageType() == Script_Skills.DamageType.Strength)
                        {
                            DamageToAllys = CurrentTurnHolder.m_Skills[EnemySkillChosen].UseSkill(CurrentTurnHolder.GetAllStrength());
                        }
                        else if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetDamageType() == Script_Skills.DamageType.Magic)
                        {
                            DamageToAllys = CurrentTurnHolder.m_Skills[EnemySkillChosen].UseSkill(CurrentTurnHolder.GetAllMagic());
                        }
                        m_BattleCamera.SetOtherCharacterReference(TurnOrderAlly[m_EnemyChosen]);

                        

                        Text_Notification.text = CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillName();

                        Canvas_CombatEndMenu.AddToDamageTaken(DamageToAllys);
                        m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.EnemyAttacking);
                        TurnOrderAlly[m_EnemyChosen].IncrementMana(5);
                        StartCoroutine(TurnOrderAlly[m_EnemyChosen].DecrementHealth(DamageToAllys, CurrentTurnHolder.m_Skills[EnemySkillChosen].GetElementalType(), 0.01f, 0.1f, 0.2f));

 
                        m_BattleStates = BattleStates.EnemyAttacking;


                    }
                }
                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Heal)
                {
                    //Checking the range the skills has single target or fulltarget
                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
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

                        m_BattleCamera.SetAllCharacterReferences(TurnOrderEnemy);

                        m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.EnemyBuff);

                        DamageToAllys -= 200;
                        //Setting the Notfication to the skillname
                        Image_Notification.SetActive(true);
                        Text_Notification.text = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();

                        for (int i = 0; i < TurnOrderEnemy.Count; i++)
                        {
                            CurrentTurnHolder.IncrementMana(5);
                            StartCoroutine(TurnOrderEnemy[i].IncrementHealth(DamageToAllys));
                        }

                        CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());

                        Attackisfinished = true;
                        m_BattleStates = BattleStates.EnemyAttacking;


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
                Canvas_CommandBoard.gameObject.SetActive(false);

                //Attack single target

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

                            Canvas_CombatEndMenu.AddToDamageDealt(DamageToEnemys);
                            //Setting the Notfication to the skillname
                            Image_Notification.SetActive(true);
                            Text_Notification.text = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();

                            //  m_BattleStates = BattleStates.AllySelecting;

                            CurrentTurnHolder.IncrementMana(5);
                            StartCoroutine(TurnOrderEnemy[m_EnemyChosen].DecrementHealth(DamageToEnemys, CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetElementalType(),0.01f, 0.2f, 0.5f));


                            CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());
                            
                            Attackisfinished = false;
                            m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.Nothing);
                        }
                    }


                    //Full Target Attack


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
                            m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.EnemyAttacking);

                            for (int i = 0; i < TurnOrderEnemy.Count; i++)
                            {
                                ParticleSystem m_Skillparticleeffect = Instantiate<ParticleSystem>(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillParticleEffect());
                                TurnOrderEnemy[i].IncrementMana(5);
                                m_Skillparticleeffect.transform.localPosition = TurnOrderEnemy[i].ModelInGame.transform.position;

                                Canvas_CombatEndMenu.AddToDamageDealt(DamageToEnemys);
                                StartCoroutine(TurnOrderEnemy[i].DecrementHealth(DamageToEnemys, CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetElementalType(),0.001f, 2.0f, 1.0f));
    
                            }
                            Attackisfinished = true;
                            CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());
                            

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
                          
                          
                              for (int i = 0; i < TurnOrderAlly.Count; i++)
                              {
                                  CurrentTurnHolder.IncrementMana(5);
                                  StartCoroutine(TurnOrderAlly[i].AddBuff(BuffToAllys));
                              }
                          
                              m_BattleCamera.SetAllCharacterReferences(TurnOrderAlly);
                              m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.AllyHealing);
                              CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());
                              Attackisfinished = true;
                          
                          }
                    }

                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Debuff)
                {
                    //Checking the range the skills has single target or fulltarget
                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                    {
                        int BuffToAllys = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].UseSkill(CurrentTurnHolder.GetAllMagic());

                        //Setting the Notfication to the skillname
                        Image_Notification.SetActive(true);
                        Text_Notification.text = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();


                        for (int i = 0; i < TurnOrderEnemy.Count; i++)
                        {
                            CurrentTurnHolder.IncrementMana(5);
                            StartCoroutine(TurnOrderEnemy[i].AddDeBuff(BuffToAllys));
                        }

                        m_BattleCamera.SetAllCharacterReferences(TurnOrderAlly);
                        m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.EnemyBuff);
                        CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());
                        Attackisfinished = true;

                    }
                }

                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Aliment)
                    {
                        //Checking the range the skills has single target or fulltarget
                        if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                        {

                            //Setting the Notfication to the skillname
                            Image_Notification.SetActive(true);
                            Text_Notification.text = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();
                            CombatTurnEndCombatManager();
                            for (int i = 0; i < TurnOrderEnemy.Count; i++)
                            {
                                CurrentTurnHolder.IncrementMana(5);
                                TurnOrderEnemy[i].InflictAliment(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetAlimentType());
                            }

                            CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());
                            

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

                                m_BattleCamera.SetAllCharacterReferences(TurnOrderAlly);

                                m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.AllyHealing);

                                Canvas_CombatEndMenu.AddToHealAmount(DamageToEnemys);
                                
                                //Setting the Notfication to the skillname
                                Image_Notification.SetActive(true);
                                Text_Notification.text = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();

                                for (int i = 0; i < TurnOrderAlly.Count; i++)
                                {
                                    CurrentTurnHolder.IncrementMana(5);
                                    StartCoroutine(TurnOrderAlly[i].IncrementHealth(DamageToEnemys));
                                }

                                CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());
                                
                                Attackisfinished = true;



                            }
                        }



                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Resurrect)
                    {
                        //Checking the range the skills has single target or fulltarget
                        if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.SingleTarget)
                        {


                                //Setting the Notfication to the skillname
                                Image_Notification.SetActive(true);
                                Text_Notification.text = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();



                                CurrentTurnHolder.IncrementMana(5);
                                StartCoroutine(DeadAllys[m_EnemyChosen].IncrementHealth(200));
                                DeadAllys[m_EnemyChosen].Resurrection();
                                TurnOrderAlly.Add(DeadAllys[m_EnemyChosen]);
                                DeadAllys.RemoveAt(m_EnemyChosen);


                                CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());
                                
                                Attackisfinished = true;
                                m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.Nothing);



                            
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
                    m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.Nothing);
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
                        m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.AllyAttackSelecting);

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
                for (int i = 0; i < CurrentTurnHolder.m_Skills.Length; i++)
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
            for (int i = 0; i < CurrentTurnHolder.m_Skills.Length; i++)
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
            for (int i = 0; i < CurrentTurnHolder.m_BloodArts.Length; i++)
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
    public void CurrentTurnHolderSkill4()
    {
        CombatTurnEndCombatManager();
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
            Canvas_CommandBoard.gameObject.SetActive(false);
            //  m_BattleStates = BattleStates.AllySelecting;

            CurrentTurnHolder.IncrementMana(5);
            StartCoroutine(TurnOrderEnemy[m_EnemyChosen].DecrementHealth(DamageToEnemys, CurrentTurnHolder.m_Attack.GetElementalType(), 0.01f, 0.2f, 0.5f));


            CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Attack.GetCostToUse());

            Attackisfinished = true;
            m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.EnemyAttacking);
        }
    }

    public void CombatEnd()
    {
        RemoveDeadFromList();
        for (int i = TurnOrderAlly.Count; i > 0; i--)
        {
            Destroy(TurnOrderAlly[0].ModelInGame);
            TurnOrderAlly.RemoveAt(0);
        }
        for (int i = TurnOrderEnemy.Count; i > 0; i--)
        {
            Destroy(TurnOrderEnemy[0].ModelInGame);
            TurnOrderEnemy.RemoveAt(0);
        }

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

        for (int i = m_TurnIdenticator.Count; i > 0 ; i--)
        {
            m_TurnIdenticator[0].DestroyThisObject();
            m_TurnIdenticator.RemoveAt(0);
        }
   
        
        Image_Notification.SetActive(false);
        Canvas_CombatEndMenu.Reset();
        PartyManager.CombatEnd();
        HasStatusAppeared = false;
        CombatHasStarted = false;
        CurrentTurnHolderNumber = 0;
        AmountofTurns = 0;
        EncounterManager.ResetEncounterManager();
        GameManager.SwitchToOverworld();

    }
    public void CombatTurnEndCombatManager()
    {
        Canvas_CommandBoard.SetActive(true);

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
        AmountofTurns--;

        if (m_TurnIdenticator.Count != 0)
        {
            m_TurnIdenticator[m_TurnIdenticator.Count - 1].DestroyThisObject();
            m_TurnIdenticator.RemoveAt(m_TurnIdenticator.Count - 1);
            
        }
        else
        {
            m_TurnIdenticator[0].DestroyThisObject();
            m_TurnIdenticator.RemoveAt(0);
        }
        Image_Notification.SetActive(false);
        HasStatusAppeared = false;
        Attackisfinished = true;
        m_AttackButton = false;
        m_CurrentTurnHolderbuttonsHaveSpawned = false;
        CurrentTurnHolder.EndTurn();
        m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.Default);
        m_BattleStates = BattleStates.AllyTurn;
        RemoveDeadFromList();
        if (AmountofTurns != 0)
        {
                CurrentTurnHolderNumber++;
        }
    }

    public void CombatTurnEndCombatManagerEnemy()
    {
        if (Attackisfinished == false)
        {
            for (int i = 0; i < TurnOrderEnemy.Count; i++)
            {
                TurnOrderEnemy[i].ModelInGame.transform.localPosition = TurnOrderEnemy[i].GetSpawnPosition();

            }
            if (m_TurnIdenticator.Count != 0)
            {
                m_TurnIdenticator[m_TurnIdenticator.Count - 1].DestroyThisObject();
                m_TurnIdenticator.RemoveAt(m_TurnIdenticator.Count - 1);
               
            }
            else
            {
                m_TurnIdenticator[0].DestroyThisObject();
                m_TurnIdenticator.RemoveAt(0);
                
            }
            Image_Notification.SetActive(false);
            AmountofTurns--;
            EnemyIsChosen = false;
            CurrentTurnHolder.EndTurn();
            Attackisfinished = true;
            RemoveDeadFromList();
            if (AmountofTurns != 0)
            {
               
                
                CurrentTurnHolderNumber++;
                
                m_BattleStates = BattleStates.EnemyTurn;
            }
        }
    }

}
