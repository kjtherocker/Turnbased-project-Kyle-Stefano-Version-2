using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_EnemyAiController : Script_AiController
{
    // Start is called before the first frame update

    public Script_AiController m_Target;

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        
       
        
    }


    public void EnemyWalkToTarget()
    {
        m_Target = Script_GameManager.Instance.PartyManager.m_CurrentParty[1].m_CreatureAi;
        StartCoroutine(SetGoalPosition(m_Target.m_Position));
    }

}
