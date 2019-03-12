using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_SlimeBlue : Script_Creatures
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
        Name = "Blue Slime";

        //m_Skills = new Script_Skills[2];
        //m_Skills[0] = gameObject.AddComponent<Script_IceRain>();
        //m_Skills[1] = gameObject.AddComponent<Script_FireBall>();

        AmountOfTurns = 1;

        Model = (GameObject)Resources.Load("Prefabs/Battle/Enemy/Forest/Slime/Prefab_SlimeBlue", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_BlueSlime", typeof(Material));

        charactertype = Charactertype.Enemy;
        elementalStrength = ElementalStrength.Water;
        elementalWeakness = ElementalWeakness.Fire;
    }

    public override int EnemyAi()
    {
        int SkillChosen = Random.Range(0, m_Skills.Count);

        return SkillChosen;
    }

}
