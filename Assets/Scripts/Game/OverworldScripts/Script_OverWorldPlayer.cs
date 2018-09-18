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

    public float Player_Speed = 40;
    public bool Player_Movment = false;
    private bool IsPartyMenuOn;
    private float Player_Speed_Delta;

    void Start ()
    {
        OverworldModel = (GameObject)Resources.Load("Prefabs/Battle/PartyModels/Main_Character", typeof(GameObject));
        Instantiate<GameObject>(OverworldModel, gameObject.transform);
        IsPartyMenuOn = false;

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

}
