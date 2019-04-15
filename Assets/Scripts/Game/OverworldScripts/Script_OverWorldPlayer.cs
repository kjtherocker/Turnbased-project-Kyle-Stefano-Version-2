using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_OverWorldPlayer : MonoBehaviour {

    // Use this for initialization

    public Script_Node Node_MovingTo;
    public Script_Node Node_PlayerIsOn;
    public Script_GameManager GameManager;
    public Script_EncounterManager m_EncounterManager;
    public GameObject OverworldModel;
    public GameObject Canvas_PartyMenu;
    public Script_PartyManager PartyManager;
    public Script_PartyMenu m_PartyMenu;

    public Material m_GridMaterial;

    public float Player_Speed = 40;
    public bool Player_Movment = false;
    private bool IsPartyMenuOn;
    private float Player_Speed_Delta;

    void Start ()
    {
        GameManager = Script_GameManager.Instance;
        m_EncounterManager = Script_GameManager.Instance.m_EncounterManager;
        PartyManager = Script_GameManager.Instance.m_PartyManager;
        OverworldModel = (GameObject)Resources.Load("Prefabs/Battle/PartyModels/Main_Character", typeof(GameObject));
        Instantiate<GameObject>(OverworldModel, gameObject.transform);
        IsPartyMenuOn = false;
        //CombineMeshes();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("escape"))
        {
            IsPartyMenuOn = !IsPartyMenuOn;
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Application.Quit();
        }
        if (IsPartyMenuOn == false)
        {
            Player_Speed_Delta = Player_Speed * Time.deltaTime;

            Canvas_PartyMenu.SetActive(false);

            if (transform.position == Node_MovingTo.transform.position)
            {
                Player_Movment = false;
                Node_PlayerIsOn = Node_MovingTo;
            }

            if (Node_PlayerIsOn.Enum_NodeType == Script_Node.NodeTypes.EncounterNode)
            {

                m_EncounterManager.SetEncounter(Script_EncounterManager.EncounterTypes.ForestEncounter);
                GameManager.SwitchToBattle();

                Node_PlayerIsOn.SetNodeType(Script_Node.NodeTypes.BasicNode);

            }

            if (Node_PlayerIsOn.Enum_NodeType == Script_Node.NodeTypes.EndNode)
            {

                m_EncounterManager.SetEncounter(Script_EncounterManager.EncounterTypes.BossForestEncounter);
                GameManager.SwitchToBattle();


                Node_PlayerIsOn.SetNodeType(Script_Node.NodeTypes.BasicNode);
            }

            if (Node_PlayerIsOn.Enum_NodeType == Script_Node.NodeTypes.DialogueNode)
            {
                Node_PlayerIsOn.StartDialogue();
                Node_PlayerIsOn.SetNodeType(Script_Node.NodeTypes.BasicNode);
            }

            if (Node_PlayerIsOn.Enum_NodeType == Script_Node.NodeTypes.ShopNode)
            {
                SceneManager.LoadScene(1);
            }


            if (Player_Movment == true)
            {
                OverworldMovement();
            }

            if (Player_Movment == false)
            {
                PlayerMovement();
            }
        }
        else
        {
            Canvas_PartyMenu.SetActive(true);
            if (Input.GetKeyDown("space"))
            {
                
            }
        }
    }

    public void OverworldMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, Node_MovingTo.transform.position, Player_Speed_Delta);
    }

    public void PlayerMovement()
    {
        if (Input.GetKey("up"))
        {
            
            if (Node_PlayerIsOn.GetComponent<Script_Node>().NodeUp != null)
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                Player_Movment = true;
                Node_MovingTo = Node_PlayerIsOn.GetComponent<Script_Node>().NodeUp;
            }
        }
        if (Input.GetKey("down"))
        {
            if (Node_PlayerIsOn.GetComponent<Script_Node>().NodeDown != null)
            {
                transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                Player_Movment = true;
                Node_MovingTo = Node_PlayerIsOn.GetComponent<Script_Node>().NodeDown;
            }
        }
        if (Input.GetKey("left"))
        {
            if (Node_PlayerIsOn.GetComponent<Script_Node>().NodeLeft != null)
            {
                transform.rotation = Quaternion.Euler(0.0f, 280.0f, 0.0f);

                Player_Movment = true;
                Node_MovingTo = Node_PlayerIsOn.GetComponent<Script_Node>().NodeLeft;
            }
        }
        if (Input.GetKey("right"))
        {
            if (Node_PlayerIsOn.GetComponent<Script_Node>().NodeRight != null)
            {
                transform.rotation = Quaternion.Euler(0.0f, 100.0f, 0.0f);

                Player_Movment = true;
                Node_MovingTo = Node_PlayerIsOn.GetComponent<Script_Node>().NodeRight;
            }
        }
    }

    public void CreateMesh(int size)
    {
        List<Vector3> verts = new List<Vector3>(); // Index used in tri list
        List<int> tris = new List<int>(); // Every 3 ints represents a triangle
        List<Vector2> uvs = new List<Vector2>(); // Vertex in 0-1 UV space
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                verts.Add(new Vector3(i, 0, j));
                uvs.Add(new Vector2((float)i / size, (float)j / size));
                if (i == 0 || j == 0) continue; // First bottom and left skipped
                tris.Add(size * i + j); //Top right
                tris.Add(size * i + (j - 1)); //Bottom right
                tris.Add(size * (i - 1) + (j - 1)); //Bottom left - First triangle
                tris.Add(size * (i - 1) + (j - 1)); //Bottom left 
                tris.Add(size * (i - 1) + j); //Top left
                tris.Add(size * i + j); //Top right - Second triangle
            }
        }
        Mesh mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.RecalculateNormals();

        GameObject grid = new GameObject("Grid");
        meshObjectList.Add(grid);
        grid.AddComponent<MeshFilter>();
        grid.AddComponent<MeshRenderer>();
        grid.GetComponent<MeshFilter>().mesh = mesh;
        // Load a material named "GridMat" from a folder named "Resources"

        grid.gameObject.transform.position = new Vector3(-78, -9.0f, 31.2f);
        grid.GetComponent<Renderer>().material = m_GridMaterial;
    }


    public List<GameObject> meshObjectList;

    public void CombineMeshes()
    {

        // combine meshes
        CombineInstance[] combine = new CombineInstance[meshObjectList.Count];
        int i = 0;
        while (i < meshObjectList.Count)
        {
            MeshFilter meshFilter = meshObjectList[i].gameObject.GetComponent<MeshFilter>();
            combine[i].mesh = meshFilter.sharedMesh;
            combine[i].transform = meshFilter.transform.localToWorldMatrix;
            i++;
        }

        Mesh combinedMesh = new Mesh();

        combinedMesh.CombineMeshes(combine);

        GameObject grid = new GameObject("Grid");
        grid.AddComponent<MeshFilter>();
        grid.AddComponent<MeshRenderer>();
        grid.GetComponent<MeshFilter>().mesh = combinedMesh;

        grid.GetComponent<Renderer>().material = m_GridMaterial;
    }

}

