using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_Player : MonoBehaviour {

    // Use this for initialization

    public Script_Node Node_MovingTo;
    public Script_Node Node_PlayerIsOn;
    public Script_GameManager GameManager;
    public GameObject OverworldModel;

    public Script_PartyManager PartyManager;

    public float Player_Speed = 40;
    public bool Player_Movment = false;
    public float Player_Speed_Delta;

    void Start ()
    {
        OverworldModel = PartyManager.PartyMemberSlot1.Model;
        Instantiate<GameObject>(OverworldModel, gameObject.transform);
       
    }
	
	// Update is called once per frame
	void Update ()
    {
        Player_Speed_Delta = Player_Speed * Time.deltaTime;
     //   OverworldModel.transform.parent = gameObject.transform;


        if (transform.position == Node_MovingTo.transform.position)
        {
            Player_Movment = false;
            Node_PlayerIsOn = Node_MovingTo;
        }

        if (Node_PlayerIsOn.Enum_NodeType == Script_Node.NodeTypes.EncounterNode)
        {
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
                transform.rotation = Quaternion.Euler(0.0f, 100.0f, 0.0f);
                Player_Movment = true;
                Node_MovingTo = Node_PlayerIsOn.GetComponent<Script_Node>().NodeUp;
            }
        }
        if (Input.GetKey("down"))
        {
            if (Node_PlayerIsOn.GetComponent<Script_Node>().NodeDown != null)
            {
                transform.rotation = Quaternion.Euler(0.0f, 280.0f, 0.0f);
                Player_Movment = true;
                Node_MovingTo = Node_PlayerIsOn.GetComponent<Script_Node>().NodeDown;
            }
        }
        if (Input.GetKey("left"))
        {
            if (Node_PlayerIsOn.GetComponent<Script_Node>().NodeLeft != null)
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

                Player_Movment = true;
                Node_MovingTo = Node_PlayerIsOn.GetComponent<Script_Node>().NodeLeft;
            }
        }
        if (Input.GetKey("right"))
        {
            if (Node_PlayerIsOn.GetComponent<Script_Node>().NodeRight != null)
            {
                transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);

                Player_Movment = true;
                Node_MovingTo = Node_PlayerIsOn.GetComponent<Script_Node>().NodeRight;
            }
        }
    }

}
