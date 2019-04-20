using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Grid : MonoBehaviour
{

    Vector2Int m_GridDimensions;

    public List<Script_CombatNode> m_GridNodes;
    public List<Script_CombatNode> m_GridPath;
    public List<Script_CombatNode> m_GridPathToGoal;

    public GameObject m_PrefabNode;
    public Script_CombatNode[,] m_GridPathArray;
    public Material m_SelectedMaterial;

    public int PlayerX;
    public int PlayerY;

	// Use this for initialization
	void Start ()
    {

        m_GridDimensions = new Vector2Int(10, 10);

        m_GridPathArray = new Script_CombatNode[m_GridDimensions.x, m_GridDimensions.y];

        PlayerX = 10;
        PlayerY = 10;

        CreateGrid(m_GridDimensions);
        

        SetGoal(new Vector2Int(0,0));

        //FindPointInGrid(new Vector2Int(1, 1));
        //CalculateHeuristic(new Vector2Int(2, 2));
    }
	
	// Update is called once per frame
	void Update ()
    {
        Mathf.Clamp(PlayerX, 0, m_GridDimensions.x);
        Mathf.Clamp(PlayerY, 0, m_GridDimensions.y);

        if (PlayerX > m_GridDimensions.x)
        {
            PlayerX = m_GridDimensions.x;
        }
        else if (PlayerX < 0)
        {
            PlayerX = 0;
        }

        if (PlayerY > m_GridDimensions.y)
        {
            PlayerY = m_GridDimensions.y;
        }
        else if (PlayerY < 0)
        {
            PlayerY = 0;
        }

        //FindPointInGrid(PlayerX, PlayerY);

        if (Input.GetKeyDown("up"))
        {
            PlayerY++;
        }
        if (Input.GetKeyDown("down"))
        {
            PlayerY--;
        }
        if (Input.GetKeyDown("left"))
        {
            PlayerX++;
        }
        if (Input.GetKeyDown("right"))
        {
            PlayerX--;
        }


    }

    public void CreateGrid(Vector2Int grid)
    {
        for (int x = 0; x < grid.x; x++)
        {
            for (int y = 0; y < grid.y; y++)
            {
                m_GridPathArray[x,y] =  Instantiate<Script_CombatNode>(m_PrefabNode.GetComponent<Script_CombatNode>(), transform);
                m_GridPathArray[x,y].transform.position = new Vector3(1 * x, 0, 1 * y);
                m_GridPathArray[x,y].m_PositionInGrid = new Vector2Int(x, y);


            }
        }
    }

    public void FindPointInGrid(Vector2Int grid)
    {
        m_GridPathArray[grid.x, grid.y].gameObject.GetComponent<Renderer>().material = m_SelectedMaterial;
    }

    public void SetGoal(Vector2Int grid)
    {
        for (int i = 0; i < m_GridDimensions.x + m_GridDimensions.y; i++)
        {
            m_GridPathArray[grid.x, grid.y].m_Heuristic = 0;
            m_GridPathArray[grid.x, grid.y].m_HeuristicCalculated = false;
        }

        m_GridPathArray[grid.x, grid.y].m_IsGoal = true;
        m_GridPathArray[grid.x, grid.y].m_HeuristicCalculated = true;

        CalculateUpHeuristic(new Vector2Int(grid.x, grid.y));
        CalculateDownHeuristic(new Vector2Int(grid.x, grid.y));
        CalculateLeftHeuristic(new Vector2Int(grid.x, grid.y));
        CalculateRightHeuristic(new Vector2Int(grid.x, grid.y));

    }

    public void CalculateUpHeuristic(Vector2Int grid)
    {
        if (grid.x + 1 < m_GridDimensions.x)
        {
            if (m_GridPathArray[grid.x + 1, grid.y].m_HeuristicCalculated == false)
            {
                // Calculating the Heuristic based on the last grid
                m_GridPathArray[grid.x + 1, grid.y].m_Heuristic =
                    m_GridPathArray[grid.x, grid.y].m_Heuristic + 1;


                m_GridPathArray[grid.x + 1, grid.y].gameObject.GetComponent<Renderer>().material = m_SelectedMaterial;


                //Setting this node to Calculated
                m_GridPathArray[grid.x + 1, grid.y].m_HeuristicCalculated = true;


                //Now Calculate heuristic of those around you
                CalculateUpHeuristic(m_GridPathArray[grid.x + 1, grid.y].m_PositionInGrid);
                CalculateLeftHeuristic(m_GridPathArray[grid.x + 1, grid.y].m_PositionInGrid);
                CalculateRightHeuristic(m_GridPathArray[grid.x + 1, grid.y].m_PositionInGrid);

            }
        }
    }

    public void CalculateDownHeuristic(Vector2Int grid)
    {
        if (grid.x - 1 > -1)
        {
           if( m_GridPathArray[grid.x - 1, grid.y].m_HeuristicCalculated == false)
            {
                // Calculating the Heuristic based on the last grid
                m_GridPathArray[grid.x - 1, grid.y].m_Heuristic =
                    m_GridPathArray[grid.x, grid.y].m_Heuristic + 1;


                m_GridPathArray[grid.x - 1, grid.y].gameObject.GetComponent<Renderer>().material = m_SelectedMaterial;


                //Setting this node to Calculated
                m_GridPathArray[grid.x - 1, grid.y].m_HeuristicCalculated = true;


                //Now Calculate heuristic of those around you
                CalculateDownHeuristic(m_GridPathArray[grid.x - 1, grid.y].m_PositionInGrid);
                CalculateLeftHeuristic(m_GridPathArray[grid.x - 1, grid.y].m_PositionInGrid);
                CalculateRightHeuristic(m_GridPathArray[grid.x - 1, grid.y].m_PositionInGrid);

            }
        }
    }

    public void CalculateLeftHeuristic(Vector2Int grid)
    {
        if (grid.y + 1 < m_GridDimensions.y )
        {
           if( m_GridPathArray[grid.x, grid.y + 1].m_HeuristicCalculated == false)
            {
                // Calculating the Heuristic based on the last grid
                m_GridPathArray[grid.x, grid.y + 1].m_Heuristic =
                    m_GridPathArray[grid.x, grid.y].m_Heuristic + 1;


                m_GridPathArray[grid.x, grid.y + 1].gameObject.GetComponent<Renderer>().material = m_SelectedMaterial;


                //Setting this node to Calculated
                m_GridPathArray[grid.x, grid.y + 1].m_HeuristicCalculated = true;



                //Now Calculate heuristic of those around you
                CalculateLeftHeuristic(m_GridPathArray[grid.x, grid.y + 1].m_PositionInGrid);

            }
        }
    }

    public void CalculateRightHeuristic(Vector2Int grid)
    {
        if (grid.y - 1 > -1 )
        {
            if (m_GridPathArray[grid.x, grid.y - 1].m_HeuristicCalculated == false)
            {
                // Calculating the Heuristic based on the last grid
                m_GridPathArray[grid.x, grid.y - 1].m_Heuristic =
                    m_GridPathArray[grid.x, grid.y].m_Heuristic + 1;


                m_GridPathArray[grid.x, grid.y - 1].gameObject.GetComponent<Renderer>().material = m_SelectedMaterial;


                //Setting this node to Calculated
                m_GridPathArray[grid.x, grid.y - 1].m_HeuristicCalculated = true;



                //Now Calculate heuristic of those around you
                CalculateRightHeuristic(m_GridPathArray[grid.x, grid.y - 1].m_PositionInGrid);

            }
        }
    }

    public void GetTheLowestH(Vector2Int grid)
    {
        int TempHeuristic;
        


        TempHeuristic = m_GridPathArray[grid.x, grid.y + 1].m_Heuristic;
        if (m_GridPathArray[grid.x, grid.y - 1].m_Heuristic < TempHeuristic)
        {
            TempHeuristic = m_GridPathArray[grid.x, grid.y - 1].m_Heuristic;
        }
        if (m_GridPathArray[grid.x + 1, grid.y].m_Heuristic < TempHeuristic)
        {
            TempHeuristic = m_GridPathArray[grid.x + 1, grid.y].m_Heuristic;
        }
        if (m_GridPathArray[grid.x - 1, grid.y].m_Heuristic < TempHeuristic)
        {
            TempHeuristic = m_GridPathArray[grid.x - 1, grid.y].m_Heuristic;
        }

        if (m_GridPathArray[grid.x, grid.y + 1].m_Heuristic == TempHeuristic)
        {
            m_GridPathToGoal.Add(m_GridPathArray[grid.x, grid.y + 1]);
        }
        if (m_GridPathArray[grid.x, grid.y - 1].m_Heuristic == TempHeuristic)
        {
            m_GridPathToGoal.Add(m_GridPathArray[grid.x, grid.y + 1]);
        }
        if (m_GridPathArray[grid.x + 1, grid.y].m_Heuristic == TempHeuristic)
        {
            m_GridPathToGoal.Add(m_GridPathArray[grid.x, grid.y + 1]);
        }
        if (m_GridPathArray[grid.x - 1, grid.y].m_Heuristic == TempHeuristic)
        {
            m_GridPathToGoal.Add(m_GridPathArray[grid.x, grid.y + 1]);
        }

        if (m_GridPathToGoal[m_GridPathToGoal.Count].m_IsGoal == true)
        {

        }
        else
        {
            GetTheLowestH(m_GridPathToGoal[m_GridPathToGoal.Count].m_PositionInGrid);
        }

    }

    public List<Script_CombatNode> GetGridPathToGoal()
    {
        return m_GridPathToGoal;
    }


    public Script_CombatNode GetPointInGrid(Vector2Int grid)
    {
        int TrueY = grid.y * m_GridDimensions.y;
        return m_GridNodes[grid.x + TrueY];
    }
}
