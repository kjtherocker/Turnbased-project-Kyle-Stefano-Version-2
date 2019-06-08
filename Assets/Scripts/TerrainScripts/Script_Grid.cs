using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Grid : MonoBehaviour
{

    public Vector2Int m_GridDimensions;

    public List<Script_CombatNode> m_GridPathToGoal;

    public GameObject m_PrefabNode;
    public Script_CombatNode[,] m_GridPathArray;
    public Script_CombatNode[] m_Test;
    public List<Script_CombatNode> m_OpenNodeList;
    public Material m_SelectedMaterial;

    public int PlayerX;
    public int PlayerY;

    public int m_Movement;

    public bool m_WalkingHasStarted;

    public bool m_GotPathNodes;


    // Use this for initialization
    void Start ()
    {
        Script_GameManager.Instance.m_Grid = this;

        PlayerX = 10;
        PlayerY = 10;


        m_WalkingHasStarted = false;

        //SetGoal(new Vector2Int(9,2));

        m_Movement = 4;
        m_GotPathNodes = false;
        
        //FindPointInGrid(new Vector2Int(1, 1));
        //CalculateHeuristic(new Vector2Int(2, 2));
    }

    public void StartGridCreation()
    {
        CreateGrid(m_GridDimensions);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_WalkingHasStarted == true)
        {
            if (IsAllNodesHeuristicCalculated())
            {
                SetWalkableArea();
                m_WalkingHasStarted = false;
            }
        }

    }

    private bool IsAllNodesHeuristicCalculated()
    {
        for (int x = 0; x < m_GridDimensions.x; x++)
        {
            for (int y = 0; y < m_GridDimensions.y; y++)
            {
                if (m_GridPathArray[x, y].m_HeuristicCalculated == false && 
                    m_GridPathArray[x,y].m_CombatsNodeType == Script_CombatNode.CombatNodeTypes.Normal)
                {
                    return false;
                }
            }
        }

        return true;
    }



    public void Convert1DArrayto2D(List<Script_CombatNode> aNodeGroup, Vector2Int grid)
    {

        m_GridDimensions = grid;
        m_GridPathArray = new Script_CombatNode[m_GridDimensions.x, m_GridDimensions.y];
        
        for (int i = 0; i < m_GridDimensions.x * m_GridDimensions.y; i++)
        {

           m_GridPathArray[aNodeGroup[i].m_PositionInGrid.x, aNodeGroup[i].m_PositionInGrid.y] = aNodeGroup[i];
            m_GridPathArray[aNodeGroup[i].m_PositionInGrid.x, aNodeGroup[i].m_PositionInGrid.y].m_Grid = this;




        }
    }

    public void CreateGrid(Vector2Int grid)
    {
        for (int x = 0; x < grid.x; x++)
        {
            for (int y = 0; y < grid.y; y++)
            {
                int RandomNumber = Random.Range(0, 2);
                m_GridPathArray[x,y] =  Instantiate<Script_CombatNode>(m_PrefabNode.GetComponent<Script_CombatNode>(), transform);

     

                    m_GridPathArray[x, y].transform.position = new Vector3(2 * x, 0.5f, 2 * y );
       

         
                m_GridPathArray[x,y].m_PositionInGrid = new Vector2Int(x, y);


            }
        }
        SetHeuristicToZero();
    }

    public Script_CombatNode GetNode(Vector2Int grid)
    {
        if (m_GridPathArray != null)
        {
            return m_GridPathArray[grid.x, grid.y];
        }
        else
        {
            return null;
        }
        
    }

    public void SetGoal(Vector2Int grid)
    {
        SetHeuristicToZero();
        m_GridPathToGoal.Clear();
        RemoveWalkableArea();

        m_GridPathArray[grid.x, grid.y].m_HeuristicCalculated = true;



        m_GridPathArray[grid.x, grid.y].m_Heuristic = 0;
        m_GridPathArray[grid.x, grid.y].m_IsGoal = true;



        m_GridPathArray[grid.x, grid.y].AddNeighboursToOpenListGoal(false);
    }

    public void SetHeuristicToZero()
    {
        for (int x = 0; x < m_GridDimensions.x; x++)
        {
            for (int y = 0; y < m_GridDimensions.y; y++)
            {
                m_GridPathArray[x, y].m_Heuristic = 0;
                m_GridPathArray[x, y].m_HeuristicCalculated = false;
                m_GridPathArray[x, y].m_IsGoal = false;
                m_GridPathArray[x, y].m_OpenListHasFinished = false;
                m_GridPathArray[x, y].m_NodeYouCameFrom = null;
            }
        }
    }

    public void SetWalkableArea()
    { 
        for (int x = 0; x < m_GridDimensions.x; x++)
        {
            for (int y = 0; y < m_GridDimensions.y; y++)
            {
                m_GridPathArray[x, y].CreateWalkableArea();
            }
        }
      
        Script_GameManager.Instance.m_BattleCamera.m_MovementHasBeenCalculated = true;
    }

    public void RemoveWalkableArea()
    {
        for (int x = 0; x < m_GridDimensions.x; x++)
        {
            for (int y = 0; y < m_GridDimensions.y; y++)
            {
                m_GridPathArray[x, y].RemoveWalkableArea();
            }
        }
    }

    public void SetWalkingHeuristic(Vector2Int grid)
    {
        SetHeuristicToZero();
        m_GridPathToGoal.Clear();
        

        m_GridPathArray[grid.x, grid.y].m_HeuristicCalculated = true;



        m_GridPathArray[grid.x, grid.y].m_Heuristic = 0;



        m_GridPathArray[grid.x, grid.y].AddNeighboursToOpenListGoal(true);
        m_WalkingHasStarted = true;
    }



  

    public void GetTheLowestH(Vector2Int grid, Script_AiController aiController)
    {
        
        float TempHeuristic = 100;
        Script_CombatNode TempNode = null;


            //Checking which heuristic is the lowest


            if (grid.y + 1 < m_GridDimensions.y)
            {
                if (m_GridPathArray[grid.x, grid.y + 1].m_CombatsNodeType == Script_CombatNode.CombatNodeTypes.Normal)
                {
                    if (m_GridPathArray[grid.x, grid.y + 1].m_Heuristic == 0 && m_GridPathArray[grid.x, grid.y + 1].m_HeuristicCalculated == false)
                    {
                    }
                    else
                    {
                        TempHeuristic = m_GridPathArray[grid.x, grid.y + 1].m_Heuristic;
                        TempNode = m_GridPathArray[grid.x, grid.y + 1];
                    }
                }
            }


            if (grid.y - 1 > -1)
            {
                if (m_GridPathArray[grid.x, grid.y - 1].m_CombatsNodeType == Script_CombatNode.CombatNodeTypes.Normal)
                {
                    if (m_GridPathArray[grid.x, grid.y - 1].m_Heuristic < TempHeuristic)
                    {
                        if (m_GridPathArray[grid.x, grid.y - 1].m_Heuristic == 0 && m_GridPathArray[grid.x, grid.y - 1].m_HeuristicCalculated == false)
                        {
                        
                        }
                        else
                        {
                            TempHeuristic = m_GridPathArray[grid.x, grid.y - 1].m_Heuristic;
                            TempNode = m_GridPathArray[grid.x, grid.y - 1];
                        }
                    }
                }
            }
            if (grid.x + 1 < m_GridDimensions.x)
            {
                 if (m_GridPathArray[grid.x + 1, grid.y].m_CombatsNodeType == Script_CombatNode.CombatNodeTypes.Normal)
                 {
                     if (m_GridPathArray[grid.x + 1, grid.y].m_Heuristic < TempHeuristic && m_GridPathArray[grid.x + 1, grid.y].m_Heuristic != -1)
                     {
                           if (m_GridPathArray[grid.x + 1, grid.y].m_Heuristic == 0 && m_GridPathArray[grid.x +1, grid.y ].m_HeuristicCalculated == false)
                           {
                           
                           }
                           else
                           {
                               TempHeuristic = m_GridPathArray[grid.x + 1, grid.y].m_Heuristic;
                               TempNode = m_GridPathArray[grid.x + 1, grid.y];
                           }
                     }
                 }
            }

            if (grid.x - 1 > -1)
            {
                 if (m_GridPathArray[grid.x - 1, grid.y ].m_CombatsNodeType == Script_CombatNode.CombatNodeTypes.Normal)
                 {
                     if (m_GridPathArray[grid.x - 1, grid.y].m_Heuristic < TempHeuristic && m_GridPathArray[grid.x - 1, grid.y ].m_Heuristic != -1)
                     {
                           if (m_GridPathArray[grid.x - 1, grid.y].m_Heuristic == 0 && m_GridPathArray[grid.x - 1, grid.y].m_HeuristicCalculated == false)
                           {
                           
                           }
                           else
                           {
                               TempHeuristic = m_GridPathArray[grid.x - 1, grid.y].m_Heuristic;
                               TempNode = m_GridPathArray[grid.x - 1, grid.y];
                           }
                     }
                 }
            }



        
            
        m_GridPathToGoal.Add(TempNode);
        m_GotPathNodes = true;
                
            
        
        
        if (m_GridPathToGoal[m_GridPathToGoal.Count - 1].m_IsGoal == true)
        {
            aiController.m_GridPath = m_GridPathToGoal;
            StartCoroutine(aiController.GetToGoal(aiController.m_GridPath));
            return;
        }
        else
        {
            if (m_Movement >= 1)
            {
                m_Movement--;
                m_GotPathNodes = false;
                GetTheLowestH(m_GridPathToGoal[m_GridPathToGoal.Count - 1].m_PositionInGrid, aiController);

            }
            else
            {
                aiController.m_GridPath = m_GridPathToGoal;
                StartCoroutine(aiController.GetToGoal(aiController.m_GridPath));
            }
        }
        
    }

    public List<Script_CombatNode> GetGridPathToGoal()
    {
        return m_GridPathToGoal;
    }

    public void SetAttackingTile(Vector2Int grid)
    {
        m_GridPathArray[grid.x, grid.y + 1].m_CurrentWalkablePlaneBeingUsed.gameObject.SetActive(true);
        m_GridPathArray[grid.x, grid.y - 1 ].m_CurrentWalkablePlaneBeingUsed.gameObject.SetActive(true);
        m_GridPathArray[grid.x + 1, grid.y].m_CurrentWalkablePlaneBeingUsed.gameObject.SetActive(true);
        m_GridPathArray[grid.x - 1, grid.y].m_CurrentWalkablePlaneBeingUsed.gameObject.SetActive(true);
    }

    public void SetSelectoringrid(Vector2Int grid)
    {
        m_GridPathArray[grid.x, grid.y].m_IsSelector = true;
    }

    public void SetAttackingTileInGrid(Vector2Int grid)
    {
        m_GridPathArray[grid.x, grid.y].m_AttackingPlane.SetActive(true);
    }

    public void DeselectAttackingTileingrid(Vector2Int grid)
    {
        m_GridPathArray[grid.x, grid.y].m_AttackingPlane.SetActive(false);
    }

    public void DeSelectSelectoringrid(Vector2Int grid)
    {
        m_GridPathArray[grid.x, grid.y].m_IsSelector = false;
    }



}
