using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_EnemyAiController : Script_AiController
{
    // Start is called before the first frame update

    public Script_AiController m_Target;
    public bool m_AiFinished;
    public int m_EnemyRange;

    public override void Start()
    {
        m_EnemyRange = 3;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        
       
        
    }


    public void EnemyWalkToTarget()
    {
        m_Target = Script_GameManager.Instance.PartyManager.m_CurrentParty[0].m_CreatureAi;
        StartCoroutine(SetGoalPosition(m_Target.m_Position));
    }

    public override IEnumerator GetToGoal(List<Script_CombatNode> aListOfNodes)
    {
        m_MovementHasStarted = true;
        m_Grid.RemoveWalkableArea();
        m_CreaturesAnimator.SetBool("b_IsWalking", true);
        Script_GameManager.Instance.m_BattleCamera.m_PlayerIsMoving = true;
        Node_ObjectIsOn.m_CreatureOnGridPoint = null;
        Node_ObjectIsOn.m_CombatsNodeType = Script_CombatNode.CombatNodeTypes.Normal;
        for (int i = 0; i < aListOfNodes.Count;)
        {

            if (Node_MovingTo == Node_ObjectIsOn)
            {

                Node_MovingTo = aListOfNodes[i];

                if (Node_MovingTo.m_Heuristic == m_EnemyRange)
                {
                    break;
                }


                Vector3 relativePos = aListOfNodes[i].gameObject.transform.position - transform.position + CreatureOffset;


                m_Position = Node_MovingTo.m_PositionInGrid;

                Script_GameManager.Instance.m_BattleCamera.m_CameraPositionInGrid = m_Position;


                transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);

                CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid + Node_MovingTo.m_NodeHeightOffset, 0);
                i++;
                yield return new WaitForSeconds(0.4f);
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


        m_Position = aListOfNodes[aListOfNodes.Count - 1].m_PositionInGrid;

        //Setting the node you are on to the new one
        Node_ObjectIsOn = Script_GameManager.Instance.m_Grid.GetNode(m_Position);

        Node_ObjectIsOn.m_CreatureOnGridPoint = m_Creature;
        Node_ObjectIsOn.m_CombatsNodeType = Script_CombatNode.CombatNodeTypes.Covered;

        m_AiFinished = true;

        for (int i = aListOfNodes.Count; i < 0; i--)
        {
            aListOfNodes.RemoveAt(i);
        }


    }

}
