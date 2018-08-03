using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_SlimeWhite : Script_Creatures
{

    // Use this for initialization
    void Start()
    {
        CurrentHealth = 200;
        MaxHealth = 200;
        CurrentMana = 10;
        MaxMana = 10;
        Strength = 25;
        Magic = 75;
        Dexterity = 10;
        Speed = 10;
        Name = "Slime";

        m_Skills = new Script_Skills[3];
        m_Skills[0] = gameObject.AddComponent<Script_FireBall>();

        //"Prefabs/Battle/Enemy/Forest/model_Slime"

        Model = (GameObject)Resources.Load("Prefabs/Battle/Enemy/Forest/Slime/Prefab_SlimeWhite", typeof(GameObject));



        charactertype = Charactertype.Enemy;
        elementalStrength = ElementalStrength.Fire;
        elementalWeakness = ElementalWeakness.Water;
    }
}
