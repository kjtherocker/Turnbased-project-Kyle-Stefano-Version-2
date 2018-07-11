using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Model : MonoBehaviour {


    public Script_Creatures CreatureParent;
    public Script_Model m_ModelInScript;

    // Use this for initialization
    void Start ()
    {
		
	}

    public void SetParent(Script_Creatures Creaturetoparent)
    {
        CreatureParent = Creaturetoparent;
      //  m_ModelInScript = CreatureParent.Model;

    }

    public Script_Model GetModel()
    {
        return m_ModelInScript;
    }

	// Update is called once per frame
	void Update ()
    {
        //if (CreatureParent.Model == null)
       // {
           // gameObject.SetActive(false);
       // }
		
	}
}
