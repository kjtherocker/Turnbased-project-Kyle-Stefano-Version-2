using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavia : Creatures
{


    // Use this for initialization
    void Start()
    {
        CurrentHealth = 700;
        MaxHealth = 700;
        CurrentMana = 100;
        MaxMana = 200;
        Strength = 200;
        Magic = 300;
        Dexterity = 60;
        Speed = 90;
        Name = "Cavia";

        AmountOfTurns = 1;

        m_CreatureMovement = 8;

        // m_Skills = new CombatNodeSkills[2];
        // m_Skills[0] = gameObject.AddComponent<CombatNodeHolyWater>();
        // m_Skills[1] = gameObject.AddComponent<CombatNodeLightRay>();
        //
        // m_BloodArts = new CombatNodeSkills[1];
        // m_BloodArts[0] = gameObject.AddComponent<CombatNodeBloodRelief>();

        SetCreature();

        Model = (GameObject)Resources.Load("Objects/Battle/PartyModels/Cavia/Prefab/Pref_Cavia", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_Knight", typeof(Material));

        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Light;
        elementalWeakness = ElementalWeakness.Shadow;
    }

}
