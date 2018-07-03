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
		
	}
}
