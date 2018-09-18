using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Script_MainCharacter : Script_Creatures {


   

	// Use this for initialization
	void Start ()
    {
        CurrentHealth = 700;
        MaxHealth = 700;
        CurrentMana = 100;
        MaxMana = 200 ;
        Strength = 200;
        Magic = 300;
        Dexterity = 50;
        Speed = 50;
        Name = "Knight";

        AmountOfTurns = 1;

        m_Skills = new Script_Skills[3];
        m_Skills[0] = gameObject.AddComponent<Script_FireBall>();
        m_Skills[1] = gameObject.AddComponent<Script_PhoenixSpirit>();
        m_Skills[2] = gameObject.AddComponent<Script_IceRain>();

        m_BloodArts = new Script_Skills[1];
        m_BloodArts[0] = gameObject.AddComponent<Script_BloodRelief>();

        SetCreature();

        Model = (GameObject)Resources.Load("Prefabs/Battle/PartyModels/Main_Character", typeof(GameObject));
        
        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_Knight", typeof(Material));



        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Fire;
        elementalWeakness = ElementalWeakness.Null;

    }
	
	// Update is called once per frame

}
