using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PartyManager : MonoBehaviour {

   public Script_Creatures PartyMemberSlot1;
   public Script_Creatures PartyMemberSlot2;
   public Script_Creatures PartyMemberSlot3;
   public Script_Creatures PartyMemberSlot4;

   public Script_HealthBar BarSlot1;
   public Script_HealthBar BarSlot2;
   public Script_HealthBar BarSlot3;
   public Script_HealthBar BarSlot4;



    // Use this for initialization
    void Start ()
    {
        PartyMemberSlot1 = gameObject.AddComponent<Script_Thief>();
        PartyMemberSlot2 = gameObject.AddComponent<Script_Thief>();
        PartyMemberSlot3 = gameObject.AddComponent<Script_MainCharacter>();
        PartyMemberSlot4 = gameObject.AddComponent<Script_MainCharacter>();


        BarSlot1.Partymember = PartyMemberSlot1;
        BarSlot2.Partymember = PartyMemberSlot2;
        BarSlot3.Partymember = PartyMemberSlot3;
        BarSlot4.Partymember = PartyMemberSlot4;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (BarSlot1.Partymember == null)
        {
           // BarSlot1.SetActive(false);
        }

        if (PartyMemberSlot1.IsCurrentTurnHolder == true)
        {
            BarSlot1.transform.localPosition = new Vector3(271, -26, 0.8999996f);
        }
        else
        {
            BarSlot1.transform.localPosition = new Vector3(303, -26, 0.8999996f);
        }

        if (PartyMemberSlot2.IsCurrentTurnHolder == true)
        {
            BarSlot2.transform.localPosition = new Vector3(271, -1, 0.8999996f);
        }
        else
        {
            BarSlot2.transform.localPosition = new Vector3(303, -1, 0.8999996f);
        }

        if (PartyMemberSlot3.IsCurrentTurnHolder == true)
        {
            BarSlot3.transform.localPosition = new Vector3(271, 29, 0.8999996f);
        }
        else
        {
            BarSlot3.transform.localPosition = new Vector3(303, 29, 0.8999996f);
        }

        if (PartyMemberSlot4.IsCurrentTurnHolder == true)
        {
            BarSlot4.transform.localPosition = new Vector3(271, 57, 0.8999996f);
        }
        else
        {
            BarSlot4.transform.localPosition = new Vector3(303, 57, 0.8999996f);
        }


    }
}
