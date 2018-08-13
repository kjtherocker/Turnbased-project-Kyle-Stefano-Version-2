﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Priest : Script_Creatures
{


    // Use this for initialization
    void Start()
    {
        CurrentHealth = 400;
        MaxHealth = 400;
        CurrentMana = 100;
        MaxMana = 200;
        Strength = 500;
        Magic = 50;
        Dexterity = 60;
        Speed = 90;
        Name = "Priest";

        m_Skills = new Script_Skills[5];
        m_Skills[0] = gameObject.AddComponent<Script_Attack>();
        m_Skills[1] = gameObject.AddComponent<Script_HolyWater>();
        m_Skills[2] = gameObject.AddComponent<Script_LightRay>();
        m_Skills[3] = gameObject.AddComponent<Script_PassTurn>();

        Model = (GameObject)Resources.Load("Prefabs/Battle/PartyModels/Main_Character", typeof(GameObject));

        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Light;
        elementalWeakness = ElementalWeakness.Shadow;
    }

}
