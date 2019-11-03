using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour {

   public Creatures EnemySlot1;
   public Creatures EnemySlot2;
   public Creatures EnemySlot3;
   public Creatures EnemySlot4;

    public Creatures EnemySlotBoss;

    public  int test = 0;

    public enum EncounterTypes
    {
        ForestEncounter,
        BossForestEncounter


    }





    // Use this for initialization
    void Start ()
    {
        EnemySlot1 = gameObject.AddComponent<RedKnightPhase4>();
        EnemySlot2 = gameObject.AddComponent<RedKnightPhase1>();
        EnemySlot3 = gameObject.AddComponent<RedKnightPhase2>();
        EnemySlot4 = gameObject.AddComponent<RedKnightPhase3>();
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


        Component[] Creatures = GetComponents<Creatures>() as Component[];
        foreach (Component creatures in Creatures)
        {
            Destroy(creatures as Creatures);
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
        EnemySlotBoss = gameObject.AddComponent<RedEyes>();

    }

    public void ForestEncounter1()
    {
     //  EnemySlot1 = gameObject.AddComponent<RedKnightPhase4>();
     //  EnemySlot2 = gameObject.AddComponent<RedKnightPhase1>();
     //  EnemySlot3 = gameObject.AddComponent<RedKnightPhase2>();
     //  EnemySlot4 = gameObject.AddComponent<RedKnightPhase3>();

    }
}
