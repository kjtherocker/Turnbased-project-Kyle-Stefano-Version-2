using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_NodeReplacement : MonoBehaviour
{
    public enum NodeReplacementType
    {
        Normal,
        RemoveInitalNode,
    }
    public GameObject m_Walkable;
    public Vector3 m_NodeSpawnOffSet;
    public float m_NodeHeightOffset;
    public NodeReplacementType m_NodeReplacementType;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
