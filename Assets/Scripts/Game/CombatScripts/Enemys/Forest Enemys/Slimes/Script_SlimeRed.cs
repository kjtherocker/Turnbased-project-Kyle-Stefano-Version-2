using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_SlimeRed : Script_Creatures
{

    // Use this for initialization
    void Start()
    {
        CurrentHealth = 300;
        MaxHealth = 200;
        CurrentMana = 10;
        MaxMana = 10;
        Strength = 25;
        Magic = 75;
        Dexterity = 10;
        Speed = 10;
        Name = "Red Slime";

        m_Skills = new Script_Skills[3];
        m_Skills[0] = gameObject.AddComponent<Script_FireBall>();
        m_Skills[1] = gameObject.AddComponent<Script_FireBall>();
        m_Skills[2] = gameObject.AddComponent<Script_Invigorate>();

        //"Prefabs/Battle/Enemy/Forest/model_Slime"

        Model = (GameObject)Resources.Load("Prefabs/Battle/Enemy/Forest/Slime/Prefab_SlimeRed", typeof(GameObject));



        charactertype = Charactertype.Enemy;
        elementalStrength = ElementalStrength.Fire;
        elementalWeakness = ElementalWeakness.Water;
    }

    public override int EnemyAi()
    {
      int SkillChosen =  Random.Range(0, m_Skills.Length);

        return SkillChosen;
    }
}
