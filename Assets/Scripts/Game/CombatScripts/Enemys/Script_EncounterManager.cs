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
            int EncounterChosen = Random.Range(0, 3);

            if (EncounterChosen == 0 )
            {
                ForestEncounter2();
            }
            if (EncounterChosen == 1)
            {
                ForestEncounter3();
            }
            if ( EncounterChosen == 2)
            {
                ForestEncounter4();
            }
            if (EncounterChosen == 3 )
            {
                ForestEncounter1();
            }
        }

    }


    public void ForestEncounter1()
    {
        EnemySlot1 = gameObject.AddComponent<Script_SlimeGreen>();
        EnemySlot2 = gameObject.AddComponent<Script_SlimeGreen>();
        EnemySlot3 = gameObject.AddComponent<Script_SlimeGreen>();
        EnemySlot4 = gameObject.AddComponent<Script_SlimeWhite>();
      
    }

    public void ForestEncounter2()
    {
        EnemySlot1 = gameObject.AddComponent<Script_SlimeRed>();
        EnemySlot2 = gameObject.AddComponent<Script_SlimeRed>();
        EnemySlot3 = gameObject.AddComponent<Script_SlimeRed>();
        EnemySlot4 = gameObject.AddComponent<Script_SlimeRed>();

    }

    public void ForestEncounter3()
    {
        EnemySlot1 = gameObject.AddComponent<Script_SlimePurple>();
        EnemySlot2 = gameObject.AddComponent<Script_SlimeWhite>();
        EnemySlot3 = gameObject.AddComponent<Script_SlimeWhite>();
        EnemySlot4 = gameObject.AddComponent<Script_SlimePurple>();

    }

    public void ForestEncounter4()
    {
        EnemySlot1 = gameObject.AddComponent<Script_SlimeBlue>();
        EnemySlot2 = gameObject.AddComponent<Script_SlimeRed>();
        EnemySlot3 = gameObject.AddComponent<Script_SlimeRed>();
        EnemySlot4 = gameObject.AddComponent<Script_SlimeBlue>();

    }
}
