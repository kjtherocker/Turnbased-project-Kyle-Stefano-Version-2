using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

   public Script_Skills[] m_Skills;

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

   bool IsAlive = true;


    // Update is called once per frame
    public void Update ()
    {
     
        //if (CurrentHealth <= 0)
        //{
        //    IsAlive = false;
        //    Death();
        //}
	}

    public void DecrementMana(int Decrementby)
    {
        CurrentMana -= Decrementby;
    }

    public void IncrementMana(int Incrementby)
    {
        CurrentMana += Incrementby;
    }


    public void DecrementHealth(int Decrementby)
    {
        CurrentHealth -= Decrementby ;
    }


    public void IncrementHealth(int Increment)
    {
        CurrentHealth += Increment;
    }


    void Death()
    {
        if (charactertype == Charactertype.Enemy)
        {
          //  gameObject.SetActive(false);
         
        }

    }




}
