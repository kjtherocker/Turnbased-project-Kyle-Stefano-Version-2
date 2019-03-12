using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Thief : Script_Creatures
{
   

    // Use this for initialization
    void Start ()
    {
        CurrentHealth = 700;
        MaxHealth = 700;
        CurrentMana = 100;
        MaxMana = 200;
        Strength = 300;
        Magic = 200;
        Dexterity = 60;
        Speed = 90;
        Name = "Thief";

        AmountOfTurns = 1;

       //m_Skills = new Script_Skills[4];
       //m_Skills[0] = gameObject.AddComponent<Script_HolyWater>();
       //m_Skills[1] = gameObject.AddComponent<Script_PhoenixSpirit>();
       //m_Skills[2] = gameObject.AddComponent<Script_IceRain>();
       //m_Skills[3] = gameObject.AddComponent<Script_Restrict>();
       //
       //m_BloodArts = new Script_Skills[1];
       //m_BloodArts[0] = gameObject.AddComponent<Script_BloodRelief>();


        SetCreature();

        Model = (GameObject)Resources.Load("Prefabs/Battle/PartyModels/Main_Character", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_Knight", typeof(Material));

        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Wind;
        elementalWeakness = ElementalWeakness.Fire;
    }

}
