using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_MainCharacter : Script_Creatures {


   

	// Use this for initialization
	void Start ()
    {
        CurrentHealth = 423;
        MaxHealth = 423;
        CurrentMana = 32;
        MaxMana = 43;
        Strength = 50;
        Magic = 50;
        Dexterity = 50;
        Speed = 50;
        Name = "MainCharacter";
   
        m_Skills = new Script_Skills[8];
        m_Skills[0] = gameObject.AddComponent<Script_FireBall>();

        

        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Null;
        elementalWeakness = ElementalWeakness.Shadow;

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
