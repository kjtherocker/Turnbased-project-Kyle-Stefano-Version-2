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
        Magic = 250;
        Dexterity = 10;
        Speed = 10;
        Name = "Red Eyes";

        AmountOfTurns = 2;

        m_DomainStages = DomainStages.Encroaching;

        m_Skills = new Script_Skills[5];
        m_Skills[0] = gameObject.AddComponent<Script_IceRain>();
        m_Skills[1] = gameObject.AddComponent<Script_LightRay>();
        m_Skills[2] = gameObject.AddComponent<Script_ShadowBlast>();
        m_Skills[3] = gameObject.AddComponent<Script_FireBall>();
        m_Skills[4] = gameObject.AddComponent<Script_Invigorate>();

        Model = (GameObject)Resources.Load("Prefabs/Battle/Enemy/Forest/Bosses/Prefab_RedEyes", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_RedEyes", typeof(Material));

        m_Domain = gameObject.AddComponent<Script_RedEyesEncroach>();

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