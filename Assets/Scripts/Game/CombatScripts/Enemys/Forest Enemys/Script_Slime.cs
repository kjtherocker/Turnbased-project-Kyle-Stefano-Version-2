using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Slime : Script_Creatures
{

    // Use this for initialization
    void Start()
    {
        CurrentHealth = 10;
        MaxHealth = 10;
        CurrentMana = 10;
        MaxMana = 10;
        Strength = 10;
        Magic = 10;
        Dexterity = 10;
        Speed = 10;
        Name = "Slime";

        charactertype = Charactertype.Enemy;
        elementalStrength = ElementalStrength.Null;
        elementalWeakness = ElementalWeakness.Fire;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
