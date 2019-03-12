using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_EndOfCombatMenu : MonoBehaviour {

    public Text m_Text_DamageTaken;
    public Text m_Text_Deaths;
    public Text m_Text_DamageDealt;
    public Text m_Text_HealAmount;
    public Text m_Text_Grade;
    public GameObject m_Canvas;
    public int m_DamageTaken;
    public int m_Deaths;
    public int m_DamageDealt;
    public int m_HealAmount;
    public int m_Grade;

    public int Score;

    public bool ScoreHasBeenGotten;
    // Use this for initialization
    void Start ()
    {
        m_Text_DamageTaken = GameObject.Find("Text_AmountDamage").GetComponent<Text>();
        m_Text_Deaths = GameObject.Find("Text_AmountDeaths").GetComponent<Text>();
        m_Text_DamageDealt = GameObject.Find("Text_AmountDamageDealt").GetComponent<Text>();
        m_Text_HealAmount = GameObject.Find("Text_AmountHeal").GetComponent<Text>();
        m_Text_Grade = GameObject.Find("Text_FinalValue").GetComponent<Text>();
        m_Canvas = GameObject.Find("Canvas_EndCombatMenu");
        Score = 1000;
        ScoreHasBeenGotten = false;
        m_Canvas.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_Text_DamageTaken.text = m_DamageTaken.ToString();

        m_Text_Deaths.text = m_Deaths.ToString();

        m_Text_DamageDealt.text = m_DamageDealt.ToString();

        m_Text_HealAmount.text = m_HealAmount.ToString();

        

        if(Score <= -1)
        {
            m_Text_Grade.text = "FFF";
        }

        if (Score >= 0)
        {
            m_Text_Grade.text = "F";
        }

        if (Score >= 200)
        {
            m_Text_Grade.text = "D";
        }

        if (Score >= 400)
        {
            m_Text_Grade.text = "C";
        }

        if (Score >= 600)
        {
            m_Text_Grade.text = "B";
        }

        if (Score >= 800)
        {
            m_Text_Grade.text = "A";
        }

        if (Score >= 1000)
        {
            m_Text_Grade.text = "S";
        }

    }

    public void AddToDamageTaken(int a_damagetaken)
    {
        m_DamageTaken += a_damagetaken;
    }

    public void AddToDeaths(int a_Deaths)
    {
        m_Deaths += a_Deaths;
    }

    public void AddToDamageDealt(int a_damageDealt)
    {
        m_DamageDealt += a_damageDealt;
    }

    public void AddToHealAmount(int a_damageHealed)
    {
        m_HealAmount += a_damageHealed;
    }

    public void Reset()
    {
        m_Canvas.SetActive(false);

        m_DamageTaken = 0;
        m_Deaths = 0;
        m_DamageDealt = 0;
        m_HealAmount = 0;
    }

    public void TurnScoreOn()
    {
        ScoreHasBeenGotten = false;
    }

    public void GetScore()
    {
        if (ScoreHasBeenGotten == false)
        {
            m_Canvas.SetActive(true);
            Score = 1000;
            ScoreHasBeenGotten = true;
            Score -= m_DamageTaken;
            Score -= m_Deaths * 200;
            Score += m_DamageDealt/4;
            Score += m_HealAmount;
        }
    }
}
