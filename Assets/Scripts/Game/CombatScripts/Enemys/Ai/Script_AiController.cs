using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_AiController : MonoBehaviour
{
    public Script_Grid m_Grid;
    public List<Script_CombatNode> m_GridPath;
    Vector2Int m_Goal;
    public Script_Node Node_MovingTo;
    public Script_Node Node_ObjectIsOn;

    // Use this for initialization
    void Start ()
    {
        m_Goal = new Vector2Int(9, 2);

        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("a"))
        {
            m_Grid.SetGoal(m_Goal);
            m_Grid.GetTheLowestH(new Vector2Int(1, 9), this);
        }

    
    }

    public IEnumerator GetToGoal(List<Script_CombatNode> aListOfNodes)
    {
        for(int i = 0; i < aListOfNodes.Count; i++)
        {
            gameObject.transform.position = aListOfNodes[i].gameObject.transform.position;
            yield return new WaitForSeconds(1);
        }
    }
}
