using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vella : Creatures
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
        Name = "Vella";

        AmountOfTurns = 1;

       //m_Skills = new Skills[4];
       //m_Skills[0] = gameObject.AddComponent<HolyWater>();
       //m_Skills[1] = gameObject.AddComponent<PhoenixSpirit>();
       //m_Skills[2] = gameObject.AddComponent<IceRain>();
       //m_Skills[3] = gameObject.AddComponent<Restrict>();
       //
       //m_BloodArts = new Skills[1];
       //m_BloodArts[0] = gameObject.AddComponent<BloodRelief>();


        SetCreature();

        Model = (GameObject)Resources.Load("Objects/Battle/PartyModels/Vella/Prefab/Pref_Vella", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_Knight", typeof(Material));

        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Wind;
        elementalWeakness = ElementalWeakness.Fire;
    }

}
