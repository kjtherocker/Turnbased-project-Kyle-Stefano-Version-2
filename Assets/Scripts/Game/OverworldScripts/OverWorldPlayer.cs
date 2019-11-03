using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


public class OverWorldPlayer : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    public PlayerInput m_Controls;

    public Node Node_MovingTo;
    public Node Node_PlayerIsOn;
    public GameManager GameManager;
    public EncounterManager m_EncounterManager;
    public GameObject OverworldModel;
    public GameObject Canvas_PartyMenu;
    public PartyManager PartyManager;
    public PartyMenu m_PartyMenu;

    public DialogueManager m_DialogueManager;

    public Material m_GridMaterial;

    public float Player_Speed = 5;
    public bool Player_Movment = false;
    private bool IsPartyMenuOn;
    private float Player_Speed_Delta;
    public GameObject m_OverworldPlayerModel;





    void Start ()
    {

        GameManager.Instance.m_InputManager.m_MovementControls.Player.Movement.performed += movement => PlayerMovement(movement.ReadValue<Vector2>());
        GameManager = GameManager.Instance;
        m_EncounterManager = GameManager.Instance.m_EncounterManager;
        PartyManager = GameManager.Instance.m_PartyManager;
        m_OverworldPlayerModel = Instantiate<GameObject>(OverworldModel, gameObject.transform);
        gameObject.transform.position = Node_PlayerIsOn.transform.position;
        IsPartyMenuOn = false;
        //CombineMeshes();
    }
	
	// Update is called once per frame
	void Update ()
    {

        switch (Node_PlayerIsOn.Enum_NodeType)
        {
            case Node.NodeTypes.BasicNode:

                break;

            case Node.NodeTypes.EncounterNode:

                if (Node_PlayerIsOn.m_GridFormation != null)
                {
                    GameManager.Instance.CombatManager.m_GridFormation = Node_PlayerIsOn.m_GridFormation;
                }
                m_EncounterManager.SetEncounter(EncounterManager.EncounterTypes.ForestEncounter);
                GameManager.SwitchToBattle();

                Node_PlayerIsOn.SetNodeType(Node.NodeTypes.BasicNode);


                break;

            case Node.NodeTypes.EndNode:

                m_EncounterManager.SetEncounter(EncounterManager.EncounterTypes.BossForestEncounter);
                GameManager.SwitchToBattle();

                Node_PlayerIsOn.SetNodeType(Node.NodeTypes.BasicNode);

                break;

            case Node.NodeTypes.DialogueNode:
                m_DialogueManager.gameObject.SetActive(true);
                Node_PlayerIsOn.StartDialogue();
                Node_PlayerIsOn.SetNodeType(Node.NodeTypes.BasicNode);

                break;

            case Node.NodeTypes.ShopNode:

                SceneManager.LoadScene(1);
                break;
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

    public void PlayerMovement(Vector2 aMovement)
    {
        //Up
        if (aMovement == new Vector2(0.0f,1.0f))
        {
            PlayerDirection(Node_PlayerIsOn.NodeUp);
        }

        //Down
        if (aMovement == new Vector2(0.0f, -1.0f))
        {
            PlayerDirection(Node_PlayerIsOn.NodeDown);
        }

        //Left
        if (aMovement == new Vector2(-1.0f, 0.0f))
        {
            PlayerDirection(Node_PlayerIsOn.NodeLeft);
        }

        //Right
        if (aMovement == new Vector2(1.0f, 0.0f))
        {
            PlayerDirection(Node_PlayerIsOn.NodeRight);
        }
    }

    public void PlayerDirection(Node node)
    {
        if (node != null)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            Player_Movment = true;
            Node_MovingTo = node;
        }
    }

}

