using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class Pathfinder : IPathfinding
{
    public Dictionary<CombatNode, List<CombatNode>> findAllPaths(Dictionary<CombatNode, Dictionary<CombatNode, int>> edges, CombatNode originNode)
    {
        IPriorityQueue<CombatNode> frontier = new HeapPriorityQueue<CombatNode>();
        frontier.Enqueue(originNode, 0);

        Dictionary<CombatNode, CombatNode> cameFrom = new Dictionary<CombatNode, CombatNode>();
        cameFrom.Add(originNode, default(CombatNode));
        Dictionary<CombatNode, int> costSoFar = new Dictionary<CombatNode, int>();
        costSoFar.Add(originNode, 0);

        while (frontier.Count != 0)
        {
            var current = frontier.Dequeue();
            List<CombatNode>neighbours = GetNeigbours(edges, current);
            foreach (CombatNode neighbour in neighbours)
            {
                int newCost = costSoFar[current] + edges[current][neighbour];
                if (!costSoFar.ContainsKey(neighbour) || newCost < costSoFar[neighbour])
                {
                    costSoFar[neighbour] = newCost;
                    cameFrom[neighbour] = current;
                    frontier.Enqueue(neighbour, newCost);
                }
            }
        }

        Dictionary<CombatNode, List<CombatNode>> paths = new Dictionary<CombatNode, List<CombatNode>>();
        foreach (CombatNode destination in cameFrom.Keys)
        {
            List<CombatNode> path = new List<CombatNode>();
            var current = destination;
            while (!current.Equals(originNode))
            {
                path.Add(current);
                current = cameFrom[current];
            }
            paths.Add(destination, path);
        }
        return paths;
    }

    public override List<T> FindPath<T>(Dictionary<T, Dictionary<T, int>> edges, T originNode, T destinationNode)
    {
        IPriorityQueue<T> frontier = new HeapPriorityQueue<T>();
        frontier.Enqueue(originNode, 0);

        Dictionary<T, T> cameFrom = new Dictionary<T, T>();
        cameFrom.Add(originNode, default(T));
        Dictionary<T, int> costSoFar = new Dictionary<T, int>();
        costSoFar.Add(originNode, 0);

        while (frontier.Count != 0)
        {
            var current = frontier.Dequeue();
            if (current.Equals(destinationNode)) break;

            var neighbours = GetNeigbours(edges, current);
            foreach (var neighbour in neighbours)
            {
                var newCost = costSoFar[current] + edges[current][neighbour];
                if (!costSoFar.ContainsKey(neighbour) || newCost < costSoFar[neighbour])
                {
                    costSoFar[neighbour] = newCost;
                    cameFrom[neighbour] = current;
                    var priority = newCost + Heuristic(destinationNode, neighbour);
                    frontier.Enqueue(neighbour, priority);
                }
            }
        }

        List<T> path = new List<T>();
        if (!cameFrom.ContainsKey(destinationNode))
            return path;

        path.Add(destinationNode);
        var temp = destinationNode;

        while (!cameFrom[temp].Equals(originNode))
        {
            var currentPathElement = cameFrom[temp];
            path.Add(currentPathElement);

            temp = currentPathElement;
        }

        return path;
    }
    private int Heuristic<T>(T a, T b) where T : IGraphNode
    {
        return a.GetDistance(b);
    }
}




public abstract class IPathfinding
{
    /// <summary>
    /// Method finds path between two nodes in a graph.
    /// </summary>
    /// <param name="edges">
    /// Graph edges represented as nested dictionaries. Outer dictionary contains all nodes in the graph, inner dictionary contains 
    /// its neighbouring nodes with edge weight.
    /// </param>
    /// <returns>
    /// If a path exist, method returns list of nodes that the path consists of. Otherwise, empty list is returned.
    /// </returns>
    public abstract List<T> FindPath<T>(Dictionary<T, Dictionary<T, int>> edges, T originNode, T destinationNode) where T : IGraphNode;

    protected List<T> GetNeigbours<T>(Dictionary<T, Dictionary<T, int>> edges, T node) where T : IGraphNode
    {
        if (!edges.ContainsKey(node))
        {
            return new List<T>();
        }
        return edges[node].Keys.ToList();
    }
}


/// <summary>
/// Represents a prioritized queue.
/// </summary>
public interface IPriorityQueue<T>
{
    /// <summary>
    /// Number of items in the queue.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Method adds item to the queue.
    /// </summary>
    void Enqueue(T item, int priority);
    /// <summary>
    /// Method returns item with the LOWEST priority value.
    /// </summary>
    T Dequeue();
}

/// <summary>
/// Represents a node in a priority queue.
/// </summary>
class PriorityQueueNode<T> : IComparable
{
    public T Item { get; private set; }
    public float Priority { get; private set; }

    public PriorityQueueNode(T item, float priority)
    {
        Item = item;
        Priority = priority;
    }

    public int CompareTo(object obj)
    {
        return Priority.CompareTo((obj as PriorityQueueNode<T>).Priority);
    }
}

class HeapPriorityQueue<T> : IPriorityQueue<T>
{
    private List<PriorityQueueNode<T>> _queue;

    public HeapPriorityQueue()
    {
        _queue = new List<PriorityQueueNode<T>>();
    }

    public int Count
    {
        get { return _queue.Count; }
    }

    public void Enqueue(T item, int priority)
    {
        _queue.Add(new PriorityQueueNode<T>(item, priority));
        int ci = _queue.Count - 1;
        while (ci > 0)
        {
            int pi = (ci - 1) / 2;
            if (_queue[ci].CompareTo(_queue[pi]) >= 0)
                break;
            var tmp = _queue[ci];
            _queue[ci] = _queue[pi];
            _queue[pi] = tmp;
            ci = pi;
        }
    }
    public T Dequeue()
    {
        int li = _queue.Count - 1;
        var frontItem = _queue[0];
        _queue[0] = _queue[li];
        _queue.RemoveAt(li);

        --li;
        int pi = 0;
        while (true)
        {
            int ci = pi * 2 + 1;
            if (ci > li) break;
            int rc = ci + 1;
            if (rc <= li && _queue[rc].CompareTo(_queue[ci]) < 0)
                ci = rc;
            if (_queue[pi].CompareTo(_queue[ci]) <= 0) break;
            var tmp = _queue[pi]; _queue[pi] = _queue[ci]; _queue[ci] = tmp;
            pi = ci;
        }
        return frontItem.Item;
    }
}
