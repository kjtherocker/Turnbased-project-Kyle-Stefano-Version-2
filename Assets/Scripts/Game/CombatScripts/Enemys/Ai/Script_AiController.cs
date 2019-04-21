using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_AiController : MonoBehaviour
{
    public Script_Grid m_Grid;
    public List<Script_CombatNode> m_GridPath;
    Vector2Int m_Goal;
    Vector2Int m_Position;
    public Script_Node Node_MovingTo;
    public Script_Node Node_ObjectIsOn;
    public int m_Movement;
    public int m_Jump;

    // Use this for initialization
    void Start ()
    {
        m_Goal = new Vector2Int(9, 2);
        m_Position = new Vector2Int(4, 4);
        m_Movement = 4;
        m_Jump = 2;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("a"))
        {
            
            m_Grid.m_Movement = m_Movement;
            m_Grid.SetWalkingHeuristic(m_Position);
           // m_Grid.GetTheLowestH(m_Position, this);
        }

    
    }

    public IEnumerator GetToGoal(List<Script_CombatNode> aListOfNodes)
    {
        for(int i = 0; i < aListOfNodes.Count; i++)
        {
            gameObject.transform.position = aListOfNodes[i].gameObject.transform.position;
            yield return new WaitForSeconds(1);
        }
        m_Position = aListOfNodes[aListOfNodes.Count - 1].m_PositionInGrid;
    }
}
