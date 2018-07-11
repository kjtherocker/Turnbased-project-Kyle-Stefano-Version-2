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

        m_Skills = new Script_Skills[3];
        m_Skills[0] = gameObject.AddComponent<Script_FireBall>();

        Model = (GameObject)Resources.Load("Prefabs/Battle/PartyModels/Main_Character", typeof(GameObject));

        charactertype = Charactertype.Enemy;
        elementalStrength = ElementalStrength.Null;
        elementalWeakness = ElementalWeakness.Fire;
    }

}
