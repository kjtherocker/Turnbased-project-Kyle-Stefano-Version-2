using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAiController : AiController
{
    // Start is called before the first frame update

    public AiController m_Target;
    public bool m_AiFinished;
    public bool m_EndMovement;
    public int m_EnemyRange;
    private Dictionary<CombatNode, List<CombatNode>> cacheRangePath;

    public override void Start()
    {
        base.Start();
        m_EnemyRange = 6;
    }

    public HashSet<CombatNode> GetAvailableEnemysInRange(List<CombatNode> cells, CombatNode NodeHeuristicIsBasedOff)
    {
        cacheRangePath = new Dictionary<CombatNode, List<CombatNode>>();

        var paths = cacheRangePaths(cells, NodeHeuristicIsBasedOff);
        foreach (var key in paths.Keys)
        {
            var path = paths[key];

            var pathCost = path.Sum(c => 1);
            key.m_Heuristic = pathCost;
            if (pathCost <= m_EnemyRange)
            {
                cacheRangePath.Add(key, path);
            }
        }
        return new HashSet<CombatNode>(cacheRangePath.Keys);
    }

    public  bool CheckIfNodeIsClearForRange(CombatNode aNode)
    {
        // if the node is out of bounds, return -1 (an invalid tile index)

        if (aNode == null)
        {
            Debug.Log("YOU BROKE " + aNode.m_PositionInGrid.ToString());
        }

        CombatNode nodeIndex = aNode;

        // if the node is already closed, return -1 (an invalid tile index)
        if (nodeIndex.m_HeuristicCalculated == true)
        {
            return false;
        }
        // if the node can't be walked on, return -1 (an invalid tile index)

        if (nodeIndex.m_CombatsNodeType == CombatNode.CombatNodeTypes.Wall)
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

    protected virtual Dictionary<CombatNode, Dictionary<CombatNode, int>> GetGraphRangeEdges(List<CombatNode> NodeList)
    {
        Dictionary<CombatNode, Dictionary<CombatNode, int>> ret = new Dictionary<CombatNode, Dictionary<CombatNode, int>>();

        foreach (CombatNode Node in NodeList)
        {
            if (CheckIfNodeIsClearForRange(Node) == true || Node.Equals(Node_ObjectIsOn))
            {
                ret[Node] = new Dictionary<CombatNode, int>();
                foreach (CombatNode neighbour in Node.GetNeighbours(NodeList))
                {
                    if (CheckIfNodeIsClearForRange(neighbour) == true)
                    {
                        ret[Node][neighbour] = neighbour.m_MovementCost;
                    }
                }
            }
        }
        return ret;
    }

    public Dictionary<CombatNode, List<CombatNode>> cacheRangePaths(List<CombatNode> cells, CombatNode aNodeHeuristicIsBasedOn)
    {
        var edges = GetGraphRangeEdges(cells);
        var paths = _Pathfinder.findAllPaths(edges, aNodeHeuristicIsBasedOn);
        return paths;
    }

    public void EnemyMovement()
    {

        _pathsInRange = GetAvailableEnemysInRange(m_Grid.m_GridPathList, Node_ObjectIsOn);

        List<Creatures> m_AllysInRange = new List<Creatures>();
        foreach (CombatNode node in _pathsInRange)
        {
            if (node.m_CreatureOnGridPoint != null && m_Creature != node.m_CreatureOnGridPoint)
            {
                m_AllysInRange.Add(node.m_CreatureOnGridPoint);
            }
        }

        if (m_AllysInRange.Count > 0)
        {

            Creatures CharacterInRange = CharacterToFollowAndAttack(m_AllysInRange);

            CombatNode NodeNeightboringAlly = 
                GameManager.Instance.m_Grid.CheckNeighborsForLowestNumber(CharacterInRange.m_CreatureAi.m_Position);

            StartCoroutine(SetGoalPosition(NodeNeightboringAlly.m_PositionInGrid));

        }

    }

    public Creatures CharacterToFollowAndAttack(List<Creatures> aCharacterList)
    {

        for (int i = 0; i < aCharacterList.Count; i++)
        {
            for (int j = 0; j < aCharacterList.Count; j++)
            {
                if (aCharacterList[j].CurrentHealth < aCharacterList[j + 1].CurrentHealth)
                {
                    Creatures tempA = aCharacterList[j];
                    Creatures tempB = aCharacterList[j + 1];
                    swap(ref tempA, ref tempB);
                }
            }
        }

        return aCharacterList[0];
    }

    void swap(ref Creatures xp, ref Creatures yp)
    {
        Creatures temp = xp;
        xp = yp;
        yp = temp;
    }

}
