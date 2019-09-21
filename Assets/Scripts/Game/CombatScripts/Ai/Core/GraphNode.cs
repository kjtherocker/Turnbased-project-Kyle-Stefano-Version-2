using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGraphNode
{
    /// <summary>
    /// Method returns distance to a IGraphNode that is given as parameter. 
    /// </summary>
    int GetDistance(IGraphNode other);
}
