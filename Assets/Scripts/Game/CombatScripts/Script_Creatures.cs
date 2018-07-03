using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Creatures : MonoBehaviour {

   public enum Charactertype
    {
        Undefined,
        Ally,
        Enemy
    }

    public enum ElementalStrength
    {
        Null,
        Fire,
        Water,
        Wind,
        Lighting,
        Shadow,
        Light
    }
    public enum ElementalWeakness
    {
        Null,
        Fire,
        Water,
        Wind,
        Lighting,
        Shadow,
        Light

    }


   public Charactertype charactertype;
   public ElementalStrength elementalStrength;
   public ElementalWeakness elementalWeakness;

   public int CurrentHealth;
   public int MaxHealth;
   public int CurrentMana;
   public int MaxMana;
   public int Strength;
   public int Magic;
   public int Dexterity;
   public int Speed;

   public string Name = "No Name";

   public GameObject Model;

	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Death()
    {

    }




}
