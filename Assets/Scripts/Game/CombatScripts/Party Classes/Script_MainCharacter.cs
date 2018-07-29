using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Script_MainCharacter : Script_Creatures {


   

	// Use this for initialization
	void Start ()
    {
        CurrentHealth = 80;
        MaxHealth = 80;
        CurrentMana = 100;
        MaxMana = 200 ;
        Strength = 50;
        Magic = 50;
        Dexterity = 50;
        Speed = 50;
        Name = "MainCharacter";
   
        m_Skills = new Script_Skills[8];
        m_Skills[0] = gameObject.AddComponent<Script_FireBall>();

        Model = (GameObject)Resources.Load("Prefabs/Battle/PartyModels/Main_Character", typeof(GameObject));

        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Null;
        elementalWeakness = ElementalWeakness.Shadow;

    }
	
	// Update is called once per frame

}
