using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class Script_CombatNode : Cell
{
    public enum CombatNodeTypes
    {
        Normal,
        Covered,
        Wall,
        Empty
    }
    public int m_Heuristic;


    public bool m_IsGoal;
    public bool m_HeuristicCalculated;
    public bool m_IsSelector;
    public bool m_IsWalkable;
    public bool m_OpenListHasFinished;

    public Vector2Int m_PositionInGrid;

    public Script_CombatNode m_NodeYouCameFrom;

    public Script_Creatures m_CreatureOnGridPoint;

    List<Script_CombatNode> neighbours = null;

    public GameObject m_WalkablePlane;
    public GameObject m_CurrentWalkablePlaneBeingUsed;
    public GameObject m_AttackingPlane;
    public GameObject m_Cube;

    public GameObject m_Prop;

    public Script_PropList m_PropList;

    public Material m_Selector;
    public Material m_Walkable;
    public Material m_Goal;
    public Renderer m_Renderer;


    public GameObject m_InitalNode;

    public Vector3 m_NodesInitalVector3Coordinates;


    public Script_Grid m_Grid;
    public Script_CombatNode[,] m_GridPathArray;
    public List<Script_CombatNode> m_OpenList;


    public CombatNodeTypes m_CombatsNodeType;

    public Script_PropList.Props m_PropOnNode;
    Script_PropList.Props m_PropOnNodeTemp;

    public Script_PropList.NodeReplacements m_NodeReplacementOnNode;
    Script_PropList.NodeReplacements m_NodeReplacementTemp;


    public float m_NodeHeightOffset;


    public Script_NodeReplacement m_NodeReplacement;


    public int m_MovementCost;

    public int m_NodeRotation;
    public int m_NodeHeight;



    // Use this for initialization
    void Start()
    {
        m_MovementCost = 1;
        //m_HeuristicCalculated = false;


        if (m_NodeReplacement == null)
        {
            m_CurrentWalkablePlaneBeingUsed = m_WalkablePlane;
        }

        m_CurrentWalkablePlaneBeingUsed.gameObject.SetActive(false);
        m_AttackingPlane.gameObject.SetActive(false);
        m_Cube.gameObject.SetActive(true);
        m_IsSelector = false;
        m_OpenListHasFinished = false;

        if (m_Grid != null)
        {
            m_GridPathArray = m_Grid.m_GridPathArray;
        }
        m_PropList = Script_GameManager.Instance.m_PropList;

        m_PropOnNodeTemp = m_PropOnNode;

        m_NodesInitalVector3Coordinates = gameObject.transform.position;
        SetPropState();
    }

    private void OnEnable()
    {
        m_PropOnNodeTemp = m_PropOnNode;
    }

    public void SetPropState()
    {
        if (m_CombatsNodeType == CombatNodeTypes.Empty)
        {
            m_Cube.gameObject.SetActive(false);
        }

        if (m_CreatureOnGridPoint == null || m_Prop == null)
        {
            //SpawnProp();
        }

        if (m_PropOnNodeTemp != m_PropOnNode)
        {
            DestroyProp();
            SpawnProp();
        }


        if (m_PropOnNode == Script_PropList.Props.None)
        {
            if (m_Prop != null)
            {
                DestroyProp();
            }
        }

        if (m_NodeReplacementTemp != m_NodeReplacementOnNode)
        {
            if (m_NodeReplacement != null)
            {
                DestroyNodeReplacement();
            }
            SpawnNodeReplacement();
        }


        if (m_NodeReplacementOnNode == Script_PropList.NodeReplacements.None)
        {
            if (m_NodeReplacement != null)
            {
                DestroyNodeReplacement();
            }
        }

        if (m_NodeRotation <= 0)
        {
            m_NodeRotation = 4;
        }
        if (m_NodeRotation > 4)
        {
            m_NodeRotation = 1;
        }


        if (m_Prop != null)
        {
            if (m_NodeRotation == 1)
            {
                m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            if (m_NodeRotation == 2)
            {
                m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            if (m_NodeRotation == 3)
            {
                m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
            }
            if (m_NodeRotation == 4)
            {
                m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 360, 0));
            }
        }


        if (m_NodeReplacement != null)
        {
            if (m_NodeRotation == 1)
            {
                m_NodeReplacement.transform.rotation = Quaternion.Euler(new Vector3(m_NodeReplacement.transform.rotation.x, 90, m_NodeReplacement.transform.rotation.y));
            }
            if (m_NodeRotation == 2)
            {
                m_NodeReplacement.transform.rotation = Quaternion.Euler(new Vector3(m_NodeReplacement.transform.rotation.x, 180, m_NodeReplacement.transform.rotation.y));
            }
            if (m_NodeRotation == 3)
            {
                m_NodeReplacement.transform.rotation = Quaternion.Euler(new Vector3(m_NodeReplacement.transform.rotation.x, 270, m_NodeReplacement.transform.rotation.y));
            }
            if (m_NodeRotation == 4)
            {
                m_NodeReplacement.transform.rotation = Quaternion.Euler(new Vector3(m_NodeReplacement.transform.rotation.x, 360, m_NodeReplacement.transform.rotation.y));
            }
        }

        if (m_NodeHeight == 0)
        {
            gameObject.transform.position = m_NodesInitalVector3Coordinates;
        }

        if (m_NodeHeight == 1)
        {
            // gameObject.transform.position = gameObject.transform.position + new Vector3(0, 2, 0);
        }
    }


    public void DestroyNodeReplacement()
    {
       // DestroyImmediate(m_NodeReplacement.gameObject);
       // m_CurrentWalkablePlaneBeingUsed = m_WalkablePlane;
    }

    public void DestroyProp()
    {

        DestroyImmediate(m_Prop);
        m_CombatsNodeType = CombatNodeTypes.Normal;
        
    }

    public void SpawnProp()
    {
        m_PropOnNodeTemp = m_PropOnNode;
        m_Prop = Instantiate(m_PropList.ReturnPropData(m_PropOnNode), this.gameObject.transform);
        Vector3 CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid, 0);
        m_Prop.gameObject.transform.position = gameObject.transform.position + CreatureOffset;

        Script_Creatures CreatureTemp = m_Prop.GetComponent<Script_Creatures>();
        if (CreatureTemp != null)
        {
            m_CreatureOnGridPoint = CreatureTemp;
        }
        m_CombatsNodeType = CombatNodeTypes.Wall;

    }

    public void SpawnNodeReplacement()
    {
        
        if (m_NodeReplacementOnNode != Script_PropList.NodeReplacements.None)
        {
            m_NodeReplacementTemp = m_NodeReplacementOnNode;
            m_NodeReplacement = Instantiate(m_PropList.NodeReplacementData(m_NodeReplacementOnNode), this.gameObject.transform);
            Vector3 CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid, 0);
            m_NodeReplacement.gameObject.transform.position =  gameObject.transform.position + m_NodeReplacement.m_NodeSpawnOffSet + CreatureOffset;
            m_NodeHeightOffset = m_NodeReplacement.m_NodeHeightOffset;
            m_CurrentWalkablePlaneBeingUsed = m_NodeReplacement.m_Walkable;

            if (m_NodeReplacement.m_NodeReplacementType == Script_NodeReplacement.NodeReplacementType.RemoveInitalNode)
            {
                m_InitalNode.gameObject.SetActive(false);
            }

        }
    }


    public void CreateWalkableArea()
    {

         m_CurrentWalkablePlaneBeingUsed.gameObject.SetActive(true);
         m_IsWalkable = true;
        
    }

    public void RemoveWalkableArea()
    {
        m_CurrentWalkablePlaneBeingUsed.gameObject.SetActive(false);
        m_IsWalkable = false;
        m_Heuristic = 0;
    }

    
    protected static readonly Vector2[] _directions =
    {
        new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1)
    };

    public int GetDistance(Script_CombatNode other)
    {
        return (int)(Mathf.Abs(m_PositionInGrid.x - other.m_PositionInGrid.x) + Mathf.Abs(m_PositionInGrid.y - other.m_PositionInGrid.y));
    }
    
    //Distance is given using Manhattan Norm.


    public override List<Script_CombatNode> GetNeighbours(List<Script_CombatNode> cells)
    {
        if (neighbours == null)
        {
            neighbours = new List<Script_CombatNode>(4);
            foreach (var direction in _directions)
            {
                var neighbour = cells.Find(c => c.m_PositionInGrid == m_PositionInGrid + direction);
                if (neighbour == null) continue;

                neighbours.Add(neighbour);
            }
        }

        return neighbours;
    }

    //
    // public bool Equals(Script_CombatNode other)
    // {
    //     return (m_PositionInGrid.x == other.m_PositionInGrid.x && m_PositionInGrid.y == other.m_PositionInGrid.y);
    // }



}



