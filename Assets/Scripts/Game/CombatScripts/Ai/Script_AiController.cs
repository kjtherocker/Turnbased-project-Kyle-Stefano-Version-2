﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Script_AiController : MonoBehaviour
{
    public Script_Grid m_Grid;
    public Script_CombatNode[,] m_GridPathArray;
    public List<Script_CombatNode> m_GridPath;
    public Vector2Int m_Goal;
    public Vector2Int m_Position;
    public Vector2Int m_InitalPosition;

    public Pathfinder _Pathfinder;
    public Script_CombatNode Node_MovingTo;
    public Script_CombatNode Node_ObjectIsOn;
    public Animator m_CreaturesAnimator;
    public Script_Creatures m_Creature;

    public Dictionary<Script_CombatNode, List<Script_CombatNode>> cachedPaths = null;

    protected HashSet<Script_CombatNode> _pathsInRange;

    public Vector3 CreatureOffset;

    public int m_Movement;
    public int m_Jump;

    public bool m_MovementHasStarted;
    public bool m_HasAttackedForThisTurn;
    public bool m_HasMovedForThisTurn;


    // Use this for initialization
    public virtual void Start ()
    {
        //m_Goal = new Vector2Int(9, 2);
        //m_Position = new Vector2Int(4, 4);
        CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid, 0);
        m_Jump = 2;
        m_HasMovedForThisTurn = false;
        m_MovementHasStarted = false;
        m_InitalPosition = m_Position;

        Node_ObjectIsOn = Script_GameManager.Instance.m_Grid.GetNode(m_Position);
        Node_MovingTo = Node_ObjectIsOn;


        m_Grid = Script_GameManager.Instance.m_Grid;

        m_GridPathArray = m_Grid.m_GridPathArray;


        _Pathfinder = new Pathfinder();

    }

    // Update is called once per frame
    public virtual void Update ()
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

    public void SetGoal(Vector2Int m_Goal)
    {
      m_Grid.SetHeuristicToZero();
      m_Grid.m_GridPathToGoal.Clear();
      m_Grid.RemoveWalkableArea();
      m_Grid.m_GridPathArray[m_Goal.x, m_Goal.y].m_IsGoal = true;
      


    }

    public void FindAllPaths()
    {
        _pathsInRange = GetAvailableDestinations(m_Grid.m_GridPathList, Node_ObjectIsOn,m_Movement);


        foreach (Script_CombatNode node in _pathsInRange)
        {
            node.CreateWalkableArea();
        }
    }

    public void DeselectAllPaths()
    {
        if (_pathsInRange != null)
        {

            foreach (Script_CombatNode node in _pathsInRange)
            {
                node.m_Heuristic = 0;
                node.RemoveWalkableArea();
            }
        }
    }

    public HashSet<Script_CombatNode> GetAvailableDestinations(List<Script_CombatNode> cells, Script_CombatNode NodeHeuristicIsBasedOff, int Range)
    {
        cachedPaths = new Dictionary<Script_CombatNode, List<Script_CombatNode>>();

        var paths = cachePaths(cells, NodeHeuristicIsBasedOff);
        foreach (var key in paths.Keys)
        {
            var path = paths[key];
            
            var pathCost = path.Sum(c => c.m_MovementCost);
            key.m_Heuristic = pathCost;
            if (pathCost <= Range)
            {
                cachedPaths.Add(key, path);
            }
        }
        return new HashSet<Script_CombatNode>(cachedPaths.Keys);
    }


    public virtual IEnumerator SetGoalPosition(Vector2Int m_Goal)
    {
        SetGoal(m_Goal);
        m_Grid.m_Movement = m_Movement;

        _pathsInRange = GetAvailableDestinations(m_Grid.m_GridPathList, m_Grid.m_GridPathArray[m_Goal.x, m_Goal.y],100);


        foreach (Script_CombatNode node in _pathsInRange)
        {
            node.m_IsWalkable = true;
        }

        yield return new WaitForSeconds(.1f);

        List<Script_CombatNode> TempList = m_Grid.GetTheLowestH(Node_ObjectIsOn.m_PositionInGrid);


        StartCoroutine(GetToGoal(TempList));

    }

    public virtual IEnumerator GetToGoal(List<Script_CombatNode> aListOfNodes)
    {
        m_MovementHasStarted = true;
        //m_Grid.RemoveWalkableArea();
        m_CreaturesAnimator.SetBool("b_IsWalking", true);
        Script_GameManager.Instance.m_BattleCamera.m_cameraState = Script_CombatCameraController.CameraState.PlayerMovement;
        Node_ObjectIsOn.m_CreatureOnGridPoint = null;
        Node_ObjectIsOn.m_CombatsNodeType = Script_CombatNode.CombatNodeTypes.Normal;
        for (int i = 0; i < aListOfNodes.Count;)
        {

                if (Node_MovingTo == Node_ObjectIsOn)
                {

                    Node_MovingTo = aListOfNodes[i];

                   


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
        Script_GameManager.Instance.m_BattleCamera.m_cameraState = Script_CombatCameraController.CameraState.Normal;

        //Setting the Walk Animation
        m_CreaturesAnimator.SetBool("b_IsWalking", false);

        //The walk has been finished
       //m_HasMovedForThisTurn = true;

        m_MovementHasStarted = false;
        //Changing the position from where the Creature was before
      

        m_Position = aListOfNodes[aListOfNodes.Count - 1].m_PositionInGrid;

        //Setting the node you are on to the new one
        Node_ObjectIsOn = Script_GameManager.Instance.m_Grid.GetNode(m_Position);

        Node_ObjectIsOn.m_CreatureOnGridPoint = m_Creature;
        Node_ObjectIsOn.m_CombatsNodeType = Script_CombatNode.CombatNodeTypes.Covered;

       // m_Grid.RemoveWalkableArea();

        for (int i = aListOfNodes.Count; i < 0; i--)
        {
            aListOfNodes.RemoveAt(i);
        }

        
    }

    public virtual Dictionary<Script_CombatNode, List<Script_CombatNode>> cachePaths(List<Script_CombatNode> cells, Script_CombatNode aNodeHeuristicIsBasedOn)
    {
        var edges = GetGraphEdges(cells);
        var paths = _Pathfinder.findAllPaths(edges, aNodeHeuristicIsBasedOn);
        return paths;
    }

    public virtual bool CheckIfNodeIsClearAndReturnNodeIndex(Script_CombatNode aNode)
    {
        // if the node is out of bounds, return -1 (an invalid tile index)

        if (aNode == null)
        {
            Debug.Log("YOU BROKE " + aNode.m_PositionInGrid.ToString());
        }

        Script_CombatNode nodeIndex = aNode;

        // if the node is already closed, return -1 (an invalid tile index)
        if (nodeIndex.m_HeuristicCalculated == true)
        {
            return false;
        }
        // if the node can't be walked on, return -1 (an invalid tile index)
        if (nodeIndex.m_CombatsNodeType != Script_CombatNode.CombatNodeTypes.Normal)
        {
            return false;
        }

        if (nodeIndex.m_PositionInGrid == m_Position)
        {
            return false;
        }


        if (nodeIndex.m_NodeHeight > 0)
        {
            return false;
        }
        // return a valid tile index
        return true;
    }

    protected virtual Dictionary<Script_CombatNode, Dictionary<Script_CombatNode, int>> GetGraphEdges(List<Script_CombatNode> NodeList)
    {
        Dictionary<Script_CombatNode, Dictionary<Script_CombatNode, int>> ret = new Dictionary<Script_CombatNode, Dictionary<Script_CombatNode, int>>();

        foreach (Script_CombatNode Node in NodeList)
        {
            if (CheckIfNodeIsClearAndReturnNodeIndex(Node) == true|| Node.Equals(Node_ObjectIsOn))
            {
                ret[Node] = new Dictionary<Script_CombatNode, int>();
                foreach (Script_CombatNode neighbour in Node.GetNeighbours(NodeList))
                {
                    if (CheckIfNodeIsClearAndReturnNodeIndex(neighbour) == true)
                    {
                        ret[Node][neighbour] = neighbour.m_MovementCost;
                    }
                }
            }
        }
        return ret;
    }

    public virtual void ReturnToInitalPosition()
    {
        if (m_MovementHasStarted == false)
        {


            Script_GameManager.Instance.m_Grid.GetNode(m_Position).m_CreatureOnGridPoint = null;
            Script_GameManager.Instance.m_Grid.GetNode(m_Position).m_CombatsNodeType = Script_CombatNode.CombatNodeTypes.Normal;

            m_Position = m_InitalPosition;

            CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid + Script_GameManager.Instance.m_Grid.GetNode(m_Position).m_NodeHeightOffset, 0);

            Script_GameManager.Instance.m_Grid.GetNode(m_Position).m_CreatureOnGridPoint = m_Creature;
            gameObject.transform.position = Script_GameManager.Instance.m_Grid.GetNode(m_Position).transform.position + CreatureOffset;

            m_HasMovedForThisTurn = false;
        }

    }
}
