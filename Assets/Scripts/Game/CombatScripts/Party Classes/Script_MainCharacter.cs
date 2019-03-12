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

        //m_BaseSkill = new Script_Skills();
        //
        m_Skills.Add(gameObject.AddComponent<Script_Attack>());
        // m_Skills.Add(m_BaseSkill);
        // m_Skills.Add(m_BaseSkill);

        // m_BloodArts[0] = gameObject.AddComponent<Script_BloodRelief>();

        SetCreature();

        Model = (GameObject)Resources.Load("Prefabs/Battle/PartyModels/Main_Character", typeof(GameObject));
        
        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_Knight", typeof(Material));



        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Fire;
        elementalWeakness = ElementalWeakness.Null;

    }
	
	// Update is called once per frame

}
