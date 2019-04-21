using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CombatNode : MonoBehaviour
{
    public int m_Heuristic;
    public bool m_IsGoal;
    public bool m_HeuristicCalculated;

    public bool m_IsSelector;

    public Vector2Int m_PositionInGrid;

    public GameObject m_WalkablePlane;
    public GameObject m_SelectorPlane;

    public Material m_Selector;
    public Material m_Walkable;
    public Material m_Goal;
    public Renderer m_Renderer;

    public int m_Movement;
	// Use this for initialization
	void Start ()
    {
        //m_HeuristicCalculated = false;
        m_Heuristic = 430;
        m_Movement = 3;
        m_WalkablePlane.gameObject.SetActive(false);
        m_SelectorPlane.gameObject.SetActive(false);
        m_IsSelector = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(m_IsGoal == true)
        {
            m_Renderer.material = m_Goal;
        }
       
        if (m_Heuristic <= m_Movement)
        {
            m_WalkablePlane.gameObject.SetActive(true);
            m_WalkablePlane.GetComponent<Renderer>().material = m_Walkable;
        }

        if (m_IsSelector == true)
        {
            m_SelectorPlane.gameObject.SetActive(true);
        }
        else if (m_IsSelector == false)
        {
            m_SelectorPlane.gameObject.SetActive(false);
        }
	}
}
