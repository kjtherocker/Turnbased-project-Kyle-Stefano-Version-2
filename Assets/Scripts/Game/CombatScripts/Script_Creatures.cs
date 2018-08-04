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
   public int BuffandDebuffDamageStrength;
   public int BuffandDebuffDamageMagic;

    public bool IsSelected;

   public string Name = "No Name";

    public ParticleSystem m_SelectedParticlesystem;

   public GameObject Model;
   public GameObject ModelInGame;

    public GameObject WeaknessIndicator;
    public GameObject StrongIndicator;
    public GameObject MissIndicator;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              

    bool IsAlive = true;

    public void Start()
    {
        
    }
    // Update is called once per frame
    public void Update ()
    {
        

        if (CurrentHealth <= 0)
        {
            IsAlive = false;
            Death();
        }

        DebuffsandBuffs();



    }

    public void EndTurn()
    {
        if (BuffandDebuff > 0)
        {
            BuffandDebuff--;
        }
        if (BuffandDebuff < 0)
        {
            BuffandDebuff++;
        }

    }

    public void DebuffsandBuffs()
    {
        //Cant go further then maxium
        if (BuffandDebuff > 3)
        {
            BuffandDebuff = 3;
        }
        if (BuffandDebuff < -3)
        {
            BuffandDebuff = -3;
        }

      

        //The Buffs
        if (BuffandDebuff == 1)
        {
            BuffandDebuffDamageStrength = Strength / 4;
            BuffandDebuffDamageMagic = Magic / 4;
        }
        if (BuffandDebuff == 2)
        {
            BuffandDebuffDamageStrength = Strength / 3;
            BuffandDebuffDamageMagic = Magic / 3;
        }
        if (BuffandDebuff == 3)
        {
            BuffandDebuffDamageStrength = Strength / 2;
            BuffandDebuffDamageMagic = Magic;
        }

        //NormalState
        if (BuffandDebuff == 0)
        {
            BuffandDebuffDamageStrength = 0;
            BuffandDebuffDamageMagic = 0;
        }

        //The Debuffs
        if (BuffandDebuff == -1)
        {
            BuffandDebuffDamageStrength = -Strength / 4;
            BuffandDebuffDamageMagic = -Magic / 4;
        }
        if (BuffandDebuff == -2)
        {
            BuffandDebuffDamageStrength = -Strength / 3;
            BuffandDebuffDamageMagic = -Magic / 3;
        }
        if (BuffandDebuff == -3)
        {
            BuffandDebuffDamageStrength = -Strength / 2;
            BuffandDebuffDamageMagic = -Magic / 2;
        }

    }

    public void AddBuff(int a_buffamount)
    {
        BuffandDebuff += a_buffamount;
    }

    public void AddDeBuff(int a_debuffamount)
    {
        BuffandDebuff -= a_debuffamount;
    }

    public int GetAllStrength()
    {
        int TemporaryStrength;

        TemporaryStrength = BuffandDebuffDamageStrength + Strength;

       return TemporaryStrength;
    }

    public int GetAllMagic()
    {
        int TemporaryMagic;

        TemporaryMagic = BuffandDebuffDamageMagic + Magic;

        return TemporaryMagic;
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
            Script_FloatingUiElementsController.CreateFloatingText(Decrementby.ToString(), ModelInGame.gameObject.transform, Script_FloatingUiElementsController.UiElementType.Weak);

        }
        if (AttackingElement.Equals(ElementalStrength))
        {
            int ArgumentReference = Decrementby;
            float ConvertToFloat = ArgumentReference / 1.5f;
            int ConvertToInt = Mathf.CeilToInt(ConvertToFloat);
            Decrementby = ConvertToInt;
            Script_FloatingUiElementsController.CreateFloatingText(Decrementby.ToString(), ModelInGame.gameObject.transform, Script_FloatingUiElementsController.UiElementType.Strong);
            // Instantiate<GameObject>(StrongIndicator, ModelInGame.gameObject.transform);
        }

        m_SelectedParticlesystem = (ParticleSystem)Resources.Load("ParticleSystems/SelectRedicule/ParticlesS_Circle", typeof(ParticleSystem));


        ParticleSystem InstnatiatedSelectionRedicle = Instantiate<ParticleSystem>(m_SelectedParticlesystem, ModelInGame.gameObject.transform);
        InstnatiatedSelectionRedicle.Play();

        Script_FloatingUiElementsController.CreateFloatingText(Decrementby.ToString(), ModelInGame.gameObject.transform, Script_FloatingUiElementsController.UiElementType.Text);

        CurrentHealth -= Decrementby ;
    }


    public void IncrementHealth(int Increment)
    {
        CurrentHealth += Increment;
        Script_FloatingUiElementsController.Initalize();
        Script_FloatingUiElementsController.CreateFloatingText(Increment.ToString(), ModelInGame.gameObject.transform, Script_FloatingUiElementsController.UiElementType.Text);
    }


    void Death()
    {

        if (charactertype == Charactertype.Enemy)
        {
            
            Destroy(ModelInGame.gameObject);
        }
        if (charactertype == Charactertype.Ally)
        {
            ModelInGame.gameObject.SetActive(false);
        }


        }




}
