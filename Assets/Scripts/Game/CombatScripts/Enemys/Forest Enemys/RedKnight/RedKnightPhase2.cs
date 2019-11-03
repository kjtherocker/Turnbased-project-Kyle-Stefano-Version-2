using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKnightPhase2 : Creatures
{

    // Use this for initialization
    void Start()
    {
        CurrentHealth = 200;
        MaxHealth = 200;
        CurrentMana = 10;
        MaxMana = 10;
        Strength = 350;
        Magic = 350;
        Dexterity = 10;
        Speed = 10;
        Name = "R2";


        SetCreature();

        m_Attack = GameManager.Instance.SkillList.SetSkills(SkillList.SkillEnum.Attack);

        m_Skills.Add(GameManager.Instance.SkillList.SetSkills(SkillList.SkillEnum.Attack));


        AmountOfTurns = 1;

        Model = (GameObject)Resources.Load("Objects/Battle/Enemy/Forest/RedKnights/Pref_RedKnight_Phase2", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_GreenSlime", typeof(Material));

        charactertype = Charactertype.Enemy;
        elementalStrength = ElementalStrength.Water;
        elementalWeakness = ElementalWeakness.Fire;
    }


}