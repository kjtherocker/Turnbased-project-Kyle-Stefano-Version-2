using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell : MonoBehaviour, IGraphNode
{
    public int GetDistance(IGraphNode other)
    {
        return GetDistance(other as CombatNode);
    }

    public abstract List<CombatNode> GetNeighbours(List<CombatNode> cells);


}
