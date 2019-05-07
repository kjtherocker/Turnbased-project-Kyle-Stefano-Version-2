using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CombatNode : MonoBehaviour
{
    public int m_Heuristic;
    public bool m_IsGoal;
    public bool m_HeuristicCalculated;

    public bool m_IsSelector;

    public bool m_IsWalkable;

    public Vector2Int m_PositionInGrid;

    public Script_Creatures m_CreatureOnGridPoint;

    public GameObject m_WalkablePlane;
    public GameObject m_AttackingPlane;
    public GameObject m_SelectorPlane;

    public Material m_Selector;
    public Material m_Walkable;
    public Material m_Goal;
    public Renderer m_Renderer;

    public int m_Movement;
	// Use this for initialization
	void Start ()
    {
        m_Movement = 3;
        //m_HeuristicCalculated = false;
        m_WalkablePlane.gameObject.SetActive(false);
        m_SelectorPlane.gameObject.SetActive(false);
        m_AttackingPlane.gameObject.SetActive(false);

        m_IsSelector = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
     
        if (m_IsSelector == true)
        {
            m_SelectorPlane.gameObject.SetActive(true);
        }
        else if (m_IsSelector == false)
        {
            m_SelectorPlane.gameObject.SetActive(false);
        }
	}

    public void CreateWalkableArea()
    {
        if (m_Heuristic <= m_Movement)
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
