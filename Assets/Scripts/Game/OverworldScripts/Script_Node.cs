using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Node : MonoBehaviour {

    // Use this for initialization

    public Script_Node NodeUp;
    public Script_Node NodeDown;
    public Script_Node NodeLeft;
    public Script_Node NodeRight;

    public Material MaterialBasicNode;
    public Material MaterialEncounterNode;
    public Material MaterialStoreNode;

    public GameObject model_CurvedE;
    public GameObject model_CurvedN;
    public GameObject model_CurvedS;
    public GameObject model_CurvedW;
    public GameObject model_DeadEndE;
    public GameObject model_DeadEndN;
    public GameObject model_DeadEndS;
    public GameObject model_DeadEndW;
    public GameObject model_StraightH;
    public GameObject model_StraightV;



    Renderer m_Renderer;

    bool RoadsAreSpawned;

    public enum NodeTypes
    {
        BasicNode,
        EncounterNode,
        ShopNode
    };

    public NodeTypes Enum_NodeType;
    

    void Start ()
    {
        // Enum_NodeType = NodeTypes.BasicNode;
        m_Renderer = GetComponent<Renderer>();
        RoadsAreSpawned = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Enum_NodeType == NodeTypes.BasicNode)
        {
          m_Renderer.material = MaterialBasicNode;

        }
        if (Enum_NodeType == NodeTypes.EncounterNode)
        {
            m_Renderer.material = MaterialEncounterNode;

        }
        if (Enum_NodeType == NodeTypes.ShopNode)
        {
            m_Renderer.material = MaterialStoreNode;

        }

        if (RoadsAreSpawned != true)
        {
            SpawnRoads();
        }

    }

    public void SpawnRoads()
    {
        if (NodeUp == null && NodeDown == null && NodeLeft == null)
        {
            Instantiate<GameObject>(model_DeadEndE, gameObject.transform);
            RoadsAreSpawned = true;
        }

        if (NodeUp == null && NodeRight == null && NodeLeft == null)
        {
            Instantiate<GameObject>(model_DeadEndS, gameObject.transform);
            RoadsAreSpawned = true;
        }

        if (NodeDown == null && NodeRight == null)
        {
            Instantiate<GameObject>(model_CurvedE, gameObject.transform);
            RoadsAreSpawned = true;
        }

        if (NodeLeft == null && NodeRight == null)
        {
            Instantiate<GameObject>(model_StraightV, gameObject.transform);
            RoadsAreSpawned = true;
        }
    }

    public void SetNodeType(NodeTypes nodetype)
    {
        Enum_NodeType = nodetype;
    }
}
