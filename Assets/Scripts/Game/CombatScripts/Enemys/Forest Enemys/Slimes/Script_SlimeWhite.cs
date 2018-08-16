using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_SlimeWhite : Script_Creatures
{

    // Use this for initialization
    void Start()
    {
        CurrentHealth = 200;
        MaxHealth = 200;
        CurrentMana = 10;
        MaxMana = 10;
        Strength = 25;
        Magic = 75;
        Dexterity = 10;
        Speed = 10;
        Name = "White Slime";

        m_Skills = new Script_Skills[2];
        m_Skills[0] = gameObject.AddComponent<Script_LightRay>();
        m_Skills[1] = gameObject.AddComponent<Script_FireBall>();

        //"Prefabs/Battle/Enemy/Forest/model_Slime"

        Model = (GameObject)Resources.Load("Prefabs/Battle/Enemy/Forest/Slime/Prefab_SlimeWhite", typeof(GameObject));



        charactertype = Charactertype.Enemy;
        elementalStrength = ElementalStrength.Light;
        elementalWeakness = ElementalWeakness.Shadow;
    }

    public override int EnemyAi()
    {
        int SkillChosen = Random.Range(0, m_Skills.Length);

        return SkillChosen;
    }
}
