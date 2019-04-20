using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_AiController : MonoBehaviour
{
    Script_Grid m_Grid;

    Vector2Int m_Goal;
    public Script_Node Node_MovingTo;
    public Script_Node Node_ObjectIsOn;

    // Use this for initialization
    void Start ()
    {
        m_Goal = new Vector2Int(2, 2);

        m_Grid.FindPointInGrid(m_Goal);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


}
