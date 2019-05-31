using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class Script_PropList : MonoBehaviour
{

    public enum Props
    {
        None,

        //Trees
        Tree1,
        Tree2,
        Tree3,

        //Pillars
        Angled_Top_Broken_Pillar,
        Fully_Intact_Pillar,
        Middle_Broken_Pillar_1,
        Middle_Broken_Pillar_2,
        Middle_Dented_Pillar,
        Pillar_Stub,
        Top_Broken_Pillar,

        //Bridges
        StartBridge,
        MiddleBridge,
        EndBridge,

        //Ruin Walls

        Corner_Ruin_Wall,
        Modular_Wall_End,
        Modular_Wall_Mid1,
        Modular_Wall_Mid2,
        Modular_Wall_Mid3,
        Modular_Wall_Mid4,
        Modular_Wall_Mid5,
        Modular_Wall_Start,
        Singular_Ruin_Wall,



        //End
        NumberOfProps
    }



    public List<GameObject> m_PropSet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject ReturnPropData(Props aProp, string sourceName = "Global")
    {
        return m_PropSet[(int)aProp];
    }
}
