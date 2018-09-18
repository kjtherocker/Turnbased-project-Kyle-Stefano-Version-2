using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_TurnIndicatorWrapper : MonoBehaviour {

    public bool WhichTurnIsIt;
    public Material Material_Ally;
    public Material Material_Enemy;
    Image m_Image;
    // Use this for initialization
    void Start ()
    {
        
        m_Image = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (WhichTurnIsIt == true)
        {
            m_Image.material = Material_Ally;
        }
        if (WhichTurnIsIt == false)
        {
            m_Image.material = Material_Enemy;
        }

	}

    public void SetGroup(bool a_Whatgroup)
    {
        WhichTurnIsIt = a_Whatgroup;
    }

    public void DestroyThisObject()
    {
        Destroy(gameObject);
    }
}
