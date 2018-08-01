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

   public int BuffandDebuff;

   public string Name = "No Name";

   public GameObject Model;
   public GameObject ModelInGame;

    public GameObject WeaknessIndicator;
    public GameObject StrongIndicator;
    public GameObject MissIndicator;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              

    bool IsAlive = true;

    public void Start()
    {
        
        WeaknessIndicator = (GameObject)Resources.Load("Prefabs/Battle/Text/Image_Weak", typeof(GameObject)); 
        StrongIndicator = (GameObject)Resources.Load("Prefabs/Battle/Text/Image_Strong", typeof(GameObject));
        MissIndicator = (GameObject)Resources.Load("Prefabs/Battle/Text/Image_Miss", typeof(GameObject));
    }
    // Update is called once per frame
    public void Update ()
    {
        
        WeaknessIndicator = (GameObject)Resources.Load("Prefabs/Battle/Text/Image_Weak", typeof(GameObject));
        StrongIndicator = (GameObject)Resources.Load("Prefabs/Battle/Text/Image_Strong", typeof(GameObject));
        MissIndicator = (GameObject)Resources.Load("Prefabs/Battle/Text/Image_Miss", typeof(GameObject));
        if (CurrentHealth <= 0)
        {
            IsAlive = false;
            Death();
        }

        if (BuffandDebuff > 3)
        {
            BuffandDebuff = 3;
        }
        if (BuffandDebuff < -3)
        {
            BuffandDebuff = -3;
        }

	}

    public void DecrementMana(int Decrementby)
    {
        CurrentMana -= Decrementby;
    }

    public void IncrementMana(int Incrementby)
    {
        CurrentMana += Incrementby;
    }


    public void DecrementHealth(int Decrementby , Script_Skills.ElementalType elementalType)
    {
        Script_FloatingUiElementsController.Initalize();
        string AttackingElement = elementalType.ToString();
        string ElementalWeakness = elementalWeakness.ToString();
        string ElementalStrength = elementalStrength.ToString();

        if (AttackingElement.Equals(ElementalWeakness))
        {
            int ArgumentReference = Decrementby;
            float ConvertToFloat = ArgumentReference * 1.5f;
            int ConvertToInt = Mathf.CeilToInt(ConvertToFloat);
            Decrementby = ConvertToInt;

            //Instantiate<GameObject>(WeaknessIndicator, gameObject.transform); 


        }
        if (AttackingElement.Equals(ElementalStrength))
        {
            int ArgumentReference = Decrementby;
            float ConvertToFloat = ArgumentReference / 1.5f;
            int ConvertToInt = Mathf.CeilToInt(ConvertToFloat);
            Decrementby = ConvertToInt;

           // Instantiate<GameObject>(StrongIndicator, ModelInGame.gameObject.transform);
        }

        

        Script_FloatingUiElementsController.CreateFloatingText(Decrementby.ToString(), ModelInGame.gameObject.transform);

        CurrentHealth -= Decrementby ;
    }


    public void IncrementHealth(int Increment)
    {
        CurrentHealth += Increment;
    }


    void Death()
    {
        
            ModelInGame.gameObject.SetActive(false);
         
        

    }




}
