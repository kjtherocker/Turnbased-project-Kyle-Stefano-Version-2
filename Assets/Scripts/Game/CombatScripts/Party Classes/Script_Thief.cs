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
        Strength = 24;
        Magic = 32;
        Dexterity = 60;
        Speed = 90;
        Name = "Thiefboy";

        m_Skills = new Script_Skills[5];
        m_Skills[0] = gameObject.AddComponent<Script_IceRain>();
        m_Skills[1] = gameObject.AddComponent<Script_Invigorate>();
        m_Skills[2] = gameObject.AddComponent<Script_HolyWater>();

        Model = (GameObject)Resources.Load("Prefabs/Battle/PartyModels/Main_Character", typeof(GameObject));

        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Wind;
        elementalWeakness = ElementalWeakness.Fire;
    }

}
