using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using TMPro;


public class CombatManager : MonoBehaviour
{

    public PartyManager PartyManager;
    public EncounterManager EncounterManager;
    public GameManager GameManager;

    public Grid m_Grid;

    //For the skills

    public Creatures CurrentTurnHolder;

    bool WhichSidesTurnIsIt;
    bool CombatHasStarted;

    int m_EnemyAiCurrentlyInList;


    public CombatCameraController m_BattleCamera;

    public GridFormations m_GridFormation;

    public Vector3 CreatureOffset;

    private GameObject m_Gridformation;


    public TextMeshProUGUI m_TurnSwitchText;

    public List<TurnIndicatorWrapper> m_TurnIdenticator;

    public List<Creatures> DeadAllys;
    public List<Creatures> TurnOrderAlly;
    public List<Creatures> TurnOrderEnemy;
    public List<Creatures> CurrentTurnOrderSide;


    public enum BattleStates
    {
        NoTurn,
        Spawn,
        EnemyTurn,
        AllyTurn,
    
        EndOfCombat


    }

    public BattleStates m_BattleStates;

    void Start()
    {
        CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid, 0);


        GameManager = GameManager.Instance;
        EncounterManager = GameManager.Instance.EncounterManager;
        PartyManager = GameManager.Instance.PartyManager;




    }

    public void CombatStart()
    {
        if (CombatHasStarted == false)
        {


            //Setting up the players

            m_Gridformation = Instantiate<GameObject>(m_GridFormation.gameObject);


            m_Grid.Convert1DArrayto2D(m_Gridformation.GetComponent<GridFormations>().m_ListToConvert,
                m_Gridformation.GetComponent<GridFormations>().m_GridDimensions);
            

            AddCreatureToCombat(PartyManager.m_CurrentParty[0], new Vector2Int(3, 2), TurnOrderAlly);
          
            AddCreatureToCombat(PartyManager.m_CurrentParty[1], new Vector2Int(3, 6), TurnOrderAlly);
            
            AddCreatureToCombat(PartyManager.m_CurrentParty[2], new Vector2Int(12, 4), TurnOrderAlly);
                                                                                 
            AddCreatureToCombat(PartyManager.m_CurrentParty[3], new Vector2Int(12, 5), TurnOrderAlly);


            //Setting up the Enemy
            AddCreatureToCombat(EncounterManager.EnemySlot4, new Vector2Int(9, 13), TurnOrderEnemy);



    
            CombatHasStarted = true;


            //AmountofTurns = TurnOrderAlly.Count;
            //for (int i = 0; i < AmountofTurns; i++)
            //{
            //    m_TurnIdenticator.Add(Instantiate<TurnIndicatorWrapper>(m_ImageReference));
            //    m_TurnIdenticator[i].gameObject.transform.localPosition = new Vector3( -365 + i * 10, 100, 0);
            //    m_TurnIdenticator[i].gameObject.transform.SetParent(Canvas_TurnMenu.transform, false);
            //}
            
            m_BattleStates = BattleStates.AllyTurn;

            CurrentTurnOrderSide = TurnOrderAlly;
            //Canvas_CommandBoard.SetActive(false);
            //Canvas_CombatEndMenu.Reset();
            WhichSidesTurnIsIt = false;


        }

    }

    public void AddCreatureToCombat(Creatures aCreature, Vector2Int aPosition, List<Creatures> aList)
    {
        if (aCreature == null)
        {
            Debug.Log("Creature does not exist Position" + aPosition.ToString());
            return;

        }

        aList.Add(aCreature);

        int TopElement = aList.Count - 1;


        aList[TopElement].ModelInGame = Instantiate<GameObject>(aList[TopElement].Model);
        aList[TopElement].ModelInGame.transform.position = m_Grid.m_GridPathArray[aPosition.x, aPosition.y].gameObject.transform.position + CreatureOffset;
        aList[TopElement].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, 180, 0.0f);
        aList[TopElement].m_CreatureAi = aList[aList.Count - 1].ModelInGame.GetComponent<AiController>();
        aList[TopElement].m_CreatureAi.m_Position =
            m_Grid.m_GridPathArray[aPosition.x, aPosition.y].m_PositionInGrid;

        aList[TopElement].m_CreatureAi.m_Grid = m_Grid;

        aList[TopElement].m_CreatureAi.m_Movement = aCreature.m_CreatureMovement;

        aList[TopElement].m_CreatureAi.m_Creature = aList[aList.Count - 1];


        m_Grid.m_GridPathArray[aPosition.x, aPosition.y].GetComponent<CombatNode>().m_CreatureOnGridPoint = aList[TopElement];
        m_Grid.m_GridPathArray[aPosition.x, aPosition.y].GetComponent<CombatNode>().m_CombatsNodeType = CombatNode.CombatNodeTypes.Covered;


        //aList[aList.Count - 1].ModelInGame.transform.localScale = new Vector3(0.02448244f, 0.02448244f, 0.02448244f);

    }




    void Update()
    {


       switch (m_BattleStates)
       {
           case BattleStates.Spawn:
             
                   m_BattleStates = BattleStates.AllyTurn;


             
               break;

           case BattleStates.AllyTurn:
                //isPlayersDoneMoving();
                if (Input.GetButtonDown("Ps4_Triangle") )
                {
                    StartCoroutine(EnemyTurn());
                }


                break;

            case BattleStates.EnemyTurn:

                if (Input.GetButtonDown("Ps4_Triangle") )
                {
                    StartCoroutine(AllyTurn());
                }


                break;



            case BattleStates.EndOfCombat:

               if (Input.anyKey)
               {
                   //CombatEnd();
               }
               break;
       }

        

    }




    private bool isPlayersDoneMoving()
    {
        for (int i = 0; i < 1; i++)
        {
            if (TurnOrderAlly[i].m_CreatureAi.m_HasMovedForThisTurn == true)
            { 
                return false;
            }
            
        }

        StartCoroutine(AllyTurn());
        return true;
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

    }

    public IEnumerator EnemyTurn()
    {
        CurrentTurnOrderSide = TurnOrderEnemy;

        m_BattleStates = BattleStates.EnemyTurn;

        m_TurnSwitchText.gameObject.SetActive(true);
        m_TurnSwitchText.text = "ENEMY TURN";
        m_TurnSwitchText.color = Color.red;

        yield return new WaitForSeconds(2f);
        m_TurnSwitchText.gameObject.SetActive(false);

        foreach (Creatures creature in CurrentTurnOrderSide)
        {

            EnemyAiController EnemyTemp = creature.m_CreatureAi as EnemyAiController;

            EnemyTemp.EnemyMovement();

            
        }

       m_EnemyAiCurrentlyInList = 0;
       EnemyMovement();

    }

    public void EnemyMovement()
    {

        if (m_EnemyAiCurrentlyInList == TurnOrderEnemy.Count )
        {
            StartCoroutine(AllyTurn());
            return ;
        }

        EnemyAiController EnemyTemp = TurnOrderEnemy[m_EnemyAiCurrentlyInList].m_CreatureAi as EnemyAiController;
       // EnemyTemp.EnemyWalkToTarget();

        m_EnemyAiCurrentlyInList++;
        

    }


    public IEnumerator AllyTurn()
    {
        CurrentTurnOrderSide = TurnOrderAlly;

        m_BattleStates = BattleStates.AllyTurn;

        m_TurnSwitchText.gameObject.SetActive(true);
        m_TurnSwitchText.text = "PLAYER TURN";
        m_TurnSwitchText.color = Color.blue;


        foreach (Creatures creature in CurrentTurnOrderSide)
        {
            creature.m_CreatureAi.m_HasMovedForThisTurn = false;
            creature.m_CreatureAi.m_HasAttackedForThisTurn = false;
        }

        yield return new WaitForSeconds(2f);
        m_TurnSwitchText.gameObject.SetActive(false);
    }


    public void PlayerTurnSkill()
    {

          //if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillType() == Skills.SkillType.Attack)
          //{
          //    //Checking the range the skills has single target or fulltarget
          //    if (CurrentTurnHolder.m_Skills[CurrentTurnHolderSkills].GetSkillRange() == Skills.SkillRange.SingleTarget)
          //    {

          //    }
          //}
    }

    public void PlayerTurnBloodArt()
    {

    }

    public void PlayerSelecting()
    {


    }

    public bool SwitchTurnSides()
    {
        WhichSidesTurnIsIt = !WhichSidesTurnIsIt;

        return WhichSidesTurnIsIt;

    }

}
