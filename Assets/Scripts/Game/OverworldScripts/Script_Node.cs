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


    Renderer m_Renderer;

    bool RoadsAreSpawned;

    public enum NodeTypes
    {
        BasicNode,
        EncounterNode,
        ShopNode,
        EndNode
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

    }

    public void SetNodeType(NodeTypes nodetype)
    {
        Enum_NodeType = nodetype;
    }
}
