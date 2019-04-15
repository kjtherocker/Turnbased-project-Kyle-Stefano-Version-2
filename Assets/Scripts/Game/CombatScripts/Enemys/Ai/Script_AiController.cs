using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_AiController : MonoBehaviour
{
    Script_Grid m_Grid;

    Vector2Int m_Goal;

	// Use this for initialization
	void Start ()
    {
        m_Goal = new Vector2Int(2, 2);

        m_Grid.FindPointInGrid(m_Goal.x, m_Goal.y);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


}
