using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Thief : Script_Creatures
{
   

    // Use this for initialization
    void Start ()
    {
        CurrentHealth = 400;
        MaxHealth = 400;
        CurrentMana = 100;
        MaxMana = 200;
        Strength = 500;
        Magic = 500;
        Dexterity = 60;
        Speed = 90;
        Name = "Thief";

        m_Skills = new Script_Skills[3];
        m_Skills[0] = gameObject.AddComponent<Script_Attack>();
        m_Skills[1] = gameObject.AddComponent<Script_HolyWater>();
        m_Skills[2] = gameObject.AddComponent<Script_PhoenixSpirit>();

        m_BloodArts = new Script_Skills[1];
        m_BloodArts[0] = gameObject.AddComponent<Script_BloodRelief>();




        Model = (GameObject)Resources.Load("Prefabs/Battle/PartyModels/Main_Character", typeof(GameObject));

        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Wind;
        elementalWeakness = ElementalWeakness.Fire;
    }

}
