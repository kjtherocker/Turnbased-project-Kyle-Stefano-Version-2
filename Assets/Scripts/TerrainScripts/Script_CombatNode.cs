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
    public int m_Heuristic;
    public bool m_IsGoal;
    public bool m_HeuristicCalculated;

    public bool m_IsSelector;

    public bool m_IsWalkable;

    public Vector2Int m_PositionInGrid;

    public Script_Creatures m_CreatureOnGridPoint;

    public GameObject m_WalkablePlane;
    public GameObject m_AttackingPlane;

    public GameObject m_Cube;

    public Material m_Selector;
    public Material m_Walkable;
    public Material m_Goal;
    public Renderer m_Renderer;


    public CombatNodeTypes m_CombatsNodeType;

    public int m_Movement;
	// Use this for initialization
	void Start ()
    {
        m_Movement = 3;
        //m_HeuristicCalculated = false;
        m_WalkablePlane.gameObject.SetActive(false);
        m_AttackingPlane.gameObject.SetActive(false);
        m_Cube.gameObject.SetActive(true);
        m_IsSelector = false;
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
       
	}

    public void CreateWalkableArea()
    {
        if (m_Heuristic <= m_Movement && m_Heuristic != 0)
        {
            m_WalkablePlane.gameObject.SetActive(true);
            m_WalkablePlane.GetComponent<Renderer>().material = m_Walkable;
            m_IsWalkable = true;
        }
    }

    public void RemoveWalkableArea()
    {
        
        m_Heuristic = 0;
        m_WalkablePlane.gameObject.SetActive(false);
        //m_WalkablePlane.GetComponent<Renderer>().material = m_Walkable;
        m_IsWalkable = false;
        
    }
}
