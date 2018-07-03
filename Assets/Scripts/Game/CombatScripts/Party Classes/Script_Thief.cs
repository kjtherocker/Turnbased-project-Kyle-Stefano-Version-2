using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Thief : Script_Creatures
{
   

    // Use this for initialization
    void Start ()
    {
        CurrentHealth = 32;
        MaxHealth = 32;
        CurrentMana = 43;
        MaxMana = 43;
        Strength = 24;
        Magic = 32;
        Dexterity = 60;
        Speed = 90;
        Name = "Thiefboy";
        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Wind;
        elementalWeakness = ElementalWeakness.Fire;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
