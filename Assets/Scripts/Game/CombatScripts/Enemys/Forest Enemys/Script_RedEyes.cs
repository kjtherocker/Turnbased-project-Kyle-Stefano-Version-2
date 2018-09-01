using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_RedEyes : Script_Creatures
{

    // Use this for initialization
    void Start()
    {
        CurrentHealth = 2000;
        MaxHealth = 2000;
        CurrentMana = 10;
        MaxMana = 10;
        Strength = 75;
        Magic = 300;
        Dexterity = 10;
        Speed = 10;
        Name = "Red Eyes";

        m_Skills = new Script_Skills[2];
        m_Skills[0] = gameObject.AddComponent<Script_RedEyesEncroach>();
        m_Skills[1] = gameObject.AddComponent<Script_RedEyesEncroach>();

        Model = (GameObject)Resources.Load("Prefabs/Battle/Enemy/Forest/Bosses/Prefab_RedEyes", typeof(GameObject));





        charactertype = Charactertype.Enemy;
        elementalStrength = ElementalStrength.Fire;
        elementalWeakness = ElementalWeakness.Water;
    }

    public override int EnemyAi()
    {
        int SkillChosen = Random.Range(0, m_Skills.Length);

        return SkillChosen;
    }

}