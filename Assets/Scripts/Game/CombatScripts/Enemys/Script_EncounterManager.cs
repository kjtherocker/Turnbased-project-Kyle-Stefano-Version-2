using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_EncounterManager : MonoBehaviour {

   public Script_Creatures EnemySlot1;
   public Script_Creatures EnemySlot2;
   public Script_Creatures EnemySlot3;
   public Script_Creatures EnemySlot4;

   public  int test = 0;

    public enum EncounterTypes
    {
        ForestEncounter


    }





    // Use this for initialization
    void Start ()
    {
      //  EnemySlot1 = gameObject.AddComponent<Script_Slime>();
      //  EnemySlot2 = gameObject.AddComponent<Script_Slime>();
      //  EnemySlot3 = gameObject.AddComponent<Script_Slime>();
      //  EnemySlot4 = gameObject.AddComponent<Script_Slime>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ResetEncounterManager()
    {
        EnemySlot1 = null;
        EnemySlot2 = null;
        EnemySlot3 = null;
        EnemySlot4 = null;


        Component[] Creatures = GetComponents<Script_Creatures>() as Component[];
        foreach (Component creatures in Creatures)
        {
            Destroy(creatures as Script_Creatures);
        }

        Component[] Skills = GetComponents<Script_Skills>() as Component[];
        foreach (Component skill in Skills)
        {
            Destroy(skill as Script_Skills);
        }


    }



    public void  SetEncounter(EncounterTypes a_encounter)
    {

        if (a_encounter == EncounterTypes.ForestEncounter)
        {
            ForestEncounter1();

        }

    }


    public void ForestEncounter1()
    {
        EnemySlot1 = gameObject.AddComponent<Script_SlimeGreen>();
        EnemySlot2 = gameObject.AddComponent<Script_SlimeGreen>();
        EnemySlot3 = gameObject.AddComponent<Script_SlimeGreen>();
        EnemySlot4 = gameObject.AddComponent<Script_SlimeWhite>();
      
    }
}
