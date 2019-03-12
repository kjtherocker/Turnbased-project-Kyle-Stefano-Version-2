using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Priest : Script_Creatures
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
        Name = "Priest";

        AmountOfTurns = 1;

       // m_Skills = new Script_Skills[2];
       // m_Skills[0] = gameObject.AddComponent<Script_HolyWater>();
       // m_Skills[1] = gameObject.AddComponent<Script_LightRay>();
       //
       // m_BloodArts = new Script_Skills[1];
       // m_BloodArts[0] = gameObject.AddComponent<Script_BloodRelief>();

        SetCreature();

        Model = (GameObject)Resources.Load("Prefabs/Battle/PartyModels/Main_Character", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_Knight", typeof(Material));

        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Light;
        elementalWeakness = ElementalWeakness.Shadow;
    }

}
