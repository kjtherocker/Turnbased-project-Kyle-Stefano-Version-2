using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CombatNode : MonoBehaviour
{
    public int m_Heuristic;
    public bool m_IsGoal;
    public bool m_HeuristicCalculated;
    public Vector2Int m_PositionInGrid;
    public Material m_Selected;
    public Material m_Goal;
    public Renderer m_Renderer;
	// Use this for initialization
	void Start ()
    {
        //m_HeuristicCalculated = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(m_IsGoal == true)
        {
            m_Renderer.material = m_Goal;
        }
       
        if (m_Heuristic > 1)
        {
          //  m_Renderer.material = m_Selected;
        }
        
		
	}
}
