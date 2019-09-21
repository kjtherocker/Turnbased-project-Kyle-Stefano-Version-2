using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell : MonoBehaviour, IGraphNode
{
    public int GetDistance(IGraphNode other)
    {
        return GetDistance(other as Script_CombatNode);
    }

    public abstract List<Script_CombatNode> GetNeighbours(List<Script_CombatNode> cells);


}
