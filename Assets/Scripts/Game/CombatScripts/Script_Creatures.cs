using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Script_Creatures : MonoBehaviour
{

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

    public enum CreaturesAilment
    {
        None,
        Poison,
        Daze,
        Sleep,
        Rage,

    }
    public enum DomainStages
    {
        NotActivated,
        Encroaching,
        Finished,
        End
    }


    public Script_Skills m_Domain;
    public Script_Skills m_Attack;
    public List<Script_Skills> m_Skills { get; protected set; }
    public List<Script_Skills> m_BloodArts { get; protected set; }

    public Script_AiController m_CreatureAi;

    public CreaturesAilment m_creaturesAilment;
    public Charactertype charactertype;
    public ElementalStrength elementalStrength;
    public ElementalWeakness elementalWeakness;
    public DomainStages m_DomainStages;

    public int CurrentHealth;
    public int MaxHealth;
    public int CurrentMana;
    public int MaxMana;
    public int Strength;
    public int Magic;
    public int Dexterity;
    public int Speed;

    public int m_CreatureMovement = 4;

    public int AmountOfTurns;

    public int BuffandDebuff;
    public int BuffandDebuffDamageStrength;
    public int BuffandDebuffDamageMagic;

    public bool IsSelected;
    public bool IsCurrentTurnHolder;

    public string Name = "No Name";

    public Material m_Texture;

    public GameObject Model;
    public GameObject ModelInGame;

    public Script_Creatures ObjectToRotateAround;


    int AlimentCounter;

    bool m_IsAlive;


    // Update is called once per frame
    public void SetCreature()
    {
        m_Skills = new List<Script_Skills>();
        m_BloodArts = new List<Script_Skills>();

        m_DomainStages = DomainStages.NotActivated;
       
        //m_Attack = gameObject.AddComponent<Script_Attack>();
    }
    public virtual void Update()
    {

        if (CurrentHealth <= 0)
        {
            m_IsAlive = false;
            Death();
        }
        else if (CurrentHealth >= 0)
        {
            m_IsAlive = true;
        }

        if (CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }



    }

    public virtual void EndTurn()
    {


    }

    public virtual void SetObjectToRotateAround(Script_Creatures gameObject)
    {
        ObjectToRotateAround = gameObject;

    }

    public virtual int GetAllStrength()
    {
        int TemporaryStrength;

        TemporaryStrength = BuffandDebuffDamageStrength + Strength;

        return TemporaryStrength;
    }

    public virtual int GetAllMagic()
    {
        int TemporaryMagic;

        TemporaryMagic = BuffandDebuffDamageMagic + Magic;

        return TemporaryMagic;
    }



    public virtual void DecrementHealth(int Decremenby)
    {
        Script_FloatingUiElementsController.CreateFloatingText(Decremenby.ToString(), ModelInGame.gameObject.transform, Script_FloatingUiElementsController.UiElementType.Text);
        CurrentHealth -= Decremenby;
    }
    public virtual IEnumerator DecrementHealth(int Decrementby, Script_Skills.ElementalType elementalType,float TimeTillInitalDamage, float TimeTillHoveringUiElement, float TimeTillDamage)
    {
        if (m_creaturesAilment == CreaturesAilment.Sleep)
        {
            AlimentCounter = 0;
        }
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


            yield return new WaitForSeconds(TimeTillHoveringUiElement);
            Script_FloatingUiElementsController.CreateFloatingText(Decrementby.ToString(), ModelInGame.gameObject.transform, Script_FloatingUiElementsController.UiElementType.Weak);

        }
        if (AttackingElement.Equals(ElementalStrength))
        {
            int ArgumentReference = Decrementby;
            float ConvertToFloat = ArgumentReference / 1.5f;
            int ConvertToInt = Mathf.CeilToInt(ConvertToFloat);
            Decrementby = ConvertToInt;

            yield return new WaitForSeconds(TimeTillHoveringUiElement);
            Script_FloatingUiElementsController.CreateFloatingText(Decrementby.ToString(), ModelInGame.gameObject.transform, Script_FloatingUiElementsController.UiElementType.Strong);

        }

        yield return new WaitForSeconds(TimeTillDamage);
        Script_FloatingUiElementsController.CreateFloatingText(Decrementby.ToString(), ModelInGame.gameObject.transform, Script_FloatingUiElementsController.UiElementType.Text);



        CurrentHealth -= Decrementby;


    }


    public virtual IEnumerator IncrementHealth(int Increment)
    {
        CurrentHealth += Increment;
        yield return new WaitForSeconds(0.5f);
        Script_FloatingUiElementsController.Initalize();
        Script_FloatingUiElementsController.CreateFloatingText(Increment.ToString(), ModelInGame.gameObject.transform, Script_FloatingUiElementsController.UiElementType.Text);
    }

    public virtual Charactertype GetCharactertype()
    {

        return charactertype;
    }

    public virtual void Resurrection()
    {
        ModelInGame.gameObject.SetActive(true);
    }
    public virtual void Death()
    {
        CurrentHealth = 0;
        AlimentCounter = 0;
        BuffandDebuff = 0;
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
