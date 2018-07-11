using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_EncounterManager : MonoBehaviour {

   public Script_Creatures EnemySlot1;
   public Script_Creatures EnemySlot2;
   public Script_Creatures EnemySlot3;
   public Script_Creatures EnemySlot4;




    // Use this for initialization
    void Start ()
    {
        EnemySlot1 = gameObject.AddComponent<Script_Slime>();
        EnemySlot2 = gameObject.AddComponent<Script_Slime>();
        EnemySlot3 = gameObject.AddComponent<Script_Slime>();
        EnemySlot4 = gameObject.AddComponent<Script_Slime>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
