using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Grid : MonoBehaviour
{

    Vector2Int m_GridDimensions;

    public List<GameObject> m_GridNodes;

    public GameObject m_PrefabNode;

    public Material m_SelectedMaterial;

    public int PlayerX;
    public int PlayerY;

	// Use this for initialization
	void Start ()
    {

        m_GridDimensions = new Vector2Int(20, 20);
        

        PlayerX = 10;
        PlayerY = 10;

        CreateGrid(m_GridDimensions.x, m_GridDimensions.y);


        //FindPointInGrid(PlayerX, PlayerY);

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

        FindPointInGrid(PlayerX, PlayerY);

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

    public void CreateGrid(int gridX, int gridY)
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                m_GridNodes.Add(Instantiate<GameObject>(m_PrefabNode, transform));
                m_GridNodes[m_GridNodes.Count - 1].transform.position = new Vector3(1 * x, 0, 1 * y);
             
            }
        }
    }

    public void FindPointInGrid(int gridx, int gridy)
    {
        int TrueY = gridy * m_GridDimensions.y;
        m_GridNodes[gridx + TrueY].gameObject.GetComponent<Renderer>().material = m_SelectedMaterial;
    }
}
