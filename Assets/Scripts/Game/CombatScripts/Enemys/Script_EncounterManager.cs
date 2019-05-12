using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_EncounterManager : MonoBehaviour {

   public Script_Creatures EnemySlot1;
   public Script_Creatures EnemySlot2;
   public Script_Creatures EnemySlot3;
   public Script_Creatures EnemySlot4;

    public Script_Creatures EnemySlotBoss;

    public  int test = 0;

    public enum EncounterTypes
    {
        ForestEncounter,
        BossForestEncounter


    }





    // Use this for initialization
    void Start ()
    {
        //  EnemySlot1 = gameObject.AddComponent<Script_Slime>();
        //  EnemySlot2 = gameObject.AddComponent<Script_Slime>();
        //  EnemySlot3 = gameObject.AddComponent<Script_Slime>();
        //  EnemySlot4 = gameObject.AddComponent<Script_Slime>();
        //SetEncounter(EncounterTypes.ForestEncounter);
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


    }



    public void  SetEncounter(EncounterTypes a_encounter)
    {

        if (a_encounter == EncounterTypes.ForestEncounter)
        {
            int EncounterChosen = Random.Range(0, 5);

            if (EncounterChosen == 0 )
            {
                ForestEncounter1();
            }
            if (EncounterChosen == 1)
            {
                ForestEncounter1();
            }
            if ( EncounterChosen == 2)
            {
                ForestEncounter1();
            }
            if (EncounterChosen == 3 )
            {
                ForestEncounter1();
            }
            if (EncounterChosen == 4)
            {
                ForestEncounter1();
            }
        }
        if (a_encounter == EncounterTypes.BossForestEncounter)
        {
            ForestEncounterRedeyes();
        }

    }

    public void ForestEncounterRedeyes()
    {
        EnemySlotBoss = gameObject.AddComponent<Script_RedEyes>();

    }

    public void ForestEncounter1()
    {
        EnemySlot1 = gameObject.AddComponent<Script_RedKnightPhase4>();
        EnemySlot2 = gameObject.AddComponent<Script_RedKnightPhase1>();
        EnemySlot3 = gameObject.AddComponent<Script_RedKnightPhase2>();
        EnemySlot4 = gameObject.AddComponent<Script_RedKnightPhase3>();

    }
}
