using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_AiFlying : Script_AiController
{
    public override bool CheckIfNodeIsClearAndReturnNodeIndex(Script_CombatNode aNode)
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
        if (nodeIndex.m_CombatsNodeType == Script_CombatNode.CombatNodeTypes.Covered)
        {
            return false;
        }

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


}
