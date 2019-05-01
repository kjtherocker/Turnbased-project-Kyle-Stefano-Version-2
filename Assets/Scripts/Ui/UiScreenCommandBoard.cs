using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiScreenCommandBoard : UiScreen
{
    public Animator m_CommandBoardAnimator;
    public Script_Creatures m_CommandboardCreature;
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnPop()
    {
        m_CommandBoardAnimator.SetTrigger("t_CommandBoardCrossOut");
        TurnCommandBoardOff();

    }

    public override void OnPush()
    {
        gameObject.SetActive(true);
        m_CommandBoardAnimator.SetTrigger("t_CommandBoardCrossIn");
    }

    public void TurnCommandBoardOff()
    {
        gameObject.SetActive(false);
        

        
    }

    public void PlayerMovement()
    {

        Script_GameManager.Instance.m_Grid.SetWalkingHeuristic(m_CommandboardCreature.m_CreatureAi.m_Position);
        Script_GameManager.Instance.UiManager.PopScreen();
    }

    public void SpawnSkillBoard()
    {
        //Script_GameManager.Instance.UiManager.PushScreen(UiManager.Screen)
    }

}
