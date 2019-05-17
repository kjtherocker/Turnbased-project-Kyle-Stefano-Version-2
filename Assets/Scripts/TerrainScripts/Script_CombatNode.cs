using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Script_CombatNode : MonoBehaviour
{
    public enum CombatNodeTypes
    {
        Normal,
        Covered,
        Wall,
        Empty
    }
    public float m_Heuristic;


    public bool m_IsGoal;
    public bool m_HeuristicCalculated;
    public bool m_IsSelector;
    public bool m_IsWalkable;
    public bool m_OpenListHasFinished;

    public Vector2Int m_PositionInGrid;

    public Script_CombatNode m_NodeYouCameFrom;

    public Script_Creatures m_CreatureOnGridPoint;

    public GameObject m_WalkablePlane;
    public GameObject m_AttackingPlane;

    public GameObject m_Cube;

    public Material m_Selector;
    public Material m_Walkable;
    public Material m_Goal;
    public Renderer m_Renderer;



    public Script_Grid m_Grid;
    public Script_CombatNode[,] m_GridPathArray;
    public List<Script_CombatNode> m_OpenList;

    public CombatNodeTypes m_CombatsNodeType;

    public int m_Movement;
    // Use this for initialization
    void Start()
    {
        m_Movement = 4;
        //m_HeuristicCalculated = false;
        m_WalkablePlane.gameObject.SetActive(false);
        m_AttackingPlane.gameObject.SetActive(false);
        m_Cube.gameObject.SetActive(true);
        m_IsSelector = false;
        m_OpenListHasFinished = false;

        m_Grid = Script_GameManager.Instance.m_Grid;

        m_GridPathArray = m_Grid.m_GridPathArray;
    }

    // Update is called once per frame
    void Update()
    {

        if (m_CombatsNodeType == CombatNodeTypes.Empty)
        {
            m_Cube.gameObject.SetActive(false);
        }
        if (m_IsWalkable == true)
        {
            m_WalkablePlane.gameObject.SetActive(true);
        }

        if (m_NodeYouCameFrom != null)
        {
            if (m_NodeYouCameFrom.m_OpenListHasFinished == true && m_OpenListHasFinished == false)
            {
                LoopthroughOpenList();
            }
        }


    }

    public void CreateWalkableArea()
    {
        if (m_Heuristic <= m_Movement && m_Heuristic != 0 && m_Heuristic != -1)
        {
            m_WalkablePlane.gameObject.SetActive(true);
            m_WalkablePlane.GetComponent<Renderer>().material = m_Walkable;
            m_IsWalkable = true;
        }
    }

    public void RemoveWalkableArea()
    {

        //m_Heuristic = 0;
        m_WalkablePlane.gameObject.SetActive(false);
        //m_WalkablePlane.GetComponent<Renderer>().material = m_Walkable;
        m_IsWalkable = false;

    }


    public Script_CombatNode CheckIfNodeIsClearAndReturnNodeIndex(Vector2Int aGrid)
    {
        // if the node is out of bounds, return -1 (an invalid tile index)
        if (aGrid.x < 0 || aGrid.x >= m_Grid.m_GridDimensions.x ||
            aGrid.y < 0 || aGrid.y >= m_Grid.m_GridDimensions.y)
            return null;

        Script_CombatNode nodeIndex = m_GridPathArray[aGrid.x, aGrid.y];

        // if the node is already closed, return -1 (an invalid tile index)
        if (m_Grid.m_GridPathArray[aGrid.x, aGrid.y].m_HeuristicCalculated == true)
            return null;

        // if the node can't be walked on, return -1 (an invalid tile index)
        if (m_Grid.m_GridPathArray[aGrid.x, aGrid.y].m_CombatsNodeType != Script_CombatNode.CombatNodeTypes.Normal)
            return null;

        // return a valid tile index
        return nodeIndex;
    }

    public void AddNeighboursToOpenList()
    {

        // create an array of the four neighbour tiles
        Script_CombatNode[] nodestoads = new Script_CombatNode[4];
        nodestoads[0] = CheckIfNodeIsClearAndReturnNodeIndex(new Vector2Int(m_PositionInGrid.x, m_PositionInGrid.y - 1));
        nodestoads[1] = CheckIfNodeIsClearAndReturnNodeIndex(new Vector2Int(m_PositionInGrid.x, m_PositionInGrid.y + 1));
        nodestoads[2] = CheckIfNodeIsClearAndReturnNodeIndex(new Vector2Int(m_PositionInGrid.x + 1, m_PositionInGrid.y));
        nodestoads[3] = CheckIfNodeIsClearAndReturnNodeIndex(new Vector2Int(m_PositionInGrid.x - 1, m_PositionInGrid.y));

        // loop through the array
        for (int i = 0; i < 4; i++)
        {
            Script_CombatNode NodeToAdd;
            NodeToAdd = nodestoads[i];

            // check if the node to add has a valid node index
            if (NodeToAdd != null)
            {

                NodeToAdd.m_Heuristic = m_Heuristic + 1;
                NodeToAdd.m_HeuristicCalculated = true;
                NodeToAdd.m_NodeYouCameFrom = this;
                m_OpenList.Add(NodeToAdd);

            }
        }
    }


    public void AddNeighboursToOpenListGoal()
    {

        // create an array of the four neighbour tiles
        Script_CombatNode[] nodestoads = new Script_CombatNode[4];
        nodestoads[0] = CheckIfNodeIsClearAndReturnNodeIndex(new Vector2Int(m_PositionInGrid.x, m_PositionInGrid.y - 1));
        nodestoads[1] = CheckIfNodeIsClearAndReturnNodeIndex(new Vector2Int(m_PositionInGrid.x, m_PositionInGrid.y + 1));
        nodestoads[2] = CheckIfNodeIsClearAndReturnNodeIndex(new Vector2Int(m_PositionInGrid.x + 1, m_PositionInGrid.y));
        nodestoads[3] = CheckIfNodeIsClearAndReturnNodeIndex(new Vector2Int(m_PositionInGrid.x - 1, m_PositionInGrid.y));

        // loop through the array
        for (int i = 0; i < 4; i++)
        {
            Script_CombatNode NodeToAdd;
            NodeToAdd = nodestoads[i];

            // check if the node to add has a valid node index
            if (NodeToAdd != null)
            {

                NodeToAdd.m_Heuristic = m_Heuristic + 1;
                NodeToAdd.m_HeuristicCalculated = true;
                NodeToAdd.m_NodeYouCameFrom = this;
                m_OpenList.Add(NodeToAdd);

            }
        }

        LoopthroughOpenList();
    }



    public void LoopthroughOpenList()
    {
        for (int i = m_OpenList.Count - 1; i >= 0; i--)
        {

            m_OpenList[i].AddNeighboursToOpenList();
            m_OpenList.RemoveAt(i);
        }
        m_OpenListHasFinished = true;
    }

}



