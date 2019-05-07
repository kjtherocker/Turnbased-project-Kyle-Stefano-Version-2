using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_AiController : MonoBehaviour
{
    public Script_Grid m_Grid;
    public List<Script_CombatNode> m_GridPath;
    public Vector2Int m_Goal;
    public Vector2Int m_Position;
    public Vector2Int m_InitalPosition;


    public Script_CombatNode Node_MovingTo;
    public Script_CombatNode Node_ObjectIsOn;
    public Animator m_CreaturesAnimator;
    public Script_Creatures m_Creature;

    public Vector3 CreatureOffset;

    public int m_Movement;
    public int m_Jump;

    public bool m_MovementHasStarted;
    public bool m_HasAttackedForThisTurn;
    public bool m_HasMovedForThisTurn;


    // Use this for initialization
    void Start ()
    {
        //m_Goal = new Vector2Int(9, 2);
        //m_Position = new Vector2Int(4, 4);
        CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid, 0);
        m_Movement = 4;
        m_Jump = 2;
        m_HasMovedForThisTurn = false;
        m_MovementHasStarted = false;
        m_InitalPosition = m_Position;

        Node_ObjectIsOn = Script_GameManager.Instance.m_Grid.GetNode(m_Position);
        Node_MovingTo = Node_ObjectIsOn;
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Node_ObjectIsOn != Node_MovingTo)
        {
            transform.position = Vector3.MoveTowards
                    (transform.position, Node_MovingTo.gameObject.transform.position + CreatureOffset,
                    8 * Time.deltaTime);
        }

        if (m_MovementHasStarted == true)
        {
            if (transform.position == Node_MovingTo.transform.position + CreatureOffset)
            {
                Node_ObjectIsOn = Node_MovingTo;
            }
        }
    }

    public void SetGoalPosition(Vector2Int m_Goal)
    {
        m_Grid.SetGoal(m_Goal);
        m_Grid.GetTheLowestH(m_Position, this);
    }

    public IEnumerator GetToGoal(List<Script_CombatNode> aListOfNodes)
    {
        m_MovementHasStarted = true;
        m_Grid.RemoveWalkableArea();
        m_CreaturesAnimator.SetBool("b_IsWalking", true);
        Script_GameManager.Instance.m_BattleCamera.m_PlayerIsMoving = true;
        for (int i = 0; i < aListOfNodes.Count;)
        {
            if (Node_MovingTo != aListOfNodes[aListOfNodes.Count - 1])
            {
                if (Node_MovingTo == Node_ObjectIsOn)
                {

                    Node_MovingTo = aListOfNodes[i];

                    Vector3 relativePos = aListOfNodes[i].gameObject.transform.position - transform.position + CreatureOffset;


                    m_Position = Node_MovingTo.m_PositionInGrid;

                    Script_GameManager.Instance.m_BattleCamera.m_CameraPositionInGrid = m_Position;

                    transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                    i++;
                    yield return new WaitForSeconds(0.4f);
                }
            }

        }

        //Camera no longer following the player;
        Script_GameManager.Instance.m_BattleCamera.m_PlayerIsMoving = false;

        //Setting the Walk Animation
        m_CreaturesAnimator.SetBool("b_IsWalking", false);

        //The walk has been finished
        m_HasMovedForThisTurn = true;

        m_MovementHasStarted = false;
        //Changing the position from where the Creature was before
        aListOfNodes[0].m_CreatureOnGridPoint = null;
        aListOfNodes[aListOfNodes.Count - 1].m_CreatureOnGridPoint = m_Creature;
        m_Position = aListOfNodes[aListOfNodes.Count - 1].m_PositionInGrid;
        Node_ObjectIsOn = Script_GameManager.Instance.m_Grid.GetNode(m_Position);
    }

    public void ReturnToInitalPosition()
    {
        
        m_Position = m_InitalPosition;

        gameObject.transform.position = Script_GameManager.Instance.m_Grid.GetNode(m_Position).transform.position + CreatureOffset;
        m_HasMovedForThisTurn = false;


    }


    public void SpawnWalkableTiles()
    {
        m_Grid.m_Movement = m_Movement;
        m_Grid.SetWalkingHeuristic(m_Position);

        //yield return new WaitForSeconds(1);
        m_Grid.SetWalkableArea();
    }

  
}
