using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Node : MonoBehaviour {

    // Use this for initialization

    public Script_Node NodeUp;
    public Script_Node NodeDown;
    public Script_Node NodeLeft;
    public Script_Node NodeRight;

    public DialogueTrigger m_DialogueTrigger;

    public GameObject m_EnemyPlaneReference;
    public GameObject m_SpawnedObject;

    public Script_GridFormations m_GridFormation;

    bool RoadsAreSpawned;

    public enum NodeTypes
    {
        BasicNode,
        DialogueNode,
        EncounterNode,
        ShopNode,
        EndNode
    };

    public NodeTypes Enum_NodeType;
    

    void Start ()
    {
        RoadsAreSpawned = false;
    }
	
    public void SetNodeType(NodeTypes nodetype)
    {
        if (m_SpawnedObject != null)
        {
            Destroy(m_SpawnedObject);
        }

        Enum_NodeType = nodetype;
    }

    public void StartDialogue()
    {
        m_DialogueTrigger = Instantiate<DialogueTrigger>(m_DialogueTrigger);

        m_DialogueTrigger.TriggerDialogue();
    }
}
