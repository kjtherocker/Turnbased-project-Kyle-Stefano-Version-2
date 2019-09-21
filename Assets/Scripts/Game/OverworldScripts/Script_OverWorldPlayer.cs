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

    //TODO make dialogue a Dialogue manager thing
    public DialogueManager m_DialogueManager;

    public Material m_GridMaterial;

    public float Player_Speed = 5;
    public bool Player_Movment = false;
    private bool IsPartyMenuOn;
    private float Player_Speed_Delta;


    public GameObject m_OverworldPlayerModel;

    void Start ()
    {
        GameManager = Script_GameManager.Instance;
        m_EncounterManager = Script_GameManager.Instance.m_EncounterManager;
        PartyManager = Script_GameManager.Instance.m_PartyManager;
        m_OverworldPlayerModel = Instantiate<GameObject>(OverworldModel, gameObject.transform);
        gameObject.transform.position = Node_PlayerIsOn.transform.position;
        IsPartyMenuOn = false;
        //CombineMeshes();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Application.Quit();
        }


        switch (Node_PlayerIsOn.Enum_NodeType)
        {
            case Script_Node.NodeTypes.BasicNode:

                break;

            case Script_Node.NodeTypes.EncounterNode:

                if (Node_PlayerIsOn.m_GridFormation != null)
                {
                    Script_GameManager.Instance.CombatManager.m_GridFormation = Node_PlayerIsOn.m_GridFormation;
                }
                m_EncounterManager.SetEncounter(Script_EncounterManager.EncounterTypes.ForestEncounter);
                GameManager.SwitchToBattle();

                Node_PlayerIsOn.SetNodeType(Script_Node.NodeTypes.BasicNode);


                break;

            case Script_Node.NodeTypes.EndNode:

                m_EncounterManager.SetEncounter(Script_EncounterManager.EncounterTypes.BossForestEncounter);
                GameManager.SwitchToBattle();

                Node_PlayerIsOn.SetNodeType(Script_Node.NodeTypes.BasicNode);

                break;

            case Script_Node.NodeTypes.DialogueNode:
                m_DialogueManager.gameObject.SetActive(true);
                Node_PlayerIsOn.StartDialogue();
                Node_PlayerIsOn.SetNodeType(Script_Node.NodeTypes.BasicNode);

                break;

            case Script_Node.NodeTypes.ShopNode:

                SceneManager.LoadScene(1);
                break;
        }

        if (Player_Movment == false)
        {
            PlayerMovement();
        }

        if (Node_MovingTo != Node_PlayerIsOn)
        {
            StartCoroutine(OverworldMovement());
        }
        
    }

    public IEnumerator OverworldMovement()
    {
        Player_Speed_Delta = Player_Speed * Time.deltaTime;
        Player_Movment = true;
        

        m_OverworldPlayerModel.GetComponent<Animator>().SetBool("b_IsWalking", true);

        transform.position = Vector3.MoveTowards(transform.position, Node_MovingTo.transform.position, Player_Speed_Delta);

        if (transform.position == Node_MovingTo.transform.position)
        {
            Player_Movment = false;
            Node_PlayerIsOn = Node_MovingTo;
            m_OverworldPlayerModel.GetComponent<Animator>().SetBool("b_IsWalking", false);
            yield break;
        }
    }

    public void PlayerMovement()
    {
        if (Input.GetKey("up"))
        {
            PlayerUp();
        }
        

        if (Input.GetKey("down"))
        {
            PlayerDown();
        }
       

        if (Input.GetKey("left"))
        {
            PlayerLeft();
        }
        

        if (Input.GetKey("right"))
        {
            PlayerRight();
        }

        if (Constants.Constants.m_XboxController == true)
        {

            Script_GameManager.Instance.m_InputManager.SetXboxAxis
            (PlayerUp, "Xbox_DPadY", true, ref Script_GameManager.Instance.m_InputManager.m_DPadY);

            Script_GameManager.Instance.m_InputManager.SetXboxAxis
            (PlayerRight, "Xbox_DPadX", true, ref Script_GameManager.Instance.m_InputManager.m_DPadX);

            Script_GameManager.Instance.m_InputManager.SetXboxAxis
           (PlayerDown, "Xbox_DPadY", false, ref Script_GameManager.Instance.m_InputManager.m_DPadY);

            Script_GameManager.Instance.m_InputManager.SetXboxAxis
            (PlayerLeft, "Xbox_DPadX", false, ref Script_GameManager.Instance.m_InputManager.m_DPadX);

        }
        if (Constants.Constants.m_PlaystationController == true)
        {
            Script_GameManager.Instance.m_InputManager.SetPlaystationAxis
            (PlayerUp, "Ps4_DPadY", true, ref Script_GameManager.Instance.m_InputManager.m_DPadY);
        
            Script_GameManager.Instance.m_InputManager.SetPlaystationAxis
            (PlayerRight, "Ps4_DPadX", true, ref Script_GameManager.Instance.m_InputManager.m_DPadX);
        
            Script_GameManager.Instance.m_InputManager.SetPlaystationAxis
           (PlayerDown, "Ps4_DPadY", false, ref Script_GameManager.Instance.m_InputManager.m_DPadY);
        
            Script_GameManager.Instance.m_InputManager.SetPlaystationAxis
            (PlayerLeft, "Ps4_DPadX", false, ref Script_GameManager.Instance.m_InputManager.m_DPadX);
        }

    }

    public void PlayerUp()
    {
        if (Node_PlayerIsOn.GetComponent<Script_Node>().NodeUp != null)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            Player_Movment = true;
            Node_MovingTo = Node_PlayerIsOn.GetComponent<Script_Node>().NodeUp;
        }
    }

    public void PlayerDown()
    {
        if (Node_PlayerIsOn.GetComponent<Script_Node>().NodeDown != null)
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            Player_Movment = true;
            Node_MovingTo = Node_PlayerIsOn.GetComponent<Script_Node>().NodeDown;
        }
    }

    public void PlayerLeft()
    {
        if (Node_PlayerIsOn.GetComponent<Script_Node>().NodeLeft != null)
        {
            transform.rotation = Quaternion.Euler(0.0f, 280.0f, 0.0f);

            Player_Movment = true;
            Node_MovingTo = Node_PlayerIsOn.GetComponent<Script_Node>().NodeLeft;
        }
    }

    public void PlayerRight()
    {
        if (Node_PlayerIsOn.GetComponent<Script_Node>().NodeRight != null)
        {
            transform.rotation = Quaternion.Euler(0.0f, 100.0f, 0.0f);

            Player_Movment = true;
            Node_MovingTo = Node_PlayerIsOn.GetComponent<Script_Node>().NodeRight;
        }
    }

}

