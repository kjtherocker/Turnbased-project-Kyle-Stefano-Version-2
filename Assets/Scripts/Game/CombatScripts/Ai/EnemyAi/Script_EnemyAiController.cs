using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Script_EnemyAiController : Script_AiController
{
    // Start is called before the first frame update

    public Script_AiController m_Target;
    public bool m_AiFinished;
    public bool m_EndMovement;
    public int m_EnemyRange;
    private Dictionary<Script_CombatNode, List<Script_CombatNode>> cacheRangePath;

    public override void Start()
    {
        base.Start();
        m_EnemyRange = 6;
    }

    public HashSet<Script_CombatNode> GetAvailableEnemysInRange(List<Script_CombatNode> cells, Script_CombatNode NodeHeuristicIsBasedOff)
    {
        cacheRangePath = new Dictionary<Script_CombatNode, List<Script_CombatNode>>();

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
        return new HashSet<Script_CombatNode>(cacheRangePath.Keys);
    }

    public  bool CheckIfNodeIsClearForRange(Script_CombatNode aNode)
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

        if (nodeIndex.m_CombatsNodeType == Script_CombatNode.CombatNodeTypes.Wall)
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

    protected virtual Dictionary<Script_CombatNode, Dictionary<Script_CombatNode, int>> GetGraphRangeEdges(List<Script_CombatNode> NodeList)
    {
        Dictionary<Script_CombatNode, Dictionary<Script_CombatNode, int>> ret = new Dictionary<Script_CombatNode, Dictionary<Script_CombatNode, int>>();

        foreach (Script_CombatNode Node in NodeList)
        {
            if (CheckIfNodeIsClearForRange(Node) == true || Node.Equals(Node_ObjectIsOn))
            {
                ret[Node] = new Dictionary<Script_CombatNode, int>();
                foreach (Script_CombatNode neighbour in Node.GetNeighbours(NodeList))
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

    public Dictionary<Script_CombatNode, List<Script_CombatNode>> cacheRangePaths(List<Script_CombatNode> cells, Script_CombatNode aNodeHeuristicIsBasedOn)
    {
        var edges = GetGraphRangeEdges(cells);
        var paths = _Pathfinder.findAllPaths(edges, aNodeHeuristicIsBasedOn);
        return paths;
    }

    public void EnemyMovement()
    {

        _pathsInRange = GetAvailableEnemysInRange(m_Grid.m_GridPathList, Node_ObjectIsOn);

        List<Script_Creatures> m_AllysInRange = new List<Script_Creatures>();
        foreach (Script_CombatNode node in _pathsInRange)
        {
            if (node.m_CreatureOnGridPoint != null && m_Creature != node.m_CreatureOnGridPoint)
            {
                m_AllysInRange.Add(node.m_CreatureOnGridPoint);
            }
        }

        if (m_AllysInRange.Count > 0)
        {

            Script_Creatures CharacterInRange = CharacterToFollowAndAttack(m_AllysInRange);

            Script_CombatNode NodeNeightboringAlly = 
                Script_GameManager.Instance.m_Grid.CheckNeighborsForLowestNumber(CharacterInRange.m_CreatureAi.m_Position);

            StartCoroutine(SetGoalPosition(NodeNeightboringAlly.m_PositionInGrid));

        }

    }

    public Script_Creatures CharacterToFollowAndAttack(List<Script_Creatures> aCharacterList)
    {

        for (int i = 0; i < aCharacterList.Count; i++)
        {
            for (int j = 0; j < aCharacterList.Count; j++)
            {
                if (aCharacterList[j].CurrentHealth < aCharacterList[j + 1].CurrentHealth)
                {
                    Script_Creatures tempA = aCharacterList[j];
                    Script_Creatures tempB = aCharacterList[j + 1];
                    swap(ref tempA, ref tempB);
                }
            }
        }

        return aCharacterList[0];
    }

    void swap(ref Script_Creatures xp, ref Script_Creatures yp)
    {
        Script_Creatures temp = xp;
        xp = yp;
        yp = temp;
    }

}
