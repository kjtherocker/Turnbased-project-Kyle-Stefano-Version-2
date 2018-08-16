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

    

    bool WhichSidesTurnIsIt;
    bool Attackisfinished;
    bool CombatHasStarted;
    bool EnemyIsChosen;
    bool m_CurrentTurnHolderbuttonsHaveSpawned;

    public Script_CombatCameraController m_BattleCamera;

    public Script_ButtonSkillWrapper m_ButtonReference;

    public Script_ButtonEnemyWrapper m_ButtonEnemyReference;

    public List<Text> CurentTurnHolderSkillText;
    public List<Button> m_BasicMenuButtons;
    public List<Script_ButtonSkillWrapper> m_CurrentSkillMenuButtonsMenu;
    public List<Script_ButtonEnemyWrapper> m_CurrentEnemyMenuButtons;
    public List<ParticleSystem> m_Skillparticleeffect;
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
        AllyTurn,
        AllySelecting,
        AllyAttack,


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



            // m_BattleStates = BattleStates.AllyTurn;


            m_CurrentTurnHolderbuttonsHaveSpawned = false;
            //Setting up the players

            if (PartyManager.m_CurrentParty[0] != null)
            {
                TurnOrderAlly.Add(PartyManager.m_CurrentParty[0]);

                TurnOrderAlly[0].ModelInGame = Instantiate<GameObject>(TurnOrderAlly[0].Model, SpawnPosition1.transform);
                TurnOrderAlly[0].SetSpawnPosition(SpawnPosition1.transform.position);
                TurnOrderAlly[0].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, 260.0f, 0.0f);

            }

            if (PartyManager.m_CurrentParty[1] != null)
            {
                TurnOrderAlly.Add(PartyManager.m_CurrentParty[1]);

                TurnOrderAlly[1].ModelInGame = Instantiate<GameObject>(TurnOrderAlly[1].Model, SpawnPosition2.transform);
                TurnOrderAlly[1].SetSpawnPosition(SpawnPosition2.transform.position);

                TurnOrderAlly[1].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, 260.0f, 0.0f);
            }

            if (PartyManager.m_CurrentParty[2] != null)
            {
                TurnOrderAlly.Add(PartyManager.m_CurrentParty[2]);

                TurnOrderAlly[2].ModelInGame = Instantiate<GameObject>(TurnOrderAlly[2].Model, SpawnPosition3.transform);
                TurnOrderAlly[2].SetSpawnPosition(SpawnPosition3.transform.position);

                TurnOrderAlly[2].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, 260.0f, 0.0f);

            }

            if (PartyManager.m_CurrentParty[3] != null)
            {
                TurnOrderAlly.Add(PartyManager.m_CurrentParty[3]);

                TurnOrderAlly[3].ModelInGame = Instantiate<GameObject>(TurnOrderAlly[3].Model, SpawnPosition4.transform);
                TurnOrderAlly[3].SetSpawnPosition(SpawnPosition4.transform.position);

                TurnOrderAlly[3].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, 260.0f, 0.0f);
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

            }
            EnemyIsChosen = false;
            CombatHasStarted = true;

            AmountofTurns = TurnOrderAlly.Count;
            m_BattleStates = BattleStates.AllyTurn;
            m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.Spawn);
            CurrentTurnOrderSide = TurnOrderAlly;
            Canvas_CommandBoard.SetActive(false);

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

        //Ending the Current encounter
        if (CombatHasStarted == true)
        {
            if (TurnOrderEnemy.Count == 0)
            {

                CombatEnd();

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
            if (m_BattleCamera.m_cameraState == Script_CombatCameraController.CameraState.Default)
            {
                if (m_Skillparticleeffect.Count == 0)
                {
                    CombatTurnEndCombatManagerEnemy();
                }
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
            RemoveDeadFromList();

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

            if (m_BattleCamera.GetCameraState() != Script_CombatCameraController.CameraState.Spawn)
            {
                Canvas_CommandBoard.SetActive(true);
                //m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.Default);
            }

        }

        if (m_BattleStates == BattleStates.AllySelecting)
        {
            PlayerSelecting();
        }
        if (m_BattleStates == BattleStates.AllyAttack)
        {
            PlayersTurn();
        }



        if (m_BattleStates == BattleStates.AllySelecting)
        {

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
        m_BattleStates = BattleStates.AllySelecting;
    }

    public void SetBattleStateToAllyAttack()
    {
        m_BattleStates = BattleStates.AllyAttack;
    }

    void SettingSkillText()
    {
        if (CurrentTurnHolder.charactertype == Script_Creatures.Charactertype.Ally)
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
                for (int i = 0; i < 4; i++)
                {
                    m_BasicMenuButtons[i].interactable = true;
                }
            }
            m_CurrentTurnHolderbuttonsHaveSpawned = false;
        }
       // m_BasicMenuButtons[0].onClick.AddListener(SetBattleStateToSelect);
        m_BasicMenuButtons[1].onClick.AddListener(CurrentTurnHolderSkill2);
       // m_BasicMenuButtons[2].onClick.AddListener(SetBattleStateToSelect);
       // m_BasicMenuButtons[3].onClick.AddListener(SetBattleStateToSelect);
    }
    void RemoveDeadFromList()
    {
        if (TurnOrderAlly != null)
        {
            for (int i = 0; i < TurnOrderAlly.Count; i++)
            {
                if (TurnOrderAlly[i].CurrentHealth <= 0)
                {

                    DeadAllys.Add(TurnOrderAlly[i]);
                    TurnOrderAlly.RemoveAt(i);
                }

            }
        }

        if (m_Skillparticleeffect != null)
        {
            if (m_Skillparticleeffect.Count != 0)
            {
                if (m_Skillparticleeffect != null)
                {
                    for (int i = 0; i < m_Skillparticleeffect.Count; i++)
                    {
                        if (m_Skillparticleeffect != null)
                        {
                            if (m_Skillparticleeffect[i].IsAlive() == false)
                            {
                                if (m_Skillparticleeffect != null)
                                {
                                    m_Skillparticleeffect.RemoveAt(i);
                                }
                            }
                        }
                    }
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
           
                if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillType() == Script_Skills.SkillType.Buff)
                {
                    //Checking the range the skills has single target or fulltarget
                    if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillRange() == Script_Skills.SkillRange.FullTarget)
                    {

                        int BuffToAllys = CurrentTurnHolder.m_Skills[EnemySkillChosen].UseSkill(CurrentTurnHolder.GetAllMagic());

                        //Setting the Notfication to the skillname
                        Image_Notification.SetActive(true);
                        Text_Notification.text = CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillName();

                        CurrentTurnHolder.EndTurn();

                        for (int i = 0; i < TurnOrderEnemy.Count; i++)
                        {
                            CurrentTurnHolder.IncrementMana(5);
                            TurnOrderEnemy[i].AddBuff(BuffToAllys);
                        }

                        CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[EnemySkillChosen].GetCostToUse());



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

                        Text_Notification.text = CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillName();

                        m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.EnemyAttacking);
                        for (int i = 0; i < TurnOrderAlly.Count; i++)
                        {
                            m_Skillparticleeffect.Add(Instantiate<ParticleSystem>(CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillParticleEffect()));
                            TurnOrderAlly[i].IncrementMana(5);
                            m_Skillparticleeffect[i].transform.localPosition = TurnOrderAlly[i].ModelInGame.transform.position;

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
                        if (CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillName().Equals("Attack"))
                        {
                            CurrentTurnHolder.ModelInGame.transform.position = Vector3.Lerp(CurrentTurnHolder.ModelInGame.transform.position, TurnOrderAlly[m_EnemyChosen].GetSpawnPosition(), Time.deltaTime * 2);


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

                        // Image_Notification.SetActive(true);

                        Text_Notification.text = CurrentTurnHolder.m_Skills[EnemySkillChosen].GetSkillName();

                        //m_BattleStates = BattleStates.EnemyAttacking;
                        if (Vector3.Distance(CurrentTurnHolder.ModelInGame.transform.position, TurnOrderAlly[m_EnemyChosen].GetSpawnPosition()) < 35.00f)
                        {

                            m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.EnemyAttackingMelee);
                            TurnOrderAlly[m_EnemyChosen].IncrementMana(5);
                            StartCoroutine(TurnOrderAlly[m_EnemyChosen].DecrementHealth(DamageToAllys, CurrentTurnHolder.m_Skills[EnemySkillChosen].GetElementalType(), 0.01f, 0.1f, 0.2f));
                            CombatTurnEndCombatManagerEnemy();


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


    public void PlayersTurn()
    {
        RemoveDeadFromList();
        if (CurrentTurnHolder.m_creaturesAilment != Script_Creatures.CreaturesAilment.Sleep)
        {
            //Extra
            if (Attackisfinished == false)
            {
               
                if (CurrentTurnHolder.CurrentMana > CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse())
                {


                    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Extra)
                    {
                        string SkillName = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();

                        if (SkillName.Equals("Pass Turn"))
                        {
                            CombatTurnEndCombatManager();

                        }


                    }

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


                            //Setting the Notfication to the skillname
                            Image_Notification.SetActive(true);
                            Text_Notification.text = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();

                            //  m_BattleStates = BattleStates.AllySelecting;

                            CurrentTurnHolder.IncrementMana(5);
                            StartCoroutine(TurnOrderEnemy[m_EnemyChosen].DecrementHealth(DamageToEnemys, CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetElementalType(),0.01f, 0.2f, 0.5f));


                            CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());
                            CombatTurnEndCombatManager();



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


                            for (int i = 0; i < TurnOrderEnemy.Count; i++)
                            {
                                m_Skillparticleeffect.Add(Instantiate<ParticleSystem>(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillParticleEffect()));
                                TurnOrderEnemy[i].IncrementMana(5);
                                m_Skillparticleeffect[i].transform.localPosition = TurnOrderEnemy[i].ModelInGame.transform.position;

                                StartCoroutine(TurnOrderEnemy[i].DecrementHealth(DamageToEnemys, CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetElementalType(),0.001f, 2.0f, 1.0f));
    
                            }

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

                            CurrentTurnHolder.EndTurn();

                            for (int i = 0; i < TurnOrderAlly.Count; i++)
                            {
                                CurrentTurnHolder.IncrementMana(5);
                                TurnOrderAlly[i].AddBuff(BuffToAllys);
                            }

                            CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());
                            CombatTurnEndCombatManager();

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

                            CurrentTurnHolder.EndTurn();

                            for (int i = 0; i < TurnOrderEnemy.Count; i++)
                            {
                                CurrentTurnHolder.IncrementMana(5);
                                TurnOrderEnemy[i].InflictAliment(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetAlimentType());
                            }

                            CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());
                            CombatTurnEndCombatManager();

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

                                CombatTurnEndCombatManager();



                            }
                        }



                        if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Resurrect)
                        {
                            //Checking the range the skills has single target or fulltarget
                            if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.SingleTarget)
                            {

                                if (DeadAllys.Count > 0)
                                {


                                    //Setting the Notfication to the skillname
                                    Image_Notification.SetActive(true);
                                    Text_Notification.text = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();



                                    CurrentTurnHolder.IncrementMana(5);
                                    DeadAllys[0].IncrementHealth(200);
                                    DeadAllys[0].Resurrection();
                                    TurnOrderAlly.Add(DeadAllys[0]);
                                    DeadAllys.RemoveAt(0);


                                    CurrentTurnHolder.DecrementMana(CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetCostToUse());


                                    CombatTurnEndCombatManager();


                                }
                            }

                        }
                    }
                }
            }
        }
        else
        {
            AmountofTurns--;
            RemoveDeadFromList();
            CurrentTurnHolder.EndTurn();

            if (AmountofTurns != 0)
            {
                CurrentTurnHolderNumber++;

            }
        }

    }

    public void PlayerSelecting()
    {

        if (CurrentTurnHolder.m_creaturesAilment != Script_Creatures.CreaturesAilment.Sleep)
        {
            if (Input.GetKeyDown("space"))
            {
                m_BattleStates = BattleStates.AllyAttack;
            }
            //Extra
            if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Extra)
            {
                string SkillName = CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillName();

                if (SkillName.Equals("Pass Turn"))
                {
                    CombatTurnEndCombatManager();

                }


            }

            //Attack single target

            if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Attack)
            {
                //Checking the range the skills has single target or fulltarget
                if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Script_Skills.SkillRange.SingleTarget)
                {
                    for (int i = 0; i < TurnOrderEnemy.Count; i++)
                    {
                        m_CurrentEnemyMenuButtons.Add(Instantiate<Script_ButtonEnemyWrapper>(m_ButtonEnemyReference, gameObject.transform));
                        m_CurrentEnemyMenuButtons[i].gameObject.transform.position = new Vector3(48-0, 85 + i * 43, 0);
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

                    m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.AllyAttackSelecting);

                }
            }

            if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Script_Skills.SkillType.Buff)
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

                        m_BattleCamera.SetCameraState(Script_CombatCameraController.CameraState.AllyHealingSelecting);


                    }
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





    public void CurrentTurnHolderSkill1()
    {
       

    }
    public void CurrentTurnHolderSkill2()
    {

        if (m_CurrentTurnHolderbuttonsHaveSpawned == false)
        {
            
           // Canvas_CommandBoard.SetActive(false);
            for (int i = 0; i < CurrentTurnHolder.m_Skills.Length; i++)
            {
                m_CurrentSkillMenuButtonsMenu.Add(Instantiate<Script_ButtonSkillWrapper>(m_ButtonReference, gameObject.transform));
                m_CurrentSkillMenuButtonsMenu[i].gameObject.transform.position = new Vector3(-1540, 115 + i * 31, 0);
                m_CurrentSkillMenuButtonsMenu[i].SetupButton(CurrentTurnHolder, CurrentTurnHolder.m_Skills[i], i, this);
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

    public void CombatEnd()
    {

        RemoveDeadFromList();
        for (int i = TurnOrderAlly.Count; i > 0; i--)
        {
            Destroy(TurnOrderAlly[0].ModelInGame);
            TurnOrderAlly.RemoveAt(0);
        }
        PartyManager.CombatEnd();

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
        Attackisfinished = true;
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

        for (int i = 0; i < TurnOrderEnemy.Count; i++)
        {
            TurnOrderEnemy[i].ModelInGame.transform.localPosition = TurnOrderEnemy[i].GetSpawnPosition();

        }

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
